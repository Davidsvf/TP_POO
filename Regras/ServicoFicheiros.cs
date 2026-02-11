// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 28/01/2026
// Description: Camada de serviço para persistência de dados
// -------------------------------------------------

using System;
using BO;
using Dados;
using Exceptions;

namespace Regras
{
    /// <summary>
    /// Fornece serviços de alto nível para a gestão de ficheiros do sistema.
    /// Intermedeia a comunicação entre a interface e a camada de persistência <see cref="Dados.Ficheiros"/>.
    /// </summary>
    public class ServicoFicheiros
    {
        /// <summary>
        /// Solicita a gravação de todos os dados do sistema num ficheiro binário.
        /// </summary>
        /// <param name="nomeficheiro">Caminho ou nome do ficheiro de destino.</param>
        /// <returns><c>true</c> se a gravação for bem-sucedida; <c>false</c> se o nome do ficheiro for inválido.</returns>
        /// <exception cref="GuardarDadosException">Lançada quando ocorre um erro crítico durante a escrita.</exception>
        public static bool GuardarDados(string nomeficheiro)
        {
            if (string.IsNullOrEmpty(nomeficheiro))
                return false;
            try
            {
                return Ficheiros.GuardarDados(nomeficheiro);
            }
            catch (Exception e)
            {
                throw new GuardarDadosException();
            }

        }

        /// <summary>
        /// Solicita o carregamento de dados a partir de um ficheiro binário para a memória.
        /// </summary>
        /// <param name="nomeficheiro">Caminho ou nome do ficheiro a carregar.</param>
        /// <returns><c>true</c> se o carregamento for bem-sucedido; <c>false</c> se o nome for inválido.</returns>
        /// <exception cref="CarregarDadosException">Lançada quando ocorre um erro crítico durante a leitura.</exception>
        public static bool CarregarDados(string nomeficheiro)
        {
            if(string.IsNullOrEmpty(nomeficheiro))
                return false;

            try
            {
                return Ficheiros.CarregarDados(nomeficheiro);
            }
            catch (Exception e)
            {
                throw new CarregarDadosException();
            }
        }
    }
}
