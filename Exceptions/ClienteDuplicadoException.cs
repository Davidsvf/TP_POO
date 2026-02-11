// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 15/12/2025
// Description: Exceção lançada quando um cliente duplicado é detetado
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Exceção lançada quando se tenta inserir um cliente que já existe no sistema
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ClienteDuplicadoException:Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="ClienteDuplicadoException"/> com uma mensagem padrão.
        /// </summary>
        public ClienteDuplicadoException() : base("Já existe um cliente com o mesmo NIF.")
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="ClienteDuplicadoException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        public ClienteDuplicadoException(string message) : base(message)
        {
        }
    }
}
