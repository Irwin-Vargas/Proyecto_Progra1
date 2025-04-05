using Microsoft.AspNetCore.Mvc;
using Proyecto_Programacion1.Models;
using System.Collections.Generic;

namespace Proyecto_Programacion1.Controllers
{
    public class UserController : Controller
    {
        private static List<UserModel> usuarios = new List<UserModel>(); // Lista en memoria

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(UserModel usuario)
        {
            if (ModelState.IsValid)
            {
                usuarios.Add(usuario); // Agrega el usuario a la lista
                return RedirectToAction("ListaUsuarios");
            }
            return View(usuario);
        }

        public IActionResult ListaUsuarios()
        {
            return View(usuarios); // Muestra los usuarios registrados
        }
    }
}
