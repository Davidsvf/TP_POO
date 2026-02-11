// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 28/01/2026
// Description: Exceção personalizada para falhas na gravação de dados
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Representa erros que ocorrem durante o processo de serialização ou gravação 
    /// de dados em suporte físico (ficheiros).
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class GuardarDadosException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GuardarDadosException"/> com uma mensagem de erro predefinida.
        /// </summary>
        public GuardarDadosException(): base("Erro ao guardar dados no ficheiro!")
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GuardarDadosException"/> com uma mensagem de erro personalizada.
        /// </summary>
        /// <param name="message">A mensagem que descreve o erro de gravação ocorrido.</param>
        public GuardarDadosException(string message): base(message) 
        { 
        }
        
    }
}
