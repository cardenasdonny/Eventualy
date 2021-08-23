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
    public class TipoDocumentoService: ITipoDocumentoService
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
