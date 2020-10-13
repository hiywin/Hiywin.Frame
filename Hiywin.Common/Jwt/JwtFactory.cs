using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.Common.Jwt
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuserOptions _jwtOptions;
        public JwtFactory(IOptions<JwtIssuserOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> GenerateEncodedToken(string userNo, ClaimsIdentity identity)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,userNo),
                new Claim(JwtRegisteredClaimNames.Jti,await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),ClaimValueTypes.Integer64),
                identity.FindFirst("userNo"),
                identity.FindFirst("userName"),
                identity.FindFirst("realName"),
                identity.FindFirst("staffNo"),
                identity.FindFirst("adAccount"),
                identity.FindFirst("mobile"),
                identity.FindFirst("email"),
                identity.FindFirst("isAdmin"),
                identity.FindFirst("appNo")
            };

            //Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenModel
            {
                auth_token = encodeJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                token_type = "Bearer"
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public ClaimsIdentity GenerateClaimsIdentity(LoginUser user)
        {
            var claimsIdentity = new ClaimsIdentity(new GenericIdentity(user.UserNo, "Token"));
            claimsIdentity.AddClaim(new Claim("userNo", user.UserNo));
            claimsIdentity.AddClaim(new Claim("userName", user.UserName));
            claimsIdentity.AddClaim(new Claim("realName", string.IsNullOrEmpty(user.RealName) ? string.Empty : user.RealName));
            claimsIdentity.AddClaim(new Claim("staffNo", string.IsNullOrEmpty(user.StaffNo) ? string.Empty : user.StaffNo));
            claimsIdentity.AddClaim(new Claim("adAccount", string.IsNullOrEmpty(user.AdAccount) ? string.Empty : user.AdAccount));
            claimsIdentity.AddClaim(new Claim("mobile", string.IsNullOrEmpty(user.Mobile) ? string.Empty : user.Mobile));
            claimsIdentity.AddClaim(new Claim("email", string.IsNullOrEmpty(user.Email) ? string.Empty : user.Email));
            claimsIdentity.AddClaim(new Claim("isAdmin", user.IsAdmin.ToString()));
            claimsIdentity.AddClaim(new Claim("appNo", string.IsNullOrEmpty(user.AppNo) ? string.Empty : user.AppNo));

            return claimsIdentity;
        }

        #region Extensions

        public static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        #endregion
    }
}
