using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace DAL
{
    public class Conexion
    {
        private SqlConnection _connection;


        private SqlCommand _command;


        private SqlDataReader _reader;


        private string StringConexion;


        public Conexion(string pStrConexion)
        {
            StringConexion = pStrConexion;
        }




        public DataSet BuscarEmpleado(string pNombre)
        {
            try
            {
                DataSet datos = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();

                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Buscar_Empleados]";
                _command.Parameters.AddWithValue("@Nombre", pNombre);

                adapter.SelectCommand = _command;
                adapter.Fill(datos);

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                adapter.Dispose();


                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarEmpleado(string pCedula)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Del_Empleados]";
                _command.Parameters.AddWithValue("@Cedula", pCedula);
                _command.ExecuteNonQuery();

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void GuardarEmpleado(Empleado empleado)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Ins_Empleados]";
                _command.Parameters.AddWithValue("@Cedula", empleado.Cedula);
                _command.Parameters.AddWithValue("@NombreComp", empleado.NombreCompleto);
                _command.Parameters.AddWithValue("@Departamento", empleado.Departamento);
                _command.Parameters.AddWithValue("@HorasNormales", empleado.HorasNormales);
                _command.Parameters.AddWithValue("@HorasExtras", empleado.HorasExtras);
                _command.Parameters.AddWithValue("@SalarioBruto", empleado.SalarioBruto);
                _command.Parameters.AddWithValue("@Deducciones", empleado.Deducciones);
                _command.Parameters.AddWithValue("@SalarioNeto", empleado.SalarioNeto);

                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEmpleado(Empleado empleado)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Upd_Empleados]";
                _command.Parameters.AddWithValue("@Cedula", empleado.Cedula);
                _command.Parameters.AddWithValue("@NombreComp", empleado.NombreCompleto);
                _command.Parameters.AddWithValue("@Departamento", empleado.Departamento);
                _command.Parameters.AddWithValue("@HorasNormales", empleado.HorasNormales);
                _command.Parameters.AddWithValue("@HorasExtras", empleado.HorasExtras);
                _command.Parameters.AddWithValue("@SalarioBruto", empleado.SalarioBruto);
                _command.Parameters.AddWithValue("@Deducciones", empleado.Deducciones);
                _command.Parameters.AddWithValue("@SalarioNeto", empleado.SalarioNeto);

                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
