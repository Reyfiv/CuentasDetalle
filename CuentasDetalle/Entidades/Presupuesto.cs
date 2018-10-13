using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasDetalle.Entidades
{
    public class Presupuesto
    {
       [Key]
        public int PresupuestoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        public virtual List<PresupuestoDetalle> Monto { get; set; }

        public Presupuesto()
        {
            PresupuestoId = 0;
            Fecha = DateTime.Now;
            Descripcion = string.Empty;
            Monto = new List<PresupuestoDetalle>();
        }
    }
}
