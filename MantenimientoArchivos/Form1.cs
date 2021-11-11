using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; 

namespace MantenimientoArchivos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Limpiar()
        {
            txtCedula.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtNombre.Clear();
            txtSalario.Clear();
            txtCedula.Focus();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCedula.Text != "" && txtCorreo.Text != "" && txtDireccion.Text != "" && txtNombre.Text != "" && txtSalario.Text != "")

                try
                {


                    StreamWriter arch = null;
                    //OTRAS FORMAS DE CREAR LOS ARCHICOS
                    //arch= File.AppendText("Datos.txt");
                    //arch.WriteLine(txtCedula.Text);
                    // arch.WriteLine(txtNombre.Text);
                    //arch.WriteLine(txtCorreo.Text);
                    //arch.WriteLine(txtDireccion.Text);
                    //arch.WriteLine(txtSalario.Text);
                    //arch.Close();
                    //Limpiar();


                    arch = File.AppendText("Datos.txt"); //PONER LA RUTA CORRECTA 
                    arch.Write(txtCedula.Text + "#");
                    arch.Write(txtNombre.Text + "#");
                    arch.Write(txtCorreo.Text + "#");
                    arch.Write(txtDireccion.Text + "#");
                    arch.WriteLine(txtSalario.Text);
                    arch.Close();
                    Limpiar();
                    MessageBox.Show("Registro ingresado con exito");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            //finally //opcional
            //{

            //Limpiar();

            //}

            else
            {
                MessageBox.Show("Cédula requerida");
            }

        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader leer = null;
                leer = File.OpenText("Datos.txt");
                string linea = "";
                linea = leer.ReadLine();
                //Split lo que hace es separar el caracter separador y lo acomoda para imprimirlo en el messagebox en posicion hacia abajo [1] [2] [3]
                string[] p = linea.Split('#');
                txtCedula.Text = p[0];
                txtNombre.Text = p[1];
                txtDireccion.Text = p[2];
                txtCorreo.Text = p[3];
                txtSalario.Text = p[4];
                MessageBox.Show(linea);
                leer.Close(); //cerrar el archivito

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Boolean encontrado = false;
            try
            {
                StreamReader leer = null;
                leer = File.OpenText("Datos.txt");
                string linea = "";

                while ((linea = leer.ReadLine()) != null)
                {
                    string[] p = linea.Split('#');
                    if (txtCedula.Text == p[0])
                    {
                        txtNombre.Text = p[1];
                        txtDireccion.Text = p[2];
                        txtCorreo.Text = p[3];
                        txtSalario.Text = p[4];
                        encontrado = true;
                        btnModificar.Enabled = true;
                        btnBorrar.Enabled = true;

                    }
                }
                if (encontrado == false)
                {
                    MessageBox.Show("Registro no encontrado");
                    Limpiar();
                }

                leer.Close(); //cerrar el archivito

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

            try
            {
                StreamReader leer = null;
                StreamWriter arch = null; // se crea un nuevo archivo  

                leer = File.OpenText("Datos.txt"); //se abre para leer
                arch = File.AppendText("temp.txt");
                string linea = "";

                while ((linea = leer.ReadLine()) != null)
                {

                    string[] p = linea.Split('#');
                    if (txtCedula.Text != p[0])
                    {

                        arch.WriteLine(linea);
                    }
                  
                }
              
                arch.Close();
                leer.Close();
                File.Delete("Datos.txt");
                File.Copy("temp.txt", "Datos.txt", true);
                File.Delete("temp.txt");
                Limpiar();
                MessageBox.Show("Registro borrado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

        

         
    
}
