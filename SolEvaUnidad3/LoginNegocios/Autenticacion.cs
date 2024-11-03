using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LoginDatos;

namespace LoginNegocios
{
    // Clase que gestiona la autenticación de usuarios.
    public class Autenticacion
    {
        private AccesoDatos accesoDatos = new AccesoDatos();    // Acceso a los datos de usuario

        private int intentosFallidos = 0;                       // Contador de intentos fallidos
        private bool bloqueado = false;                         // Indica si el acceso está bloqueado


        public int ObtenerIntentosFallidos()                    // Obtener valor de intentos fallidos
        {
            return intentosFallidos;
        }

        public bool ObtenerEstadoBloqueado()                    // Obtener valor de estado bloqueado
        {
            return bloqueado;
        }

        // Autentica al usuario comparando el nombre de usuario y la contraseña proporcionados con los datos almacenados.
        public bool Autenticar(string username, string password)
        {
            // Si el acceso está bloqueado, se muestra un mensaje y se indica que la autenticación ha fallado
            if (bloqueado == true)
            {
                Console.WriteLine("El acceso está bloqueado.");
                return false;
            }

            // Obtener los datos de usuario
            // y comprobar si el nombre de usuario y la contraseña son correctos
            DatosUsuario datosUsuario = accesoDatos.ObtenerDatosUsuario(username);

            if (datosUsuario != null && password == datosUsuario.Password)
            {
                Console.WriteLine("Inicio de sesión exitoso.");
                return true;
            }
            else
            {
                // En caso que no correspondan las credenciales con los registros, se aumentará el contador de intentos fallidos.
                Console.WriteLine("Credenciales incorrectas.");
                Console.WriteLine(intentosFallidos +"\n");

                intentosFallidos = intentosFallidos + 1;

                // Si la cantidad de intentos fallidos es mayor o igual a 4, se procede a cambiar el estado "bloqueado" 
                if (intentosFallidos >= 4)
                {
                    bloqueado = true;
                    Console.WriteLine("Has excedido el número de intentos permitidos. Acceso bloqueado.");
                }

                return false;
            }
        }
    }
}
