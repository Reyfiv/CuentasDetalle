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
    public class TipoCuentasBLL
    {
        public static bool Guardar(TipoCuentas tipo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.TipoCuentas.Add(tipo) != null)
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

        public static bool Modificar(TipoCuentas tipo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(tipo).State = EntityState.Modified;
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
                TipoCuentas tipo = contexto.TipoCuentas.Find(id);
                contexto.TipoCuentas.Remove(tipo);

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


        public static TipoCuentas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            TipoCuentas tipo = new TipoCuentas();

            try
            {
                tipo = contexto.TipoCuentas.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return tipo;
        }

        public static List<TipoCuentas> GetList(Expression<Func<TipoCuentas, bool>> expression)
        {
            List<TipoCuentas> tipo = new List<TipoCuentas>();
            Contexto contexto = new Contexto();
            try
            {
                tipo = contexto.TipoCuentas.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return tipo;
        }
    }
}
