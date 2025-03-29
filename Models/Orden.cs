using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Programacion1.Models
{
    public class Orden
    {
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    
    // ✅ Quita cualquier "private set"
    public decimal Total { get; set; }
    public bool Completada { get; set; }
    public string Estado { get; set; }
    
    public List<CarritoItem> Items { get; set; } = new();
    }


}
