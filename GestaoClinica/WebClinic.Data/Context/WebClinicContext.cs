using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinic.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace WebClinic.Data.Context
{   
    public class WebClinicContext : DbContext
    {
        public WebClinicContext(DbContextOptions<WebClinicContext> options) : base(options)
        {
        }

        // Mapeia nossa classe Paciente para a tabela "Pacientes" no banco de dados.
        // Adicionaremos um DbSet para cada entidade (Profissionais, Consultas, etc.)
        public DbSet<Paciente> Pacientes { get; set; }
        // public DbSet<Profissional> Profissionais { get; set; } // Exemplo para o futuro
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
    }
}
