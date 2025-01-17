using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SegurosPacificoSA
{
    public partial class FrmBuscarEmpleados : Form
    {
        
        private Conexion _conexion = null;

        public FrmBuscarEmpleados()
        {
            InitializeComponent();
           
            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                Buscar(this.txtNombre.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Buscar(string pNombre)
        {
            try
            {
                
                this.dtgDatos.DataSource = _conexion.BuscarEmpleado(pNombre).Tables[0]; 
                this.dtgDatos.AutoResizeColumns(); 
                this.dtgDatos.ReadOnly = true; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Desea eliminar el empleado seleccionado?","Confirmar",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.dtgDatos.SelectedRows.Count > 0)
                    {

                        EliminarEmpleado(this.dtgDatos.SelectedRows[0].Cells["Cedula"].Value.ToString());
                    }
                    else
                    {   
                        throw new Exception("Seleccione la fila del mpleado que desea eliminar");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarEmpleado(string cedula)
        {
            try
            {
               
                _conexion.EliminarEmpleado(cedula);

                
                Buscar("");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                
                MostrarMantEmpleados(0);
                Buscar(""); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarMantEmpleados(int funcion)
        {
            try
            {
                FrmMantEmpleados formulario = new FrmMantEmpleados();
                
                
                formulario.FuncionRealizar(funcion);
                if (funcion == 1)
                {
                    if (this.dtgDatos.SelectedRows.Count > 0)
                    {
                        
                        formulario.CargarEmpleado(EmpleadoSeleccionado());
                    }
                    else
                    {
                        
                        throw new Exception("Seleccione la fila del empleado que desea editar los datos..");
                    }
                
                }

                formulario.ShowDialog();
                formulario.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1 representa el proceso de modificar los datos
                MostrarMantEmpleados(1);
                Buscar(""); //se actualiza la lista despues de registrar un empleado
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Empleado EmpleadoSeleccionado()
        {
            try
            {
                Empleado temp = null;

                
                if (this.dtgDatos.SelectedRows.Count > 0)
                {
                    
                    temp = new Empleado();

                    
                   
                    temp.Cedula = this.dtgDatos.SelectedRows[0].Cells["Cedula"].Value.ToString();
                    temp.NombreCompleto = this.dtgDatos.SelectedRows[0].Cells["NombreCompleto"].Value.ToString();
                    temp.Departamento = this.dtgDatos.SelectedRows[0].Cells["Departamento"].Value.ToString();
                    temp.HorasNormales = int.Parse(this.dtgDatos.SelectedRows[0].Cells["HorasNormales"].Value.ToString());
                    temp.HorasExtras = int.Parse(this.dtgDatos.SelectedRows[0].Cells["HorasExtras"].Value.ToString());
                    temp.SalarioBruto = decimal.Parse(this.dtgDatos.SelectedRows[0].Cells["SalarioBruto"].Value.ToString());
                    temp.Deducciones = decimal.Parse(this.dtgDatos.SelectedRows[0].Cells["Deducciones"].Value.ToString());
                    temp.SalarioNeto = decimal.Parse(this.dtgDatos.SelectedRows[0].Cells["SalarioNeto"].Value.ToString());
                }
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }//cierre formulario
} //cierre namespaces
