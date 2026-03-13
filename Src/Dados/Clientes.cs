// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Classe de acesso a dados para gestão de clientes
// -------------------------------------------------

using System;
using System.Collections.Generic;
using BO;

namespace Dados
{
    /// <summary>
    /// Camada de dados responsável pelo armazenamento e gestão da coleção de clientes.
    /// Utiliza o NIF (Número de Identificação Fiscal) como identificador único no dicionário.
    /// </summary>
    public class Clientes
    {
        #region Attributes

        /// <summary>
        /// Dicionário estático que armazena os clientes indexados pelo seu NIF.
        /// </summary>
        static Dictionary<string, Cliente> clientes;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// Inicializa a classe estática <see cref="Clientes"/> e instancia o dicionário de suporte.
        /// </summary>
        static Clientes()
        {
            clientes = new Dictionary<string, Cliente>();
        }
        #endregion

        #region OtherMethods

        /// <summary>
        /// Insere um novo cliente no sistema.
        /// Valida se o objeto não é nulo e se o NIF já não se encontra registado.
        /// </summary>
        /// <param name="cliente">O objeto <see cref="Cliente"/> a adicionar.</param>
        /// <returns>
        /// <c>true</c> se o cliente for inserido com sucesso; 
        /// <c>false</c> se o cliente for nulo ou se o NIF já existir.
        /// </returns>
        public static bool InserirCliente(Cliente cliente)
        {
            if(cliente == null || clientes.ContainsKey(cliente.Nif)) 
                return false;

            clientes.Add(cliente.Nif, cliente);
            return true;
        }

        /// <summary>
        /// Remove um cliente da coleção com base no seu NIF.
        /// </summary>
        /// <param name="cliente">O cliente a remover.</param>
        /// <returns><c>true</c> se a remoção for bem-sucedida; <c>false</c> caso contrário.</returns>
        public static bool RemoverCliente(Cliente cliente)
        {
            if(cliente==null) return false;

            clientes.Remove(cliente.Nif);
            return true;
        }

        /// <summary>
        /// Pesquisa um cliente no sistema através do seu NIF.
        /// </summary>
        /// <param name="nif">O Número de Identificação Fiscal a procurar.</param>
        /// <returns>A instância de <see cref="Cliente"/> encontrada ou <c>null</c> se não existir.</returns>
        public static Cliente ProcurarClientePorNif(string nif)
        {
            if(string.IsNullOrEmpty(nif)) return null;

            if(clientes.ContainsKey(nif))
                return clientes[nif];

            return null;
        }

        /// <summary>
        /// Obtém uma lista com todos os clientes atualmente registados no sistema.
        /// </summary>
        /// <returns>Uma nova <see cref="List{Cliente}"/> contendo todos os registos.</returns>
        public static List<Cliente> ListarClientes()
        {
            return new List<Cliente>(clientes.Values);    
        }

        /// <summary>
        /// Remove permanentemente todos os clientes da memória do sistema.
        /// </summary>
        public static void LimparTodosClientes()
        {
            clientes.Clear();
        }

        #endregion

        #endregion
    }
}
