using Eventualy.WEB.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Abstract;
using Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly ITipoDocumentoService _tipoDocumentoService;

        public ClientesController(IClienteService clienteService, ITipoDocumentoService tipoDocumentoService)
        {
            _clienteService = clienteService; //inyectamos la interface IclienteService
            _tipoDocumentoService = tipoDocumentoService;
        }
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObtenerClientes();
            return View(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Titulo = "Crear cliente";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoService.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if (cliente == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clienteService.Crear(cliente);
                    var guardar = await _clienteService.GuardarCambios();
                    if (guardar)
                        return RedirectToAction("Index");
                    else
                        return NotFound();

                }
                catch (Exception)
                {
                    throw;
                }
            }
            // si el modelo tiene errores en las validaciones
            ViewBag.Titulo = "Crear cliente";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoService.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id != null)
            {
                try
                {
                    var cliente = await _clienteService.ObtenerClientePorId(id.Value);
                    if (cliente != null)
                        return View(cliente);
                    else
                        return NotFound();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                try
                {
                    var cliente = await _clienteService.ObtenerClientePorId(id.Value);
                    if (cliente != null)
                    {
                        ViewBag.Titulo = "Editar cliente";
                        ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoService.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
                        return View(cliente);
                    }
                    else
                        return NotFound();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Editar(int? id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _clienteService.Editar(cliente);
                    var editar = await _clienteService.GuardarCambios();
                    if (editar)
                        return RedirectToAction("Index");
                    else
                        return NotFound();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            // si el modelo tiene errores en las validaciones
            ViewBag.Titulo = "Editar cliente";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoService.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id != null)
            {
                try
                {
                    _clienteService.Eliminar();
                    var eliminar = await _clienteService.GuardarCambios();
                    if (eliminar)
                        return RedirectToAction("Index");
                    else
                        return NotFound();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return NotFound();
        }
    }
}
