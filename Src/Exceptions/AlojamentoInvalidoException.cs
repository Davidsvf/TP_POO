// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Exceção lançada quando um alojamento é considerado inválido
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Exceção lançada quando um alojamento não cumpre os requisitos de validade ou não existe no sistema.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AlojamentoInvalidoException:Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da exceção  <see cref="AlojamentoInvalidoException"/> com uma mensagem padrão.
        /// </summary>
        public AlojamentoInvalidoException() : base("Alojamento não é válido.")
        { 
        }

        /// <summary>
        /// Inicializa uma nova instância da exceção <see cref="AlojamentoInvalidoException"/> com uma mensagem personalizada.
        /// </summary>
        /// <param name="mensagem">Mensagem de erro.</param>
        public AlojamentoInvalidoException(string mensagem) : base(mensagem)
        {
        }
    }
}
