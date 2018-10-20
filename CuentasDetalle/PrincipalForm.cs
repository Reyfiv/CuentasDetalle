using CuentasDetalle.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuentasDetalle
{
    public partial class PrincipalForm : Form
    {
        public PrincipalForm()
        {
            InitializeComponent();
        }


        private void cuentasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            rPresupuestos rPresupuesto = new rPresupuestos();
            rPresupuesto.Show();
        }
    }
}
