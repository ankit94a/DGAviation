using MasterApplication.DB.Interface;
using MasterApplication.DB.Models;
using MasterApplication.DB.Services;
using MasterApplication.Server.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
namespace MasterApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserDB _userDB;
        private readonly LoginAttemptService _loginAttemptService;
        private readonly IJwtManager _jwtManager;
        public AuthController(IUserDB userDB,LoginAttemptService loginAttemptService,IJwtManager jwtManager) 
        {
            _userDB = userDB;
            _loginAttemptService = loginAttemptService;
            _jwtManager = jwtManager;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> DoLogin([FromBody] Login login)
        {
            try
            {
                /* -------------------------
                   1. Basic request validation
                   -------------------------- */
                if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                {
                    return BadRequest(new { message = "Username and password are required." });
                }
                if (string.IsNullOrEmpty(login.Code) || string.IsNullOrEmpty(login.Token))
                {
                    return BadRequest(new { message = "Captcha validation is required." });
                }
                /* -------------------------
                   2. Client context
                -------------------------- */
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

                /* -------------------------
                   3. Rate limiting
                -------------------------- */
                if (_loginAttemptService.IsBlocked(ip))
                {
                    return Unauthorized(new { message = "Too many failed login attempts. Please try again after 15 minutes." });
                }
                /* -------------------------
                  4. Captcha validation
               -------------------------- */
                if (!ValidateCaptcha(login.Code, login.Token))
                {
                    _loginAttemptService.RecordFailedAttempt(ip);
                    return Unauthorized(new { message = "Invalid login credentials." });
                }

                /* -------------------------
                   5. Decrypt credentials
                -------------------------- */
                var rsaService = new RSAKeyManager();
                string decryptedUserName, decryptedPassword;
                try
                {
                    decryptedUserName = rsaService.Decrypt(login.UserName);
                    decryptedPassword = rsaService.Decrypt(login.Password);
                }
                catch (Exception)
                {
                    return BadRequest(new { message = "Invalid authentication data." });
                }
                /* -------------------------
                   6. User lookup
                -------------------------- */
                var user = await _userDB.GetUserByEmail(decryptedUserName);

                if (user == null)
                {
                    _loginAttemptService.RecordFailedAttempt(ip);
                    return Unauthorized(new { message = "Invalid login credentials." });
                }
                /* -------------------------
                   7. Password verification
                -------------------------- */
                bool passwordValid;
                try
                {
                    passwordValid = BCrypt.Net.BCrypt.Verify(decryptedPassword, user.Password);
                }
                catch
                {
                    _loginAttemptService.RecordFailedAttempt(ip);
                    return Unauthorized(new { message = "Invalid login credentials." });
                }

                if (!passwordValid)
                {
                    _loginAttemptService.RecordFailedAttempt(ip);
                    return Unauthorized(new { message = "Invalid login credentials." });
                }
                /* -------------------------
                   8. Session + JWT
                -------------------------- */
                var sessionId = Guid.NewGuid();
                var userClaims = new UserClaims
                {
                    UserId = user.Id,
                    RoleType = user.RoleType.ToString(),
                    Name = user.Name,
                    SessionId = sessionId.ToString(),
                };
                var jwtToken = _jwtManager.GenerateJwtToken(userClaims);
                Response.Cookies.Append("auth_token", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15)
                });

                await _userDB.UpsertUserSession(user.Id, sessionId,ip,userAgent);
                _loginAttemptService.ResetAttempts(ip);
                

                return Ok(new { msg = "Login successful." });
            }

            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Error during login.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred. Please try again later." });
            }

        }


        [HttpGet("publickey")]
        [AllowAnonymous]
        public IActionResult GetPublicKey()
        {
            var rsaService = new RSAKeyManager();
            string publicKeyXml = rsaService.GetPublicKeyXml();
            return Ok(new { key = publicKeyXml });
        }

        [AllowAnonymous]
        [HttpPost, Route("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] Login request)
        {
            if (!PasswordValidator.IsComplex(request.Password))
            {
                return BadRequest("Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character.");
            }
            var user = await _userDB.GetUserByEmail(request.UserName);
            if (user == null)
                return NotFound(new { message = "User not found" });
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
            _userDB.UpdatePassword(user.Id, hashedPassword);

            return Ok(new { message = "Password reset link sent to your email." });
        }


        [AllowAnonymous]
        [HttpGet("generate")]
        public IActionResult GenerateCaptcha()
        {
            var code = "12345";
            // GenerateRandomCode(5);
            var token = Guid.NewGuid().ToString();
            CaptchaStore.Captchas[token] = code;

            return Ok(new { token, code });
        }

        private bool ValidateCaptcha(string Code, string Token)
        {
            if (CaptchaStore.Captchas.TryGetValue(Token, out var correctCode))
            {
                CaptchaStore.Captchas.Remove(Token);

                if (string.Equals(Code, correctCode, StringComparison.OrdinalIgnoreCase))
                    return true;

                return false;
            }

            return false;
        }

        private static class PasswordValidator
        {
            public static bool IsComplex(string password)
            {
                if (string.IsNullOrWhiteSpace(password))
                    return false;

                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$");
                return regex.IsMatch(password);
            }
        }

    }
}
