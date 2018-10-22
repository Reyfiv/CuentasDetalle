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
    public partial class rCuentas : Form
    {
        public static int paso = 0;
        

        public rCuentas()
        {
            InitializeComponent();
            //if (paso == 1)
               // LlenaCombo();
        }

        private void LlenaCombo()
        {
            TipoComboBox.DataSource = BLL.TipoCuentasBLL.GetList(c => true);
            TipoComboBox.ValueMember = "TipoId";
            TipoComboBox.DisplayMember = "Descripcion";
        }

        private void Limpiar()
        {
            CuentaIdNumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
            MontoNumericUpDown.Value = 0;
        }

        private Cuentas LlenaClase()
        {
            Cuentas cuentas = new Cuentas();
            cuentas.CuentaId = Convert.ToInt32(CuentaIdNumericUpDown.Value);
            cuentas.Descripcion = DescripcionTextBox.Text;
            cuentas.TipoId = (int)TipoComboBox.SelectedValue;
            cuentas.Monto = Convert.ToDecimal(MontoNumericUpDown.Value);
            return cuentas;
        }

        private void LlenaCampo(Cuentas cuentas)
        {

            CuentaIdNumericUpDown.Value = cuentas.CuentaId;
            DescripcionTextBox.Text = cuentas.Descripcion;
            TipoComboBox.SelectedIndex = cuentas.TipoId;
            MontoNumericUpDown.Value = Convert.ToDecimal(cuentas.Monto);
        }


        private bool ExisteEnLaBaseDeDatos()
        {
            Cuentas cuenta = BLL.CuentasBLL.Buscar((int)CuentaIdNumericUpDown.Value);
            return (cuenta != null);
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
            Cuentas cuenta;
            bool paso = false;
            cuenta = LlenaClase();
            //if (Validar())
            //{
              //  MessageBox.Show("Favor revisar todos los campos", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
               // return;
            //}
            

            if (CuentaIdNumericUpDown.Value == 0)
                paso = BLL.CuentasBLL.Guardar(cuenta);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("El Vendedor no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = BLL.CuentasBLL.Guardar(cuenta);
            }
            Limpiar();

            if (paso)
                MessageBox.Show("Guardado", "Con Exito!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo Guardar", "Error!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(CuentaIdNumericUpDown.Value);
            if (BLL.CuentasBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar", "Fallido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BucarButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(CuentaIdNumericUpDown.Value);
            Cuentas cuenta = BLL.CuentasBLL.Buscar(id);
            if (cuenta != null)
            {
                DescripcionTextBox.Text = cuenta.Descripcion;
                MontoNumericUpDown.Value = cuenta.Monto;
            }
        }

        private void MasTipoCuentasButton_Click(object sender, EventArgs e)
        {
            rTipoCuentas rTipoCuentas = new rTipoCuentas();
            rTipoCuentas.ShowDialog();
            LlenaCombo();
        }

        private void rCuentas_Load(object sender, EventArgs e)
        {

        }
    }
}
