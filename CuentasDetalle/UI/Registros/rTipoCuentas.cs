using CuentasDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuentasDetalle.UI.Registros
{
    public partial class rTipoCuentas : Form
    {
        public rTipoCuentas()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            TipoIdNumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
        }

        private TipoCuentas LlenaClase()
        {
            TipoCuentas tipocuentas = new TipoCuentas();
            tipocuentas.TipoId = Convert.ToInt32(TipoIdNumericUpDown.Value);
            tipocuentas.Descripcion = DescripcionTextBox.Text;
            return tipocuentas;
        }

        private void LlenaCampo(TipoCuentas tipocuentas)
        {

            TipoIdNumericUpDown.Value = tipocuentas.TipoId;
            DescripcionTextBox.Text = tipocuentas.Descripcion;
           
        }


        private bool ExisteEnLaBaseDeDatos()
        {
            TipoCuentas tipocuenta = BLL.TipoCuentasBLL.Buscar((int)TipoIdNumericUpDown.Value);
            return (tipocuenta != null);
        }


        private bool Validar()
        {
            bool paso = true;
            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                errorProvider1.SetError(DescripcionTextBox, "La Descripcion esta Vacia");
                paso = false;
            }
            return paso;
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            TipoCuentas tipocuenta;
            bool paso = false;
            tipocuenta = LlenaClase();
           // if (Validar())
            //{
                //MessageBox.Show("Favor revisar todos los campos", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            //}


            if (TipoIdNumericUpDown.Value == 0)
                paso = BLL.TipoCuentasBLL.Guardar(tipocuenta);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("El Vendedor no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = BLL.TipoCuentasBLL.Guardar(tipocuenta);
            }
            Limpiar();

            if (paso)
                MessageBox.Show("Guardado", "Con Exito!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo Guardar", "Error!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TipoIdNumericUpDown.Value);
            if (BLL.TipoCuentasBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar", "Fallido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BucarButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TipoIdNumericUpDown.Value);
            TipoCuentas tipocuenta = BLL.TipoCuentasBLL.Buscar(id);
            if (tipocuenta != null)
            {
                DescripcionTextBox.Text = tipocuenta.Descripcion;
            }
        }
    }
}
