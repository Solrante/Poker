using System;
namespace Cliente_Poker
{
    /// <summary>
    /// Contenedor de claves de comunicacion
    /// </summary>
    public class Clave
    {
        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        public const string Desconexion = "Desconexion";

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        public const string Sala = "Sala";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Login = "Login";

        /// <summary>
        /// Separador en mensajes
        /// </summary>
        public const char Separador = '-';

        /// <summary>
        /// Separador en mensajes
        /// </summary>
        static public char SeparadorCredenciales = ',';

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Valido = "Valido";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Invalido = "Invalido";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Carta = "Carta";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Jugador = "Jugador";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Crupier = "Crupier";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string FinMano = "Fin Mano";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string FinEnvio = "Fin envio";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Volver = "Volver";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Ficha = "Ficha";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Retirarse = "Retirarse";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Doblar = "Doblar";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Plantarse = "Plantarse";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Pedir = "Pedir";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Saldo = "Saldo";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Valor = "Valor";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string ListaSalas = "Lista salas";

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        public const string Registro = "Registro";

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        static public string RegistroValido = Registro + Separador + Valido;

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        static public string RegistroInvalido = Registro + Separador + Invalido;

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string LoginValido = Login + Separador + Valido;

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string LoginInvalido = Login + Separador + Invalido;
    }
}
