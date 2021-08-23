using Eventualy.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Abstract
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObtenerClientes();
        void Crear(Cliente cliente);
        Task<bool> GuardarCambios();
        Task<Cliente> ObtenerClientePorId(int? id);
        void Editar(Cliente cliente);
        Task Eliminar(int? id);
    }
}
