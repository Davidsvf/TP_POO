// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Exceção lançada quando uma reserva é inválida
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Exceção lançada quando uma reserva é considerada inválida.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ReservaInvalidaException:Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção  <see cref="ReservaInvalidaException"/> com uma mensagem padrão.
        /// </summary>
        public ReservaInvalidaException() : base("A reserva não é válida")
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="ReservaInvalidaException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        public ReservaInvalidaException(string message) : base(message) 
        { 
        }
    }
}
