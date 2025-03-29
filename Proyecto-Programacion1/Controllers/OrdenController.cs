using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Programacion1.Models;
using Microsoft.AspNetCore.Authorization;



namespace Proyecto_Programacion1.Controllers
{
    public class OrdenController : Controller
    {
        public IActionResult Index()
        {
            var ordenes = CarritoController._ordenes;
            return View(ordenes);
        }
    }
}