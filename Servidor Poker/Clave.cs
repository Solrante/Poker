namespace Servidor_Poker
{
    /// <summary>
    /// Contenedor de claves de comunicacion
    /// </summary>
    public class Clave
    {
        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        static public string Desconexion = "Desconexion";

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        static public string Sala = "Sala";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string Login = "Login";

        /// <summary>
        /// Separador en mensajes
        /// </summary>
        static public char Separador = '-';

        /// <summary>
        /// Separador en mensajes
        /// </summary>
        static public char SeparadorCredenciales = ',';

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string Valido = "Valido";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string Invalido = "Invalido";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string Carta = "Carta";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string Jugador = "Jugador";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string Crupier = "Crupier";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string FinMano = "Fin Mano";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string FinEnvio = "Fin envio";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Ficha = "Ficha";

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
        public const string Volver = "Volver";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Saldo = "Saldo";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string ListaSalas = "Lista salas";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        public const string Valor = "Valor";

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
        public const string ComandoInvalido = "Comando Invalido";

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        public const string Registro = "Registro";

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        public const string SalaLlena = "Sala llena";

        /// <summary>
        /// Clave de comunicacion
        /// </summary>
        public const string SalaDisponible = "Sala disponible";

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string ValorJugador = Valor + Separador + Jugador + Separador;

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string ValorCrupier = Valor + Separador + Crupier + Separador;

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

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string CartaJugador = Carta + Separador + Jugador + Separador;

        /// <summary>
        /// Clave de comunicacion.
        /// </summary>
        static public string CartaCrupier = Carta + Separador + Crupier + Separador;

    }
}
