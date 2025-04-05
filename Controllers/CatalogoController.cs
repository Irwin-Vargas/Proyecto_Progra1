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
            new Producto { Id = 1, Nombre = "La Lomo Saltado", Descripcion = "La Lomo Saltado 🍔🔥 con su lomo saltado, papas fritas y salsa criolla", Precio = 20.00m , ImagenUrl="/images/producto1.png" },
            new Producto { Id = 2, Nombre = "Huachana", Descripcion = "La Huachana 🍔🔥 con su chorizo huachano, huevo frito y salsas peruanas", Precio = 18.00m , ImagenUrl="/images/producto2.png"},
            new Producto { Id = 3, Nombre = "Perucha Criolla", Descripcion = "La Criolla 🍔🔥 con su salsa criolla y toque peruano.", Precio = 17.00m, ImagenUrl="/images/producto3.png"},
            new Producto { Id = 4, Nombre = "Perucha Clasica", Descripcion = "La Clásica  🍔🔥 con salsa especial de la casa.", Precio = 15.00m , ImagenUrl="/images/producto4.png" },
            new Producto { Id = 5, Nombre = "Perucha Anticuchera", Descripcion = "La Anticuchera 🍔🔥 con su carne marinada y papas nativas crocantes.", Precio = 20.00m , ImagenUrl="/images/producto5.png"},
            new Producto { Id = 6, Nombre = "Trilogia Perucha", Descripcion = "La Trilogía Andina 🍔🔥 con su mezcla de res, alpaca y cerdo, queso andino y rocoto.", Precio = 26.00m , ImagenUrl="/images/producto6.png" }
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