using CuentasDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Presupuesto> Presupuesto { get; set; }
        public DbSet<Cuentas> Cuentas { get; set; }
        public DbSet<TipoCuentas> TipoCuentas { get; set; }

        public Contexto() : base("ConStr")
        { }
    }
}
