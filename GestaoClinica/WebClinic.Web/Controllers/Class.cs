using Microsoft.AspNetCore.Mvc;
using WebClinic.Core.Interfaces;
using WebClinic.Core.Models;

namespace WebClinic.Web.Controllers
{
    // Este controller é responsável por retornar as VIEWS (páginas HTML)
    public class PacienteViewController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteViewController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        // AÇÃO: Listar todos os pacientes
        // ROTA: /PacienteView/Index
        public IActionResult Index()
        {
            var pacientes = _pacienteRepository.ListarTodos();
            // Retorna a View "Index.cshtml" dentro da pasta "Views/PacienteView"
            // e passa a lista de pacientes para ela
            return View(pacientes);
        }

        // AÇÃO: Mostrar formulário para criar um novo paciente
        // ROTA: /PacienteView/Create
        public IActionResult Create()
        {
            return View();
        }

        // AÇÃO: Receber os dados do formulário e salvar o novo paciente
        [HttpPost]
        [ValidateAntiForgeryToken] // Medida de segurança contra ataques
        public IActionResult Create([Bind("NomeCompleto,CPF,DataNascimento,TelefoneContato,Email")] Paciente paciente)
        {
            // Verifica se os dados recebidos são válidos (ex: campos obrigatórios)
            if (ModelState.IsValid)
            {
                _pacienteRepository.Adicionar(paciente);
                return RedirectToAction(nameof(Index)); // Redireciona para a lista
            }
            // Se o modelo não for válido, retorna para o formulário com os dados preenchidos
            return View(paciente);
        }

        // AÇÃO: Mostrar formulário para editar um paciente existente
        // ROTA: /PacienteView/Edit/5 (onde 5 é o ID do paciente)
        public IActionResult Edit(int id)
        {
            var paciente = _pacienteRepository.ObterPorId(id);
            if (paciente == null)
            {
                return NotFound(); // Retorna erro 404 se não encontrar
            }
            return View(paciente);
        }

        // AÇÃO: Receber os dados do formulário de edição e salvar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PacienteId,NomeCompleto,CPF,DataNascimento,TelefoneContato,Email,DataCadastro")] Paciente paciente)
        {
            if (id != paciente.PacienteId)
            {
                return BadRequest(); // Retorna erro se os IDs não baterem
            }

            if (ModelState.IsValid)
            {
                _pacienteRepository.Atualizar(paciente);
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // AÇÃO: Mostrar página de confirmação para exclusão
        // ROTA: /PacienteView/Delete/5
        public IActionResult Delete(int id)
        {
            var paciente = _pacienteRepository.ObterPorId(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // AÇÃO: Confirmar e excluir o paciente
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _pacienteRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}