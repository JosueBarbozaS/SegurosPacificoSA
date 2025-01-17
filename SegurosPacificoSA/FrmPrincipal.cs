using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegurosPacificoSA
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Está seguro de cerrar el sistema?","Confirmar",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                Environment.Exit(0);
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            
            notifyIcon1.ShowBalloonTip(25);

            
        }

     

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarBuscarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void MostrarBuscarEmpleados()
        {
            try
            {
                FrmBuscarEmpleados frm = new FrmBuscarEmpleados();
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarBuscarEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }//cierre formulario
} //cierre namespaces
