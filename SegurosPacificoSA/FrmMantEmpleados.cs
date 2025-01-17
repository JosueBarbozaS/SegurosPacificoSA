using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using DAL;


using System.Configuration;
using BLL;

namespace SegurosPacificoSA
{ 
    public partial class FrmMantEmpleados : Form
    {
        
        private Conexion _conexion = null;

        
        private Empleado _empleado = null;

        public FrmMantEmpleados()
        {
            InitializeComponent();

            _conexion = new Conexion(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);

            
            txtHorasN.TextChanged += CalcularSalario;
            txtHorasE.TextChanged += CalcularSalario;
            txtDeducciones.TextChanged += CalcularSalario;

           
            txtSalarioBruto.ReadOnly = true;
            txtSalarioNeto.ReadOnly = true;
            txtDeducciones.ReadOnly = true;
        }

        private void CalcularSalario(object sender, EventArgs e)
        {
            try
            {
                
                int horasNormales = int.Parse(txtHorasN.Text.Trim());
                int horasExtras = int.Parse(txtHorasE.Text.Trim());

                
                decimal salarioBruto = (horasNormales * 1800m) + (horasExtras * 1800m * 1.5m);
                txtSalarioBruto.Text = salarioBruto.ToString("0");

                
                decimal deducciones = CalcularDeducciones(salarioBruto);
                txtDeducciones.Text = deducciones.ToString("0");
                
                decimal salarioNeto = salarioBruto - deducciones;
                txtSalarioNeto.Text = salarioNeto.ToString("0");
            }
            catch (FormatException)
            {
                
                LimpiarSalarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular el salario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal CalcularDeducciones(decimal salarioBruto)
        {
            decimal deducciones = 0m;

            if (salarioBruto <= 250000)
            {
                deducciones = salarioBruto * 0.09m;
            }
            else if (salarioBruto > 250000 && salarioBruto <= 380000)
            {
                deducciones = salarioBruto * 0.12m;
            }
            else if (salarioBruto > 380000)
            {
                deducciones = salarioBruto * 0.15m;
            }

            return deducciones;
        }

        private void LimpiarSalarios()
        {
            txtSalarioBruto.Text = "";
            txtSalarioNeto.Text = "";
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

            private void btnAcciones_Click(object sender, EventArgs e)
            {
            try
            {

               
                if (  this.btnAcciones.Text.Equals("Modificar"))
                {
                    ModificarEmpleado();
                   
                    MessageBox.Show("Empleado editado correctamente..",
                        "Proceso aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    
                    GuardarEmpleado();

                    
                    //MessageBox.Show("Empleado registrado correctamente..",
                    //    "Proceso aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



                
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void GuardarEmpleado()
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtCedula.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                    string.IsNullOrWhiteSpace(comboBox1.Text) ||
                    string.IsNullOrWhiteSpace(txtDeducciones.Text) ||
                    string.IsNullOrWhiteSpace(txtHorasN.Text) ||
                    string.IsNullOrWhiteSpace(txtHorasE.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos obligatorios.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // 
                }

                _empleado = new Empleado();

                _empleado.Cedula = txtCedula.Text.Trim();
                _empleado.NombreCompleto = txtNombreCompleto.Text.Trim();
                _empleado.Departamento = comboBox1.Text.Trim();
                _empleado.HorasNormales = int.Parse(txtHorasN.Text.Trim());
                _empleado.HorasExtras = int.Parse(txtHorasE.Text.Trim());
                _empleado.Deducciones = decimal.Parse(txtDeducciones.Text.Trim());
                _empleado.SalarioBruto = decimal.Parse(txtSalarioBruto.Text.Trim());
                _empleado.SalarioNeto = decimal.Parse(txtSalarioNeto.Text.Trim());


                _conexion.GuardarEmpleado(_empleado);

                MessageBox.Show("Empleado registrado correctamente.", "Proceso aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void FuncionRealizar(int pFunc)
        {    
            if (pFunc == 1)
            {   
                this.btnAcciones.Text = "Modificar";
            }
        }

       

        public void CargarEmpleado(Empleado pTemp)
        {
            try
            {
                    
                this.txtCedula.Text = pTemp.Cedula;
                this.txtNombreCompleto.Text = pTemp.NombreCompleto;
                this.comboBox1.Text = pTemp.Departamento;
                this.txtDeducciones.Text = pTemp.Deducciones.ToString();
                this.txtHorasN.Text = pTemp.HorasNormales.ToString();
                this.txtHorasE.Text = pTemp.HorasExtras.ToString();
                this.txtSalarioBruto.Text = pTemp.SalarioBruto.ToString();
                this.txtSalarioNeto.Text = pTemp.SalarioNeto.ToString();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void ModificarEmpleado()
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtCedula.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                    string.IsNullOrWhiteSpace(comboBox1.Text) ||
                    string.IsNullOrWhiteSpace(txtDeducciones.Text) ||
                    string.IsNullOrWhiteSpace(txtHorasN.Text) ||
                    string.IsNullOrWhiteSpace(txtHorasE.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos obligatorios.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                _empleado = new Empleado();

                _empleado.Cedula = txtCedula.Text.Trim();
                _empleado.NombreCompleto = txtNombreCompleto.Text.Trim();
                _empleado.Departamento = comboBox1.Text.Trim();
                _empleado.HorasNormales = int.Parse(txtHorasN.Text.Trim());
                _empleado.HorasExtras = int.Parse(txtHorasE.Text.Trim());
                _empleado.Deducciones = decimal.Parse(txtDeducciones.Text.Trim());
                _empleado.SalarioBruto = decimal.Parse(txtSalarioBruto.Text.Trim());
                _empleado.SalarioNeto = decimal.Parse(txtSalarioNeto.Text.Trim());

                _conexion.ModificarEmpleado(_empleado);

                //MessageBox.Show("Empleado editado correctamente.", "Proceso aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }//cierre formulario
} //cierre namespace
