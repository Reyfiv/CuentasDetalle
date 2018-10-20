using CuentasDetalle.DAL;
using CuentasDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CuentasDetalle.BLL
{
    public class CuentasBLL
    {
        public static bool Guardar(Cuentas cuenta)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Cuentas.Add(cuenta) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Modificar(Cuentas cuenta)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(cuenta).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Cuentas cuenta = contexto.Cuentas.Find(id);

                contexto.Cuentas.Remove(cuenta);

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }


        public static Cuentas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Cuentas cuenta = new Cuentas();

            try
            {
                cuenta = contexto.Cuentas.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return cuenta;
        }

        public static List<Cuentas> GetList(Expression<Func<Cuentas, bool>> expression)
        {
            List<Cuentas> cuenta = new List<Cuentas>();
            Contexto contexto = new Contexto();
            try
            {
                cuenta = contexto.Cuentas.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return cuenta;
        }
    }
}
