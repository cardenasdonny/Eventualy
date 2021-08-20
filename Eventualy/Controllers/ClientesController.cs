using Eventualy.WEB.Models.DAL;
using Eventualy.WEB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventualy.WEB.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Clientes";
            var clientes = await _context.Clientes.Include(x=>x.TiposDocumento).ToListAsync();
            return View(clientes);
        }

        public async Task<IActionResult> Crear()
        {
            ViewBag.Titulo = "Crear cliente";
            ViewBag.TiposDocumento = new SelectList(await _context.TiposDocumento.ToListAsync(), "TipoDocumentoId", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if(cliente==null)
                return View();
            //Se pregunta si el modelo es valido o no
            if (ModelState.IsValid)
            {
                //Guardamos el cliente
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Titulo = "Crear cliente";
            ViewBag.TiposDocumento = new SelectList(await _context.TiposDocumento.ToListAsync(), "TipoDocumentoId", "Nombre");
            return View(cliente);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                try
                {
                    ViewBag.Titulo = "Editar cliente";
                    ViewBag.TiposDocumento = new SelectList(await _context.TiposDocumento.ToListAsync(), "TipoDocumentoId", "Nombre");
                    var cliente = await _context.Clientes.FindAsync(id);
                    return View(cliente);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            if (cliente == null)
                return View();
            //Se pregunta si el modelo es valido o no
            if (ModelState.IsValid)
            {
                //Guardamos el cliente
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Titulo = "editar cliente";
            ViewBag.TiposDocumento = new SelectList(await _context.TiposDocumento.ToListAsync(), "TipoDocumentoId", "Nombre");
            return View(cliente);
        }



    }
}
