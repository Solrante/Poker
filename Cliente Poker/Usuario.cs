using System;

namespace Cliente_Poker
{
    /// <summary>
    /// Definicion de usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Gets or sets la dirección de correo.
        /// </summary>
        /// <value>
        /// El correo.
        /// </value>
        public string Correo { set; get; }

        /// <summary>
        /// Gets or sets el saldo.
        /// </summary>
        /// <value>
        /// El saldo.
        /// </value>
        public double Saldo { set; get; }

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="Usuario"/>.
        /// </summary>
        /// <param name="datos">Credenciales del usuario.</param>
        public Usuario(string datos)
        {
            Console.WriteLine(datos);
            guardarDatos(datos);
        }

        /// <summary>
        /// Guarda en variables de clase información recogida por parametro.
        /// </summary>
        /// <param name="datos">Información recibida.</param>
        private void guardarDatos(string datos)
        {
            Correo = datos.Split(Clave.SeparadorCredenciales)[0];
            Saldo = Convert.ToDouble(datos.Split(Clave.SeparadorCredenciales)[1]);

        }
        /// <summary>
        /// Devuelve un <see cref="System.String" /> que representa esta instancia.
        /// </summary>
        /// <returns>
        /// Un <see cref="System.String" /> que presesenta esta instancia.
        /// </returns>
        public override string ToString()
        {
            return Correo + Clave.SeparadorCredenciales + Saldo;
        }
    }
}
