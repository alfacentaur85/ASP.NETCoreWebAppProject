using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Bot.Schema;

namespace Security
{
    public sealed class SecurityService : ISecurityService
    {
        /*private IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            {"test", "test"}
        };

        public const string SecretCode = "df;ko%k;ky)=-=965;ljdfjmfg;i";

        public TokenResponse Authenticate(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            int i = 0;
            foreach (KeyValuePair<string, string> pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Key, user) == 0 && string.CompareOrdinal(pair.Value, password) == 0)
                {
                    return new TokenResponse(GenerateJwtToken(i));
                }
                
            }
            return null;

        }

        private string GenerateJwtToken(int id)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }*/

        private IDictionary<string, AuthResponse> _users = new Dictionary<string, AuthResponse>()
        {
            {"test", new AuthResponse() { Password = "test"}}
        };
        public const string SecretCode = "df;ko%k;ky)=-=965;ljdfjmfg;i";
        public TokenResponse Authenticate(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            TokenResponse tokenResponse = new TokenResponse();
            int i = 0;
            foreach (KeyValuePair<string, AuthResponse> pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Key, user) == 0 && string.CompareOrdinal(pair.Value.Password, password) == 0)
                {
                    tokenResponse.Token = GenerateJwtToken(i, 15);
                    RefreshToken refreshToken = GenerateRefreshToken(i);
                    pair.Value.LatestRefreshToken = refreshToken;
                    tokenResponse.RefreshToken = refreshToken.Token;
                    return tokenResponse;

                }

            }

            return null;
        }

        public string RefreshToken(string token)
        {

            int i = 0;
            foreach (KeyValuePair<string, AuthResponse> pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Value.LatestRefreshToken.Token, token) == 0
                    && pair.Value.LatestRefreshToken.IsExpired is false)
                {
                    pair.Value.LatestRefreshToken = GenerateRefreshToken(i);
                    return pair.Value.LatestRefreshToken.Token;
                }
            }
            return string.Empty;
        }

        private string GenerateJwtToken(int id, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public RefreshToken GenerateRefreshToken(int id)
        {
            RefreshToken refreshToken = new RefreshToken();
            refreshToken.Expires = DateTime.Now.AddMinutes(360);
            refreshToken.Token = GenerateJwtToken(id, 360);
            return refreshToken;
        }
    }
}




