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
    public class PresupuestoBLL
    {
        public static bool Guardar(Presupuesto presupuesto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if(contexto.Presupuesto.Add(presupuesto) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }
                contexto.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Modificar(Presupuesto presupuesto)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var Anterior = contexto.Presupuesto.Find(presupuesto.PresupuestoId);

                foreach(var item in Anterior.Monto)
                {
                    if (!presupuesto.Monto.Exists(d => d.Id == item.Id))
                        contexto.Entry(item).State = EntityState.Deleted;
                }
                contexto.Entry(presupuesto).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Presupuesto presupuesto = contexto.Presupuesto.Find(id);

                contexto.Presupuesto.Remove(presupuesto);

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

        public static Presupuesto Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Presupuesto presupuesto = new Presupuesto();
            try
            {
                presupuesto = contexto.Presupuesto.Find(id);
                presupuesto.Monto.Count();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return presupuesto;
        }

        public static List<Presupuesto> GetList(Expression<Func<Presupuesto, bool>> expression)
        {
            List<Presupuesto> presupuesto = new List<Presupuesto>();
            Contexto contexto = new Contexto();
            try
            {
                presupuesto = contexto.Presupuesto.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return presupuesto;
        }

    }
}
