using Eventualy.Business.Dtos.Clientes;
using Eventualy.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Abstract
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResumenDto>> ObtenerClientes();
        Task<ClienteDetalleDto> ObtenerClienteDetalleDto(int? id);
        Task<ClienteDto> ObtenerClienteDtoPorId(int? id);
        void Crear(ClienteDto clienteDto);
        Task<bool> GuardarCambios();
        Task<Cliente> ObtenerClientePorId(int? id);
        void Editar(ClienteDto clienteDto);
        void Eliminar(Cliente cliente);
    }
}
