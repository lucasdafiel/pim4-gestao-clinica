using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebClinic.Core.Models;

namespace WebClinic.Web.Services
{
    /// <summary>
    /// Implementação do serviço de geração de tokens JWT.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            // O handler é responsável por criar e serializar o token.
            var tokenHandler = new JwtSecurityTokenHandler();

            // A chave secreta é lida do appsettings.json para maior segurança.
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            // O 'descriptor' contém todas as informações que farão parte do token (payload).
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    // Claims são "alegações" sobre o usuário que o token carrega.
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()), // Armazena o ID do usuário
                    new Claim(ClaimTypes.Email, usuario.Email),                         // Armazena o Email
                    new Claim(ClaimTypes.Role, usuario.Perfil.Nome)                      // Armazena o Perfil (Role)
                }),
                // Define o tempo de expiração do token.
                Expires = DateTime.UtcNow.AddHours(8),

                // Define as credenciais de assinatura, usando a chave e o algoritmo de segurança.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Cria o token com base no descritor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Retorna o token serializado como uma string.
            return tokenHandler.WriteToken(token);
        }
    }
}