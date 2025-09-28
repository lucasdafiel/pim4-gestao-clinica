using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinic.Core.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TelefoneContato { get; set; }
        public string? Email { get; set; } // O '?' indica que o campo pode ser nulo (opcional)
        public DateTime DataCadastro { get; set; }
    }
}
