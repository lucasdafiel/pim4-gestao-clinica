// Local: /Controllers/ProntuarioController.cs
using Microsoft.AspNetCore.Mvc;

namespace WebClinicSystem.Controllers
{
    public class ProntuarioController : Controller
    {
        // Em um sistema real, este método receberia o ID do paciente. Ex: Index(int id)
        // Para o front-end, vamos apenas mostrar um prontuário de exemplo.
        public IActionResult Index()
        {
            return View();
        }
    }
}