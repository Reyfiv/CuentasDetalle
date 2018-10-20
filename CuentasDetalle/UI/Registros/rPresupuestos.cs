using CuentasDetalle.DAL;
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
    public partial class rPresupuestos : Form
    {
        public List<PresupuestoDetalle> Detalle { get; set; }

        public rPresupuestos()
        {
            InitializeComponent();
            this.Detalle = new List<PresupuestoDetalle>();

            LlenaCombo();
        }

        private void CargarGrid()
        {
            MontoDataGridView.DataSource = null;
            MontoDataGridView.DataSource = this.Detalle;
        }

        private void Limpiar()
        {
            errorProvider1.Clear();

            IdNumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
            FechaDateTimePicker.Value = DateTime.Now;

            this.Detalle = new List<PresupuestoDetalle>();
            CargarGrid();
        }

        private Presupuesto LlenaClase()
        {
            Presupuesto presupuesto = new Presupuesto();
            presupuesto.PresupuestoId = Convert.ToInt32(IdNumericUpDown.Value);
            presupuesto.Descripcion = DescripcionTextBox.Text;
            presupuesto.Fecha = FechaDateTimePicker.Value;

            presupuesto.Monto = this.Detalle;

            return presupuesto;
        }

        private void LlenaCampos(Presupuesto presupuesto)
        {
            IdNumericUpDown.Value = presupuesto.PresupuestoId;
            DescripcionTextBox.Text = presupuesto.Descripcion;
            FechaDateTimePicker.Value = presupuesto.Fecha;

            this.Detalle = presupuesto.Monto;
            CargarGrid();
        }

        private bool Validar()
        {
            bool validar = false;
            errorProvider1.Clear();

            if (String.IsNullOrWhiteSpace(DescripcionTextBox.Text))
            {
                errorProvider1.SetError(DescripcionTextBox, "La descripcion esta vacia");
                validar = true;
            }

            if (this.Detalle.Count == 0)
            {
                errorProvider1.SetError(MontoDataGridView, "Debe agregar algun Monto");
                MontoTextBox.Focus();
                validar = true;
            }

            return validar;
        }

        private bool ValidarDetalle()
        {
            bool validarDetalle = false;
            errorProvider1.Clear();

            if (String.IsNullOrWhiteSpace(MontoTextBox.Text))
            {
                errorProvider1.SetError(MontoTextBox, "El Monto esta vacia");
                validarDetalle = true;
            }

            return validarDetalle;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Presupuesto presupuesto = BLL.PresupuestoBLL.Buscar((int)IdNumericUpDown.Value);
            return (presupuesto != null);
        }


        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }



        private void MasCuentasButton_Click(object sender, EventArgs e)
        {
            rCuentas rCuentas = new rCuentas();
            rCuentas.ShowDialog();
        }

        private void AgregarButton_Click(object sender, EventArgs e)
        {
            if (MontoDataGridView.DataSource != null)
                this.Detalle = (List<PresupuestoDetalle>)MontoDataGridView.DataSource;

            if (ValidarDetalle())
            {
                MessageBox.Show("Favor revisar todos los campos", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Detalle.Add(
                new PresupuestoDetalle(
                    id: 0,
                    presupuestoId: (int)IdNumericUpDown.Value,
                    cuentaId : CuentaComboBox.Text,
                    monto: (decimal)Convert.ToSingle(MontoTextBox.Text)
                    ));
            CargarGrid();
            MontoTextBox.Focus();
            MontoTextBox.Clear();
        }

        private void RemoverFilaButton_Click(object sender, EventArgs e)
        {
            if (MontoDataGridView.Rows.Count > 0 && MontoDataGridView.CurrentRow != null)
            {
                Detalle.RemoveAt(MontoDataGridView.CurrentRow.Index);

                CargarGrid();
            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Presupuesto presupuesto;
            bool paso = false;
            if (Validar())
            {
                MessageBox.Show("Favor revisar todos los campos", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            presupuesto = LlenaClase();

            if (IdNumericUpDown.Value == 0)
                paso = BLL.PresupuestoBLL.Guardar(presupuesto);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("El Vendedor no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = BLL.PresupuestoBLL.Guardar(presupuesto);
            }
            Limpiar();

            if (paso)
                MessageBox.Show("Guardado", "Con Exito!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo Guardar", "Error!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IdNumericUpDown.Value);
            if (BLL.PresupuestoBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar", "Fallido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IdNumericUpDown.Value);
            Presupuesto presupuesto = BLL.PresupuestoBLL.Buscar(id);
            if (presupuesto != null)
            {
                DescripcionTextBox.Text = presupuesto.Descripcion;
                FechaDateTimePicker.Value = presupuesto.Fecha;
                MontoDataGridView.DataSource = presupuesto.Monto;
            }
        }

        private void LlenaCombo()
        {   
            CuentaComboBox.DataSource = BLL.CuentasBLL.GetList(c => true);
            CuentaComboBox.ValueMember = "CuentaId";
            CuentaComboBox.DisplayMember = "Descripcion";
        }
    }
}
