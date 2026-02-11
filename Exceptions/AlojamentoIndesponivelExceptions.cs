// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Exceção lançada quando o alojamento não se encontra disponível
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Exceção lançada quando um alojamento não possui disponibilidade para efetuar uma reserva.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AlojamentoIndesponivelException:Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção  <see cref="AlojamentoIndesponivelExceptions"/> com uma mensagem padrão.
        /// </summary>
        public AlojamentoIndesponivelException() : base("Alojamento indesponivel.")
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="AlojamentoIndesponivelExceptions"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        public AlojamentoIndesponivelException(string message) : base(message)
        {
        }
    }
}
