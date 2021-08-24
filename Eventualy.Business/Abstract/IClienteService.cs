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
        Task<IEnumerable<Cliente>> ObtenerClientes();
        void Crear(Cliente cliente);
        Task<bool> GuardarCambios();
        Task<Cliente> ObtenerClientePorId(int? id);
        void Editar(Cliente cliente);
        void Eliminar(Cliente cliente);
    }
}
