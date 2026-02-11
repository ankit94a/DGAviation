
using MasterApplication.DB.Services;
using static MasterApplication.DB.Enum.Enum;

namespace MasterApplication.Server.Helpers
{
    public static class ClaimHelper
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            var userIdClaim = httpContext.User?.Claims?.FirstOrDefault(c => c.Type == MasterConstant.UserId);

            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value))
            {
                throw new UnauthorizedAccessException("User ID claim is missing.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID value.");
            }

            return userId;
        }

        public static string GetNameId(this HttpContext httpContext)
        {
            var userIdClaim = httpContext.User?.Claims?.FirstOrDefault(c => c.Type == MasterConstant.UserId)?.Value ?? "";

            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim))
            {
                throw new UnauthorizedAccessException("Name ID claim is missing.");
            }

            

            return userIdClaim;
        }
        public static RoleType GetRoleType(this HttpContext httpContext)
        {
            var roleClaim = httpContext.User?.Claims?.FirstOrDefault(c => c.Type == MasterConstant.RoleType);
           

            if (roleClaim == null || string.IsNullOrEmpty(roleClaim.Value))
            {
                throw new UnauthorizedAccessException("User role claim is missing.");
            }

            if (!Enum.TryParse<RoleType>(roleClaim.Value, out var role))
            {
                throw new UnauthorizedAccessException("Invalid role type.");
            }

            return role;
        }


    }
}
