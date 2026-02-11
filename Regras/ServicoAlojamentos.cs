// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Regras de negócio para a gestão dos Alojamentos
// -------------------------------------------------

using System;
using System.Collections.Generic;
using Dados;
using BO;
using Exceptions;

namespace Regras
{
    /// <summary>
    /// Fornece serviços e regras de negócio para a gestão de alojamentos.
    /// Atua como intermediário entre a interface e a camada de dados <see cref="Dados.Alojamentos"/>.
    /// </summary>
    public class ServicoAlojamentos
    {

        #region OtherMethods

        /// <summary>
        /// Insere um novo alojamento no sistema após validar se o objeto é válido e se não há duplicados.
        /// </summary>
        /// <param name="alojamento">A instância de <see cref="Alojamento"/> a inserir.</param>
        /// <returns><c>true</c> se a operação for concluída com sucesso.</returns>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada se o objeto fornecido for nulo.</exception>
        /// <exception cref="Exceptions.AlojamentoDuplicadoException">Lançada se já existir um alojamento com o mesmo nome.</exception>
        public static bool InserirAlojamento(Alojamento alojamento)
        {
            if (alojamento == null)
                throw new AlojamentoInvalidoException("O alojamento não existe.");

                
            if(VerificarAlojamentoDuplicado(alojamento.Nome)) 
                throw new AlojamentoDuplicadoException($"Já existe um alojamento com o nome '{alojamento.Nome}.");
   
            return Alojamentos.InserirAlojamento(alojamento);
        }

        /// <summary>
        /// Remove um alojamento do sistema através do seu nome único.
        /// </summary>
        /// <param name="nome">O nome do alojamento a remover.</param>
        /// <returns><c>true</c> se for removido com sucesso.</returns>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada se o nome for inválido ou o alojamento não for encontrado.</exception>
        public static bool RemoverAlojamento(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new AlojamentoInvalidoException("Nome do alojamento não pode ser vazio.");

            Alojamento alojamento = Alojamentos.ProcurarAlojamentoPorNome(nome);
            if (alojamento == null)
                throw new AlojamentoInvalidoException("O alojamento não existe.");

            return Alojamentos.RemoverAlojamento(alojamento);
        }

        /// <summary>
        /// Obtém todos os alojamentos que têm o estado de disponibilidade definido como verdadeiro.
        /// </summary>
        /// <returns>Uma lista de alojamentos disponíveis, ordenada por nome (critério padrão).</returns>
        public static List<Alojamento> ListarAlojamentosDisponiveis()
        {
            List<Alojamento> lista = new List<Alojamento>();
            List<Alojamento> todos = Alojamentos.ListarAlojamentos();

            if(todos == null) return lista;
            
            foreach(Alojamento alojamento in todos)
            {
                if (alojamento.Disponivel)
                {
                    lista.Add(alojamento);
                }
            }

            if (lista != null)
            {
                lista.Sort();
            }
            
            return lista;
        }

        /// <summary>
        /// Filtra os alojamentos pelo nome da classe (ex: "Casa", "Apartamento", "Hotel").
        /// </summary>
        /// <param name="tipo">O nome do tipo de alojamento.</param>
        /// <returns>Lista de alojamentos que pertencem ao tipo especificado.</returns>
        /// <exception cref="AlojamentoInvalidoException">Lançada se o tipo fornecido for nulo ou vazio.</exception>
        public static List<Alojamento> ListarAlojamentosPorTipo(string tipo)
        { 
            if (string.IsNullOrWhiteSpace(tipo))
                throw new AlojamentoInvalidoException("Tipo de alojmaento não pode ser vazio.");

            List<Alojamento> lista = new List<Alojamento>();
            List<Alojamento> todos = Alojamentos.ListarAlojamentos();

            if (todos == null)
                return lista;

            foreach(Alojamento alojamento in todos)
            {
                if(alojamento.GetType().Name.ToLower() == tipo.ToLower())
                lista.Add(alojamento);
            }

            if(lista != null)
            {
                lista.Sort();
            }

            return lista;
        }

        /// <summary>
        /// Obtém a lista completa de alojamentos registados no sistema.
        /// </summary>
        /// <returns>Lista ordenada alfabeticamente por nome.</returns>
        public static List<Alojamento> ListarAlojamentos()
        {
            List<Alojamento> lista= Alojamentos.ListarAlojamentos();

            if(lista != null)
            {
                lista.Sort();

            }
            return lista;
        }

        /// <summary>
        /// Realiza uma pesquisa na base de dados pelo nome do alojamento.
        /// </summary>
        /// <param name="nome">Nome a procurar.</param>
        /// <returns>A instância de <see cref="Alojamento"/> ou <c>null</c>.</returns>
        /// <exception cref="AlojamentoInvalidoException">Lançada se o nome for inválido.</exception>
        public static Alojamento ProcurarAlojamentoPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new AlojamentoInvalidoException("Nome do alojamento não pode ser vazio.");

            return Alojamentos.ProcurarAlojamentoPorNome(nome);
        }

        /// <summary>
        /// Verifica se o nome indicado já está a ser utilizado por outro alojamento.
        /// </summary>
        /// <param name="nome">Nome a verificar.</param>
        /// <returns><c>true</c> se o nome já estiver registado.</returns>
        /// <exception cref="AlojamentoInvalidoException">Lançada se o nome for inválido.</exception>
        public static bool VerificarAlojamentoDuplicado(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new AlojamentoInvalidoException("Nome do alojamento não pode ser vazio.");

            List<Alojamento> todos = Alojamentos.ListarAlojamentos();

            if(todos == null) return false;

            foreach(Alojamento alojamento in todos)
            {
                if(alojamento.Nome==nome)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Obtém todos os alojamentos ordenados por valor crescente do preço por noite.
        /// </summary>
        /// <returns>Lista de alojamentos ordenada por preço.</returns>
        /// <exception cref="AlojamentoInvalidoException">Lançada se não existirem alojamentos registados.</exception>
        public static List<Alojamento> ListarAlojamentosOrdenadosPorPreco()
        {
            List<Alojamento> lista = Alojamentos.ListarAlojamentos();

            if (lista == null)
                throw new AlojamentoInvalidoException("Não há alojamentos.");

            lista.Sort(new OrdenarAlojamentosPorPreco());

            return lista;
        }

        /// <summary>
        /// Obtém todos os alojamentos ordenados alfabeticamente pela sua localização geográfica.
        /// </summary>
        /// <returns>Lista de alojamentos ordenada por localização.</returns>
        /// <exception cref="AlojamentoInvalidoException">Lançada se a lista estiver vazia.</exception>
        public static List<Alojamento> ListarAlojamentosPorLoc()
        {
            List<Alojamento> lista = Alojamentos.ListarAlojamentos();

            if(lista==null)
                throw new AlojamentoInvalidoException("Não há alojamentos.");

            lista.Sort(new OrdenarAlojamentosPorLoc());
            return lista;   
        }
        #endregion

      
    }
}
