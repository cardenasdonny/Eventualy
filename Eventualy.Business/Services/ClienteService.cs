using Eventualy.Business.Abstract;
using Eventualy.Business.Dtos.Clientes;
using Eventualy.DAL;
using Eventualy.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClienteResumenDto>> ObtenerClientes()
        {
            List<ClienteResumenDto> listaClienteResumenDto = new();
            var clientes = await _context.Clientes.Include(x => x.TiposDocumento).ToListAsync();
            clientes.ForEach(cliente =>
            {
                ClienteResumenDto clienteResumenDto = new()
                {
                    ClienteId = cliente.ClienteId,
                    Nombres = cliente.Nombres,
                    Documento = cliente.Documento,
                    Email = cliente.Email,
                    TipoDocumento = cliente.TiposDocumento.Nombre,
                    Estado = ObtenerEstadoNombre(cliente.Estado)
                };
                listaClienteResumenDto.Add(clienteResumenDto);
            });
            return listaClienteResumenDto;
        }

        private static string ObtenerEstadoNombre(bool estado)
        {
            if (estado)
                return "Habilitado";
            else
                return "Deshabilitado";
        }

        public void Crear(ClienteDto clienteDto)
        {
            if (clienteDto == null)
                throw new ArgumentNullException(nameof(clienteDto));
            clienteDto.Estado = true;

            Cliente cliente = new()
            {
                ClienteId = clienteDto.ClienteId,
                Nombres = clienteDto.Nombres,
                Documento = clienteDto.Documento,
                TipoDocumentoId = clienteDto.TipoDocumentoId.Value,
                Edad = clienteDto.Edad.Value,
                Email = clienteDto.Email,
                Estado = clienteDto.Estado,
                Cumpleanios = clienteDto.Cumpleanios
            };

            _context.Add(cliente);
        }
        public async Task<ClienteDetalleDto> ObtenerClienteDetalleDto(int? id)
        {            

            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var cliente = await _context.Clientes.Include(x => x.TiposDocumento).FirstOrDefaultAsync(x => x.ClienteId == id);
            if (cliente != null)
            {
                ClienteDetalleDto clienteResumenDto = new()
                {
                    
                    Nombres = cliente.Nombres,
                    Documento = cliente.Documento,
                    Email = cliente.Email,
                    TipoDocumento = cliente.TiposDocumento.Nombre,
                    Estado = ObtenerEstadoNombre(cliente.Estado),
                    Cumpleanios = cliente.Cumpleanios,
                    Edad = cliente.Edad
                };
                return clienteResumenDto;
            }
            return null;
        }

        public async Task<ClienteDto> ObtenerClienteDtoPorId(int? id)
        {

            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var cliente = await _context.Clientes.Include(x => x.TiposDocumento).FirstOrDefaultAsync(x => x.ClienteId == id);
            if (cliente != null)
            {
                ClienteDto clienteDto = new()
                {
                    ClienteId = cliente.ClienteId,
                    Nombres = cliente.Nombres,
                    Documento = cliente.Documento,
                    Email = cliente.Email,
                    TipoDocumentoId = cliente.TipoDocumentoId,
                    Estado = cliente.Estado,
                    Cumpleanios = cliente.Cumpleanios,
                    Edad = cliente.Edad
                };
                return clienteDto;
            }
            return null;
        }

        public async Task<Cliente> ObtenerClientePorId(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return await _context.Clientes.Include(x => x.TiposDocumento).FirstOrDefaultAsync(x => x.ClienteId == id);
        }

        public void Editar(ClienteDto clienteDto)
        {
            if (clienteDto == null)
                throw new ArgumentNullException(nameof(clienteDto));
            Cliente cliente = new()
            {
                ClienteId = clienteDto.ClienteId,
                Nombres = clienteDto.Nombres,
                Documento = clienteDto.Documento,
                TipoDocumentoId = clienteDto.TipoDocumentoId.Value,
                Edad = clienteDto.Edad.Value,
                Email = clienteDto.Email,
                Estado = clienteDto.Estado,
                Cumpleanios = clienteDto.Cumpleanios
            };
            _context.Update(cliente);
        }

        public void Eliminar(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));
            if (cliente != null)
                _context.Remove(cliente);

        }

        public async Task<bool> GuardarCambios()
        {
            return await _context.SaveChangesAsync() > 0;// si el resultado es mayor a 0, me devuelve un true (todo salió bien)
        }


    }
}
