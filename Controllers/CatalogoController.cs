using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Programacion1.Models;
using System.Collections.Generic;

namespace Proyecto_Programacion1.Controllers
{
    [Route("catalogo")]
    public class CatalogoController : Controller
    {
        private static List<Producto> productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Producto A", Descripcion = "Descripción del Producto A", Precio = 50.00m },
            new Producto { Id = 2, Nombre = "Producto B", Descripcion = "Descripción del Producto B", Precio = 75.00m },
            new Producto { Id = 3, Nombre = "Producto C", Descripcion = "Descripción del Producto C", Precio = 100.00m }
        };

        [HttpGet("")]
        public IActionResult Index()
        {
            return View(productos);
        }

        // ✅ Mostrar detalles del producto
        [HttpGet("detalle/{id}")]
        public IActionResult Detalle(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
    }
}