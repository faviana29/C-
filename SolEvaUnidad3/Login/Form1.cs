using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginNegocios;

namespace Login
{
    public partial class Form1 : Form
    {
        private Autenticacion autenticacion;
        
        public Form1()
        {
            InitializeComponent();
            // Se instancia la clase junto a la generación del form
            autenticacion = new Autenticacion();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Vamos a obtenter los valores ingresados en campo de usuario y contraseña
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Validación de campo usuario vacíos
            if (username.Length == 0)
            {
                errorProvider1.SetError(textBox1, "Ingrese Nombre de Usuario");
            }
            else {
                errorProvider1.SetError(textBox1, "");
            }

            // Validación de campo contraseña vacía
            if (password.Length == 0)
            {
                errorProvider2.SetError(textBox2, "Ingrese Contraseña");
            }
            else
            {
                errorProvider2.SetError(textBox2, "");
            }

            // A modo de prueba, mostraremos los datos ingresados en el label3
            label3.Text = username +"-"+ password + "\n";

            // Realizar la autenticación
            // sólo si se ingresaron valores en los campos de usuario y password
            if (username.Length != 0 && password.Length != 0) {
               

                if (autenticacion.Autenticar(username, password))
                {
                    MessageBox.Show("Inicio de sesión exitoso.");
                    label3.Text = "Acceso permitido";
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas.");
                    int intentosRestantes = 4 - autenticacion.ObtenerIntentosFallidos();
                    label3.Text = $"Ingresa correctamente los datos. Te quedan {intentosRestantes} intentos antes de que se bloquee el acceso.";
                }

                if (autenticacion.ObtenerEstadoBloqueado())
                {
                    button1.Enabled = false; // Bloquear el botón si se exceden los intentos
                    label3.Text = "Has excedido el número de intentos permitidos. Acceso bloqueado.";
                }
            }
         


        }
    }
}
