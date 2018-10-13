using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasDetalle.Entidades
{
    public class Cuentas
    {
        public int CuentaId { get; set; }
        public string Descripcion { get; set; }
        public string TipoId { get; set; }
        public decimal Monto { get; set; }

        public Cuentas()
        {
            CuentaId = 0;
            Descripcion = string.Empty;
            TipoId = string.Empty;
            Monto = 0;
        }
    }
}
