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
    /// Representa um perfil de usuário no sistema (ex: Administrador, Recepcionista).
    /// Mapeia a tabela 'Perfis' do banco de dados.
    /// </summary>
    [Table("Perfis")]
    public class Perfil
    {
        [Key]
        public int PerfilId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        // Propriedade de navegação para EF Core: Um perfil pode ter vários usuários.
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}