// Local: /Controllers/LoginController.cs

using Microsoft.AspNetCore.Mvc;

namespace WebClinicSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}