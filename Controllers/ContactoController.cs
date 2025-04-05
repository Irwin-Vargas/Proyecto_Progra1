using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Proyecto_Programacion1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ContactoController : Controller
    {
        public IActionResult Index() => View();
    }
}
