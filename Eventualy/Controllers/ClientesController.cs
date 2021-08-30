using Eventualy.Business.Abstract;
using Eventualy.Model.Entities;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.Titulo = "Clientes";
            var clientes = await _clienteService.ObtenerClientes();
            return View(clientes);
        }

        [HttpGet]
        [NoDirectAccess]
        public async Task<IActionResult> Crear()
        {         
            
            try
            {
                ViewBag.Titulo = "Crear cliente";
                ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoService.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
                return View();

            }
            catch (Exception)
            {
                return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
            }
            

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
                    {
                        //TempData["Accion"] = "Guardar";
                        //TempData["Mensaje"] = $"Se creó el usuario {cliente.Nombres}";
                        //return RedirectToAction("Index");
                        return Json(new { isValid = true, operacion = "crear" });
                    }
                    else
                        return Json(new { isValid = false, tipoError = "error", error = "Error al crear el registro" });

                }
                catch (Exception)
                {
                    return Json(new { isValid = false, tipoError = "error", error = "Error al crear el registro" });
                }
            }
            // si el modelo tiene errores en las validaciones
            ViewBag.Titulo = "Crear cliente";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoService.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", cliente) });
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
                return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
            if (ModelState.IsValid)
            {
                try
                {
                    _clienteService.Editar(cliente);
                    var editar = await _clienteService.GuardarCambios();
                    if (editar)
                        return Json(new { isValid = true, operacion = "editar" });
                    else
                        return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }
                catch (Exception)
                {

                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }
            }
            // si el modelo tiene errores en las validaciones
            ViewBag.Titulo = "Editar cliente";
            ViewBag.TiposDocumento = new SelectList(await _tipoDocumentoService.ObtenerTiposDocumento(), "TipoDocumentoId", "Nombre");
            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Editar", cliente) });
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id != null)
            {
                try
                {
                    var cliente = await _clienteService.ObtenerClientePorId(id);                   
                    _clienteService.Eliminar(cliente);
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
