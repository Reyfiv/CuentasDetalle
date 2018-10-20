using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasDetalle.Entidades
{
    public class PresupuestoDetalle
    {
        [Key]
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
        public int CuentaId { get; set; }
        public decimal Monto { get; set; }

        public PresupuestoDetalle(int id, int presupuestoId, int cuentaId, decimal monto)
        {
            Id = id;
            PresupuestoId = presupuestoId;
            CuentaId = cuentaId;
            Monto = monto;
        }

        public PresupuestoDetalle()
        {
            Id = 0;
            PresupuestoId = 0;
            CuentaId = 0;
            Monto = 0;
        }
    }
}
