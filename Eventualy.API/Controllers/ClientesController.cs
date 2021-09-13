using Eventualy.Business.Abstract;
using Eventualy.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventualy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ITipoDocumentoService _tipoDocumentoService;

        public ClientesController(IClienteService clienteService, ITipoDocumentoService tipoDocumentoService)
        {
            _clienteService = clienteService;
            _tipoDocumentoService = tipoDocumentoService;
        }
        //[HttpGet]
        //public async Task<IEnumerable<Cliente>> ObtenerClientes()
        //{
        //    try
        //    {
        //        var empleados = await _clienteService.ObtenerClientes();
        //        return empleados ;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetEmpleado(int id)
        {
            var empleado = await _clienteService.ObtenerClientePorId(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }
    }
    
    

}
