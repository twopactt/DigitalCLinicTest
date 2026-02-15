using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalCLinicTest.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _signingKey;
        private readonly TokenValidationParameters _validationParameters;
        public static readonly HashSet<string> _blacklist = new HashSet<string>();

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            var secretKey = _configuration["Jwt:SecretKey"]
                ?? throw new ArgumentNullException("Jwt:SecretKey не найден в конфигурации");

            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            _validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = _signingKey,
                ClockSkew = TimeSpan.Zero
            };
        }

        public string GenerateToken(string userId, string username, List<string> roles = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId не может быть пустым", nameof(userId));

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username не может быть пустым", nameof(username));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim("role", role));
                }
            }

            var tokenLifetime = TimeSpan.FromMinutes(
                Convert.ToDouble(_configuration["Jwt:TokenLifetimeMinutes"] ?? "120"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(tokenLifetime),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public string RefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("RefreshToken не может быть пустым", nameof(refreshToken));

            if (_blacklist.Contains(refreshToken))
                throw new SecurityTokenException("Токен недействителен или отозван");

            var principal = ValidateToken(refreshToken);
            if (principal == null)
                throw new SecurityTokenException("Невозможно валидировать токен");

            var userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var username = principal.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

            var roles = principal.FindAll("role")
                .Select(c => c.Value)
                .ToList();

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(username))
                throw new SecurityTokenException("Неверный формат токена: отсутствует идентификатор пользователя или имя");

            return GenerateToken(userId, username, roles);
        }

        public void RevokeToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token не может быть пустым", nameof(token));

            _blacklist.Add(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token не может быть пустым", nameof(token));

            if (_blacklist.Contains(token))
                throw new SecurityTokenException("Токен отозван");

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _validationParameters, out var validatedToken);

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new SecurityTokenExpiredException("Срок действия токена истёк");
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException($"Ошибка валидации токена: {ex.Message}");
            }
        }

        public string GetUserIdFromToken(string token)
        {
            var principal = ValidateToken(token);
            return principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        }

        public string GetUsernameFromToken(string token)
        {
            var principal = ValidateToken(token);
            return principal.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
        }

        public bool IsTokenValid(string token)
        {
            try
            {
                ValidateToken(token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return _validationParameters;
        }
    }
}
