// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Exceção lançada quando um alojamento duplicado é detetado
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Exceção lançada quando se tenta inserir um alojamento que já existe no sistema
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AlojamentoDuplicadoException:Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="AlojamentoDuplicadoException"/> com uma mensagem padrão.
        /// </summary>
        public AlojamentoDuplicadoException() : base("Já existe um alojamento com o mesmo nome.")
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="AlojamentoDuplicadoException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        public AlojamentoDuplicadoException(string message) : base(message)
        {
        }
    }
}
