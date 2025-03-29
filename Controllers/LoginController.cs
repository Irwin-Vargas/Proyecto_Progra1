using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Proyecto_Programacion1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("Index", "Home");
            }
            else if (username == "cliente" && password == "123")
            {
                HttpContext.Session.SetString("UserRole", "Cliente");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Error"] = "Usuario o contraseña incorrectos";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}