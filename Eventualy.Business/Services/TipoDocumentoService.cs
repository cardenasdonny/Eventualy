using Eventualy.Business.Abstract;
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
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly AppDbContext _context;

        public TipoDocumentoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumento()
        {
            return await _context.TiposDocumento.ToListAsync();
        }
    }
}
