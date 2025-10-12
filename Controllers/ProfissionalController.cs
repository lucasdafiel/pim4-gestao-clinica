// Local: /Controllers/ProfissionalController.cs

using Microsoft.AspNetCore.Mvc;
using WebClinicSystem.Models; // Importa nossos modelos

namespace WebClinicSystem.Controllers;

public class ProfissionalController : Controller
{
    public IActionResult Index()
    {
        // Criando uma lista de exemplo de profissionais
        var listaDeProfissionais = new List<Profissional>
        {
            new Profissional { Id = 1, NomeCompleto = "Dr.ª Ana Costa", Especialidade = "Clínica Geral", Email = "ana.costa@webclinic.com" },
            new Profissional { Id = 2, NomeCompleto = "Dr. João Alves", Especialidade = "Fisioterapia", Email = "joao.alves@webclinic.com" },
            new Profissional { Id = 3, NomeCompleto = "Dr.ª Sofia Borges", Especialidade = "Nutrição", Email = "sofia.borges@webclinic.com" }
        };

        // Enviando a lista para a View
        return View(listaDeProfissionais);
    }
}