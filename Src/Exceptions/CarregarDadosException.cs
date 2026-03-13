// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 28/01/2026
// Description: Exceção personalizada para falhas no carregamento de dados
// -------------------------------------------------

using System;

namespace Exceptions
{
    /// <summary>
    /// Representa erros que ocorrem durante o processo de desserialização ou leitura 
    /// de dados a partir de ficheiros externos.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CarregarDadosException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CarregarDadosException"/> com uma mensagem de erro predefinida.
        /// </summary>
        public CarregarDadosException() : base("Erro ao carregar dados do ficheiro!")
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CarregarDadosException"/> com uma mensagem de erro personalizada.
        /// </summary>
        /// <param name="message">A mensagem que descreve o erro ocorrido.</param>
        public CarregarDadosException(string message) : base(message)
        {
        }
    }
}
