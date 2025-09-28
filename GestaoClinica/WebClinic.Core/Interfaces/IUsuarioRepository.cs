using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinic.Core.Models;

namespace WebClinic.Core.Interfaces
{
    /// <summary>
    /// Contrato para operações de acesso a dados da entidade Usuario.
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Busca um usuário pelo seu email, incluindo seu perfil associado.
        /// </summary>
        /// <param name="email">O email do usuário a ser buscado.</param>
        /// <returns>O objeto Usuario ou nulo se não for encontrado.</returns>
        Task<Usuario> GetByEmailAsync(string email);
    }
}
