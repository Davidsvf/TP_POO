// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Regras de negócio para a gestão dos Clientes
// -------------------------------------------------

using System;
using System.Collections.Generic;
using BO;
using Dados;
using Exceptions;

namespace Regras
{
    /// <summary>
    /// Fornece serviços e lógica de negócio para a gestão de clientes.
    /// Intermedeia a comunicação entre a interface de utilizador e a camada de dados <see cref="Dados.Clientes"/>.
    /// </summary>
    public class ServicoClientes
    {

        #region OtherMethods

        /// <summary>
        /// Verifica se um cliente com o NIF indicado já se encontra registado no sistema.
        /// </summary>
        /// <param name="nif">Número de Identificação Fiscal a validar.</param>
        /// <returns><c>false</c> se o cliente não existir (indicando que o NIF está disponível).</returns>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o NIF for nulo ou apenas espaços.</exception>
        /// <exception cref="Exceptions.ClienteDuplicadoException">Lançada se já existir um registo com este NIF.</exception>
        public static bool VerificarClienteDuplicado(string nif)
        {
            if (string.IsNullOrWhiteSpace(nif))
                throw new ClienteInvalidoException("Nif não pode ser nulo.");
            if (Clientes.ProcurarClientePorNif(nif) != null) 
                throw new ClienteDuplicadoException($"Já existe um cliente com o NIF '{nif}'.");
                
            return false;
        }

        /// <summary>
        /// Valida e insere um novo cliente no sistema.
        /// </summary>
        /// <param name="cliente">Instância de <see cref="Cliente"/> com os dados preenchidos.</param>
        /// <returns><c>true</c> se a inserção na camada de dados for bem-sucedida.</returns>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o objeto cliente for nulo.</exception>
        /// <exception cref="Exceptions.ClienteDuplicadoException">Lançada se o NIF do cliente já estiver em uso.</exception>
        public static bool InserirCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new ClienteInvalidoException("Cliente não pode ser nulo.");

            VerificarClienteDuplicado(cliente.Nif);
                
            return Clientes.InserirCliente(cliente);
        }

        /// <summary>
        /// Remove um cliente do sistema através do seu NIF.
        /// </summary>
        /// <param name="nif">NIF do cliente a remover.</param>
        /// <returns><c>true</c> se o cliente for encontrado e removido com sucesso.</returns>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o NIF for inválido ou o cliente não existir.</exception>
        public static bool RemoverCliente(string nif)
        {
            if(string.IsNullOrWhiteSpace(nif))
                throw new ClienteInvalidoException("Nif não pode ser nulo.");

            Cliente cliente = Clientes.ProcurarClientePorNif(nif);

            if (cliente == null) 
                throw new ClienteInvalidoException("Cliente não existe.");

            return Clientes.RemoverCliente(cliente);
        }

        /// <summary>
        /// Procura um cliente específico na base de dados utilizando o NIF como chave.
        /// </summary>
        /// <param name="nif">NIF do cliente a pesquisar.</param>
        /// <returns>O objeto <see cref="Cliente"/> encontrado ou <c>null</c>.</returns>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o NIF fornecido for inválido.</exception>
        public static Cliente ProcurarClientePorNif(string nif)
        {
            if (string.IsNullOrWhiteSpace(nif))
                throw new Exception("Nif não pode ser nulo.");

            return Clientes.ProcurarClientePorNif(nif);
        }

        /// <summary>
        /// Obtém a lista completa de todos os clientes registados, ordenada alfabeticamente por nome.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="Cliente"/> ordenada.</returns>
        public static List<Cliente> ListarClientes()
        {
            List<Cliente> lista = Clientes.ListarClientes();

            if(lista != null)
            {
                lista.Sort();
            }

            return lista;
        }

        #endregion


    }
}
