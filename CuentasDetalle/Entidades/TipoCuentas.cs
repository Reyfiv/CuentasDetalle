using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasDetalle.Entidades
{
    public class TipoCuentas
    {
        [Key]
        public int TipoId { get; set; }
        public string Descripcion { get; set; }

        public TipoCuentas()
        {
            TipoId = 0;
            Descripcion = string.Empty;
        }
    }
}
