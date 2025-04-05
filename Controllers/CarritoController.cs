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
                new Producto { Id = 1, Nombre = "La Lomo Saltado", Descripcion = "La Lomo Saltado 🍔🔥 con su lomo saltado, papas fritas y salsa criolla", Precio = 20.00m , ImagenUrl="/images/producto1.png" },
                new Producto { Id = 2, Nombre = "Huachana", Descripcion = "La Huachana 🍔🔥 con su chorizo huachano, huevo frito y salsas peruanas", Precio = 18.00m , ImagenUrl="/images/producto2.png"},
                new Producto { Id = 3, Nombre = "Perucha Criolla", Descripcion = "La Criolla 🍔🔥 con su salsa criolla y toque peruano.", Precio = 17.00m, ImagenUrl="/images/producto3.png"},
                new Producto { Id = 4, Nombre = "Perucha Clasica", Descripcion = "La Clásica  🍔🔥 con salsa especial de la casa.", Precio = 15.00m , ImagenUrl="/images/producto4.png" },
                new Producto { Id = 5, Nombre = "Perucha Anticuchera", Descripcion = "La Anticuchera 🍔🔥 con su carne marinada y papas nativas crocantes.", Precio = 20.00m , ImagenUrl="/images/producto5.png"},
                new Producto { Id = 6, Nombre = "Trilogia Perucha", Descripcion = "La Trilogía Andina 🍔🔥 con su mezcla de res, alpaca y cerdo, queso andino y rocoto.", Precio = 26.00m , ImagenUrl="/images/producto6.png" }
            };
            return productos.FirstOrDefault(p => p.Id == id);
        }
    }
}
