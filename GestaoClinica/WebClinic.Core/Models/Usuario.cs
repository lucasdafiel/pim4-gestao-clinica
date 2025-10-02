using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebClinic.Core.Models
{
    /// <summary>
    /// Representa um usuário autenticável no sistema.
    /// Mapeia a tabela 'Usuarios' do banco de dados.
    /// </summary>
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string SenhaHash { get; set; }

        public bool Ativo { get; set; }

        // Chave estrangeira para a tabela Perfis
        public int PerfilId { get; set; }

        // Propriedade de navegação para EF Core: Um usuário tem um perfil.
        [ForeignKey("PerfilId")]
        public virtual Perfil Perfil { get; set; }
    }
}