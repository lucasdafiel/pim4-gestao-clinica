using WebClinic.Core.Models;

namespace WebClinic.Web.Services
{
    /// <summary>
    /// Contrato para o serviço de geração de tokens de autenticação.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Gera um token JWT para um usuário específico.
        /// </summary>
        /// <param name="usuario">O objeto do usuário para o qual o token será gerado.</param>
        /// <returns>Uma string representando o token JWT.</returns>
        string GenerateToken(Usuario usuario);
    }
}