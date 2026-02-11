
using MasterApplication.DB.Services;

namespace MasterApplication.Server.Authorization
{
	public interface IJwtManager
	{
        string GenerateJwtToken(UserClaims userClaims);
    }
}
