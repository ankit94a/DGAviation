
using MasterApplication.DB.Interface;
using MasterApplication.Server.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MasterApplication.Server.Extensions
{
    public static class JwtTokenConfig
    {
        public static void AddJwtTokenAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                                    .AddJwtBearer(x =>
                                    {

                                        x.TokenValidationParameters = new TokenValidationParameters()
                                        {
                                            ValidateIssuerSigningKey = true,
                                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),
                                            ValidateIssuer = true,
                                            ValidateAudience = true,
                                            ValidateLifetime = true,
                                            ValidIssuer = configuration["JWTSettings:Issuer"],
                                            ValidAudience = configuration["JWTSettings:Audience"],

                                            ClockSkew = TimeSpan.Zero,
                                        };
                                        x.RequireHttpsMetadata = false;
                                        x.SaveToken = true;
                                        x.Events = new JwtBearerEvents()
                                        {
                                            /* -------------------------
                                               1. Read token from cookie
                                            -------------------------- */
                                            OnMessageReceived = context =>
                                            {
                                                //if (context.HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                                                //{
                                                var endpoint = context.HttpContext.GetEndpoint();
                                                var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

                                                if (allowAnonymous)
                                                {
                                                    context.NoResult();
                                                    return Task.CompletedTask;
                                                }
                                                var token = context.HttpContext.Request.Cookies["auth_token"];
                                                if (!string.IsNullOrEmpty(token))
                                                {
                                                    context.Token = token;
                                                }
                                                //}
                                                //else
                                                //{
                                                //    context.NoResult();
                                                //}

                                                return Task.CompletedTask;
                                            },
                                            /* -------------------------
                                               2. Validate session per request
                                            -------------------------- */
                                            OnTokenValidated = async context =>
                                            {
                                                var endpoint = context.HttpContext.GetEndpoint();
                                                var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

                                                if (allowAnonymous)
                                                    return;
                                                var claims = context.Principal;

                                                var userIdClaim = claims?.FindFirst("userid")?.Value;
                                                var sessionIdClaim = claims?.FindFirst("sessionid")?.Value;

                                                if (!long.TryParse(userIdClaim, out var userId) ||
                                                    !Guid.TryParse(sessionIdClaim, out var sessionId))
                                                {
                                                    context.Fail("Invalid token claims.");
                                                    return;
                                                }
                                                var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
                                                var userAgent = context.HttpContext.Request.Headers.UserAgent.ToString();

                                                if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrEmpty(userAgent))
                                                {
                                                    context.Fail("Missing client context.");
                                                    return;
                                                }
                                                var userDb = context.HttpContext.RequestServices.GetRequiredService<IUserDB>();

                                                var isValidSession = await userDb.ValidateUserSession(userId, sessionId, ipAddress, userAgent);

                                                if (!isValidSession)
                                                {
                                                    context.Fail("Session expired or revoked.");
                                                }
                                            },

                                            /* -------------------------
                                               3. Token expired
                                            -------------------------- */

                                            OnAuthenticationFailed = context =>
                                            {
                                                if (context.Exception is SecurityTokenExpiredException)
                                                {
                                                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                                    context.Response.ContentType = "application/json";

                                                    return context.Response.WriteAsync(
                                                        "{\"message\":\"Session expired. Please log in again.\"}");
                                                }

                                                return Task.CompletedTask;
                                            },
                                            /* -------------------------
                                               4. Unauthorized access
                                            -------------------------- */
                                            OnChallenge = context =>
                                            {
                                                var endpoint = context.HttpContext.GetEndpoint();
                                                var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

                                                if (allowAnonymous)
                                                {
                                                    context.HandleResponse();
                                                    return Task.CompletedTask;
                                                }
                                                context.HandleResponse();
                                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                                context.Response.ContentType = "application/json";
                                                return context.Response.WriteAsync(
                                                    "{\"message\":\"Authentication is required to access this resource.\"}");


                                            },
                                            /* -------------------------
                                               5. Forbidden (role-based)
                                            -------------------------- */
                                            OnForbidden = context =>
                                            {
                                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                                context.Response.ContentType = "application/json";

                                                return context.Response.WriteAsync(
                                                    "{\"message\":\"You do not have permission to access this resource.\"}");
                                            }
                                        };
                                    });

        }
    }
}