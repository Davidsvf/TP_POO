// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Exceção lançada quando datas fornecidas são inválidas
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Exceção lançada quando uma data fornecida é inválida
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class DatasInvalidasException:Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceçãoc<see cref="DatasInvalidasException"/> com uma mensagem padrão.
        /// </summary>
        public DatasInvalidasException() : base("As datas não são válidas")
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="DatasInvalidasException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        public DatasInvalidasException(string message) : base(message)
        {
        }
    }
}
