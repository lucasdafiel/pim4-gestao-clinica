// Local: /Controllers/UsuarioController.cs
using Microsoft.AspNetCore.Mvc;
using WebClinicSystem.Models;

namespace WebClinicSystem.Controllers;

public class UsuarioController : Controller
{
    public IActionResult Index()
    {
        var listaDeUsuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Email = "admin@webclinic.com", Perfil = "Administrador" },
            new Usuario { Id = 2, Email = "ana.costa@webclinic.com", Perfil = "Profissional de Saúde" },
            new Usuario { Id = 3, Email = "carlos.almeida@webclinic.com", Perfil = "Recepcionista" }
        };

        return View(listaDeUsuarios);
    }
}