using System.Security.Claims;
using System.Threading.Tasks;

namespace Hiywin.Common.Jwt
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userNo, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(LoginUser user);
    }
}
