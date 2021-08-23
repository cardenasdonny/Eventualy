using Eventualy.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Abstract
{
    public interface ITipoDocumentoService
    {
        Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumento();
    }
}
