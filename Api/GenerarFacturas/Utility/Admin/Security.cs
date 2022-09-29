using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Security;

namespace Utility.Admin
{
    public static class Security
    {
        public static Tuple<byte[], string> Generatehashed(string password)
        {
            var salt = GenerateSalt();
            Tuple<byte[], string> response;
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(

                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            response = new Tuple<byte[], string>(salt, hashed);
            return response;

        }
        public static bool ConfirmationPassword(string password,
            byte[] salt, string passwordHash)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            if (hashed == passwordHash)
            {
                return true;
            }
            return false;

        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static string GenerateToken(TokenSetting setting, List<Claim> claims)
        {
            //creamos el header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _header = new JwtHeader(_signingCredentials);

            //creara el payload
            var _payLoad = new JwtPayload(
                issuer: setting.Issuer,
                audience: setting.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(setting.Expiration));

            var _token = new JwtSecurityToken(
                _header,
                _payLoad);
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }

    }
}
