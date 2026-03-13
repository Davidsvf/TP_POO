// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Classe de acesso a dados para gestão de alojamentos    
// -------------------------------------------------

using System;
using System.Collections.Generic;
using BO;

namespace Dados
{
    /// <summary>
    /// Camada de dados que gere a coleção de alojamentos no sistema.
    /// Utiliza um dicionário estático onde a chave é o Nome do alojamento.
    /// </summary>
    public class Alojamentos
    {
        #region Attributes

        /// <summary>
        /// Dicionário estático que armazena os alojamentos, permitindo acesso rápido pelo nome.
        /// </summary>
        static Dictionary<string, Alojamento> alojamentos;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// Inicializa a classe estática <see cref="Alojamentos"/> e instancia o dicionário de suporte.
        /// </summary>
        static Alojamentos()
        {
            alojamentos = new Dictionary<string, Alojamento> ();
        }
        #endregion



        #region OtherMethods

        /// <summary>
        /// Insere um novo alojamento no sistema.
        /// Verifica se o objeto não é nulo e se o nome já não existe na coleção.
        /// </summary>
        /// <param name="alojamento">O objeto <see cref="Alojamento"/> a adicionar.</param>
        /// <returns>
        /// <c>true</c> se a inserção for bem-sucedida; 
        /// <c>false</c> se o alojamento for nulo ou se já existir um com o mesmo nome.
        /// </returns>
        public static bool InserirAlojamento(Alojamento alojamento)
        {
            if (alojamento == null || alojamentos.ContainsKey(alojamento.Nome))
                return false;

            alojamentos.Add(alojamento.Nome, alojamento);
            return true;
        }

        /// <summary>
        /// Remove um alojamento existente da coleção com base no seu nome.
        /// </summary>
        /// <param name="alojamento">O objeto <see cref="Alojamento"/> a remover.</param>
        /// <returns><c>true</c> se for removido com sucesso; <c>false</c> caso contrário.</returns>
        public static bool RemoverAlojamento(Alojamento alojamento)
        {
            if(alojamento == null) return false;
            alojamentos.Remove(alojamento.Nome);
            return true;
        }

        /// <summary>
        /// Obtém uma lista contendo todos os alojamentos registados.
        /// </summary>
        /// <returns>Uma nova <see cref="List{Alojamento}"/> com os valores atuais do dicionário.</returns>
        public static List<Alojamento> ListarAlojamentos()
        {
            return new List<Alojamento>(alojamentos.Values);
        }

        /// <summary>
        /// Procura um alojamento específico através do seu nome único.
        /// </summary>
        /// <param name="nome">O nome do alojamento a pesquisar.</param>
        /// <returns>A instância de <see cref="Alojamento"/> encontrada, ou <c>null</c> se não existir.</returns>
        public static Alojamento ProcurarAlojamentoPorNome(string nome)
        {
            if(string.IsNullOrEmpty(nome)) return null;

            if(alojamentos.ContainsKey (nome))
                return alojamentos[nome];

            return null;
        }

        /// <summary>
        /// Remove permanentemente todos os alojamentos da memória do sistema.
        /// </summary>
        public static void LimparTodosAlojamentos()
        {
            alojamentos.Clear();
        }

        #endregion

        #endregion
    }
}
