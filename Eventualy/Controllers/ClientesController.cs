using Eventualy.Business.Abstract;
using Eventualy.Business.Dtos.Clientes;
using Eventualy.Model.Entities;
using Helpers;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador, Usuario")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Titulo = "Clientes";
            var clientes = await _clienteService.ObtenerClientes();
            return View(clientes);
        }
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Crear(ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clienteService.Crear(clienteDto);
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
            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Crear", clienteDto) });
        }
        [Authorize(Roles = "Administrador, Usuario")]
        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id != null)
            {
                try
                {
                    var clienteDetalleDto = await _clienteService.ObtenerClienteDetalleDto(id.Value);
                    if (clienteDetalleDto != null)
                        return View(clienteDetalleDto);
                    else
                        return Json(new { isValid = false, tipoError = "error", mensaje = "Error al consultar el registro" });
                }
                catch (Exception)
                {
                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }

            }
            return Json(new { isValid = false, tipoError = "error", mensaje = "Error al consultar el registro" });
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                try
                {
                    var cliente = await _clienteService.ObtenerClienteDtoPorId(id.Value);
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
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Editar(int? id, ClienteDto clienteDto)
        {
            if (id != clienteDto.ClienteId)
                return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
            if (ModelState.IsValid)
            {
                try
                {
                    _clienteService.Editar(clienteDto);
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
            return Json(new { isValid = false, tipoError = "warning", error = "Debe diligenciar los campos requeridos", html = Helper.RenderRazorViewToString(this, "Editar", clienteDto) });
        }
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> CambiarEstado(int? id)
        {
            if (id != null)
            {
                try
                {
                    var cliente = await _clienteService.ObtenerClienteDtoPorId(id.Value);
                    if (cliente != null)
                    {
                        //cliente.Estado = !cliente.Estado;

                        if (cliente.Estado)
                            cliente.Estado = false;
                        else
                            cliente.Estado = true;
                        _clienteService.Editar(cliente);
                        var editar = await _clienteService.GuardarCambios();
                        if(editar)
                            return Json(new { isValid = true});


                    }
                         
                    else
                        return Json(new { isValid = false});
                }
                catch (Exception)
                {
                    return Json(new { isValid = false, tipoError = "error", mensaje = "Error interno" });
                }

            }
            return Json(new { isValid = false, tipoError = "error", mensaje = "Error al consultar el registro" });
        }

    }
}
