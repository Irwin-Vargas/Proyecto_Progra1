using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Programacion1.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty; // ✅ Evita nulos
        public string Descripcion { get; set; } = string.Empty; // ✅ Evita nulos
        public decimal Precio { get; set; }
    }
}
