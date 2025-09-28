using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebClinic.Core.Interfaces;
using WebClinic.Core.Models;
using WebClinic.Data.Context;

namespace WebClinic.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório para a entidade Usuario.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly WebClinicContext _context;

        public UsuarioRepository(WebClinicContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            // Utiliza o método Include para carregar o Perfil do usuário na mesma consulta (eager loading).
            // Isso evita consultas adicionais ao banco (problema N+1).
            return await _context.Usuarios
                                 .Include(u => u.Perfil)
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}