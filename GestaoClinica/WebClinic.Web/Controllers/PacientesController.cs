using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClinic.Core.Interfaces;
using WebClinic.Core.Models;

namespace WebClinic.Web.Controllers
{
    [ApiController] // ETIQUETA 1: Identifica esta classe como um Controller de API
    [Route("api/[controller]")] // ETIQUETA 2: Define a rota base como "/api/pacientes"
    [Authorize] // ETIQUETA 3: Exige que o usuário esteja autenticado para acessar os endpoints
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacientesController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_pacienteRepository.ListarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var paciente = _pacienteRepository.ObterPorId(id);
            if (paciente == null) return NotFound();
            return Ok(paciente);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Paciente novoPaciente)
        {
            if (novoPaciente == null) return BadRequest();

            if (_pacienteRepository.ObterPorCPF(novoPaciente.CPF) != null)
            {
                return BadRequest("CPF já cadastrado.");
            }
            _pacienteRepository.Adicionar(novoPaciente);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoPaciente.PacienteId }, novoPaciente);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Paciente pacienteAtualizado)
        {
            if (id != pacienteAtualizado.PacienteId) return BadRequest();
            var pacienteExistente = _pacienteRepository.ObterPorId(id);
            if (pacienteExistente is null) return NotFound();

            var pacienteComMesmoCPF = _pacienteRepository.ObterPorCPF(pacienteAtualizado.CPF);
            if (pacienteComMesmoCPF != null && pacienteComMesmoCPF.PacienteId != id)
            {
                return BadRequest("CPF já pertence a outro paciente.");
            }

            _pacienteRepository.Atualizar(pacienteAtualizado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var paciente = _pacienteRepository.ObterPorId(id);
            if (paciente == null) return NotFound();

            _pacienteRepository.Excluir(id);
            return NoContent();
        }
    }
}