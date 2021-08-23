using Eventualy.WEB.Models.DAL;
using Eventualy.WEB.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Business
{
    public class ClienteService:IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cliente>> ObtenerClientes()
        {
            return await _context.Clientes.Where(x=>x.Estado==true).Include(x=>x.TiposDocumento).ToListAsync();
        }

        public void Crear(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));
            cliente.Estado = true;
            _context.Add(cliente);            
        }

        public async Task<Cliente> ObtenerClientePorId(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

           return await _context.Clientes.Include(x=>x.TiposDocumento).FirstOrDefaultAsync(x=>x.ClienteId==id);
        }

        public void Editar(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));           
            _context.Update(cliente);
        }

        public async Task Eliminar(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var cliente = await ObtenerClientePorId(id);
            if(cliente!=null)
                _context.Remove(cliente);
           
        }

        public async Task<bool> GuardarCambios()
        {
            return await _context.SaveChangesAsync() > 0;// si el resultado es mayor a 0, me devuelve un true (todo salió bien)
        }
      

    }
}
