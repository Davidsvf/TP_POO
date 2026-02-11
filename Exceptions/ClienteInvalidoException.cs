// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Exceção lançada quando um cliente é considerado inválido
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Exceção lançada quando um cliente não cumpre os requisitos de validade ou não existe no sistema.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ClienteInvalidoException:Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção  <see cref="ClienteInvalidoException"/> com uma mensagem padrão.
        /// </summary>
        public ClienteInvalidoException() : base("O cliente não é válido.")
        { 
        }

        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="ClienteInvalidoException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro.</param>
        public ClienteInvalidoException(string message) : base(message)
        {
        }
    }
}
