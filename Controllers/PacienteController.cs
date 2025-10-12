using Microsoft.AspNetCore.Mvc;
using WebClinicSystem.Models; // Precisamos "importar" nosso modelo Paciente

namespace WebClinicSystem.Controllers
{
    public class PacienteController : Controller
    { // <---- A CLASSE PacienteController COMEÇA AQUI

        // A Action 'Index' será a página principal de pacientes
        public IActionResult Index()
        { // <---- O MÉTODO Index COMEÇA AQUI
            
            // --- CRIAÇÃO DOS DADOS DE EXEMPLO ---
            var listaDePacientes = new List<Paciente>
            {
                new Paciente { Id = 1, NomeCompleto = "Carlos Pereira da Silva", CPF = "123.456.789-00", Telefone = "(11) 98765-4321", DataNascimento = new DateTime(1985, 5, 20) },
                new Paciente { Id = 2, NomeCompleto = "Mariana Souza Lima", CPF = "111.222.333-44", Telefone = "(21) 91234-5678", DataNascimento = new DateTime(1992, 11, 30) },
                new Paciente { Id = 3, NomeCompleto = "Roberto Almeida", CPF = "222.333.444-55", Telefone = "(31) 99999-8888", DataNascimento = new DateTime(1978, 1, 15) },
                new Paciente { Id = 4, NomeCompleto = "Fernanda Costa", CPF = "333.444.555-66", Telefone = "(41) 98888-7777", DataNascimento = new DateTime(2001, 7, 22) }
            };
            // --- FIM DOS DADOS DE EXEMPLO ---

            return View(listaDePacientes);

        } // <---- O MÉTODO Index TERMINA AQUI

        [HttpPost] 
        public IActionResult Salvar(Paciente paciente)
        { // <---- O MÉTODO Salvar COMEÇA AQUI

            Console.WriteLine("=================================");
            Console.WriteLine($"Nome: {paciente.NomeCompleto}");
            Console.WriteLine($"CPF: {paciente.CPF}");
            Console.WriteLine("=================================");
            
            return RedirectToAction("Index");

        } // <---- O MÉTODO Salvar TERMINA AQUI

    } // <---- A CLASSE PacienteController TERMINA AQUI
}