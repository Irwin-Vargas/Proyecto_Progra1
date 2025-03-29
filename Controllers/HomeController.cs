using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Programacion1.Models;

namespace Proyecto_Programacion1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        // ✅ Página de inicio
        public IActionResult Index() => View();

        // ✅ Página de privacidad
        public IActionResult Privacy() => View();

        // ✅ Dashboard solo para admin
        public IActionResult Dashboard()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // ✅ Panel para clientes
        public IActionResult ClientePanel()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Cliente")
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}

