using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Programacion1.Models;

namespace Proyecto_Programacion1.Controllers
{
    public class CarritoController : Controller
    {
        public static List<Orden> _ordenes = new();

        public IActionResult Index()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();
            return View(carrito);
        }

        public IActionResult Agregar(int id)
        {
            var producto = ObtenerProductoPorId(id);
            if (producto != null)
            {
                var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();
                var itemExistente = carrito.FirstOrDefault(c => c.ProductoId == id);

                if (itemExistente != null)
                {
                    itemExistente.Cantidad++;
                }
                else
                {
                    carrito.Add(new CarritoItem
                    {
                        ProductoId = producto.Id,
                        Nombre = producto.Nombre,
                        Precio = producto.Precio,
                        Cantidad = 1
                    });
                }

                HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();
            var item = carrito.FirstOrDefault(c => c.ProductoId == id);

            if (item != null)
            {
                carrito.Remove(item);
                HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ActualizarCantidad(int id, string accion)
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();
            var item = carrito.FirstOrDefault(c => c.ProductoId == id);

            if (item != null)
            {
                if (accion == "incrementar") item.Cantidad++;
                if (accion == "decrementar" && item.Cantidad > 1) item.Cantidad--;

                HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ConfirmarCompra()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();

            if (carrito.Any())
            {
                var orden = new Orden
                {
                    Id = _ordenes.Count + 1,
                    Fecha = DateTime.Now,
                    Total = carrito.Sum(c => c.Cantidad * c.Precio),
                    Estado = "Pendiente",
                    Items = carrito.ToList()
                };

                _ordenes.Add(orden);

                // 🧹 Limpiar carrito después de confirmar la compra
                HttpContext.Session.SetObjectAsJson("Carrito", new List<CarritoItem>());
            }

            return RedirectToAction("Index", "Orden");
        }

        private Producto? ObtenerProductoPorId(int id)
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Perucha Clásica", Descripcion = "Hamburguesa con salsa especial", Precio = 17.0m },
                new Producto { Id = 2, Nombre = "Perucha Criolla", Descripcion = "Hamburguesa con salsa criolla", Precio = 17.0m },
                new Producto { Id = 3, Nombre = "Trilogía Perucha", Descripcion = "Combo de hamburguesas peruanas", Precio = 26.0m }
            };
            return productos.FirstOrDefault(p => p.Id == id);
        }
    }
}
