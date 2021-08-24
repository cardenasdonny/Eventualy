using Eventualy.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventualy.Business.Abstract
{
    public interface ITipoDocumentoService
    {
        Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumento();
    }
}
