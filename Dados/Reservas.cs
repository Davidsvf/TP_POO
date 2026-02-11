// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Classe de acesso a dados para gestão de reservas
// -------------------------------------------------

using System;
using System.Collections.Generic;
using BO;

namespace Dados
{
    /// <summary>
    /// Camada de dados que gere a coleção global de reservas no sistema.
    /// Permite a criação, cancelamento e filtragem de reservas por diversos critérios.
    /// </summary>
    public class Reservas
    {
        #region Attributes

        /// <summary>
        /// Lista estática que armazena todas as instâncias de <see cref="Reserva"/> em memória.
        /// </summary>
        static List<Reserva> reservas;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// Inicializa a classe estática <see cref="Reservas"/> e instancia a lista de suporte.
        /// </summary>
        static Reservas()
        {
            reservas = new List<Reserva>();
        }
        #endregion

        #region OtherMethods

        /// <summary>
        /// Adiciona uma nova reserva à coleção global.
        /// </summary>
        /// <param name="reserva">O objeto <see cref="Reserva"/> a adicionar.</param>
        /// <returns><c>true</c> se a reserva foi adicionada; <c>false</c> se o objeto for nulo.</returns>
        public static bool CriarReserva(Reserva reserva)
        {
            if(reserva == null) return false;
            reservas.Add(reserva);
            return true;
        }

        /// <summary>
        /// Remove uma reserva existente da lista (equivalente a anular o registo).
        /// </summary>
        /// <param name="reserva">A reserva a remover.</param>
        /// <returns><c>true</c> se a remoção for bem-sucedida; <c>false</c> caso contrário.</returns>
        public static bool CancelarReserva(Reserva reserva)
        {
            if(reserva == null) return false;
            reservas.Remove(reserva);
            return true;
        }

        /// <summary>
        /// Obtém uma cópia integral da lista de todas as reservas no sistema.
        /// </summary>
        /// <returns>Uma nova <see cref="List{Reserva}"/> com todos os registos.</returns>
        public static List<Reserva> ListarReservas()
        {
            return new List<Reserva>(reservas);
        }

        /// <summary>
        /// Filtra e devolve todas as reservas associadas a um cliente específico, ordenadas por data.
        /// </summary>
        /// <param name="nif">O NIF do cliente a pesquisar.</param>
        /// <returns>Lista de reservas do cliente, ordenada cronologicamente pelo Check-in.</returns>
        public static List<Reserva> ProcurarReservasPorCliente(string nif)
        {
            List<Reserva> x = new List<Reserva>();

            foreach (Reserva reserva in reservas)
            {
                if(reserva.Cliente.Nif == nif)
                x.Add(reserva);
            }

            x.Sort(new OrdenarReservasPorData());
            return x;
        }

        /// <summary>
        /// Filtra e devolve todas as reservas efetuadas para um alojamento específico, ordenadas por data.
        /// </summary>
        /// <param name="nome">O nome do alojamento a pesquisar.</param>
        /// <returns>Lista de reservas do alojamento, ordenada cronologicamente pelo Check-in.</returns>
        public static List<Reserva> ProcurarReservasPorAlojamento(string nome)
        {
            List<Reserva> y = new List<Reserva>();

            foreach (Reserva reserva in reservas)
            {
                if(reserva.Alojamento.Nome == nome)
                y.Add(reserva);
            }

            y.Sort(new OrdenarReservasPorData());

            return y;
        }

        /// <summary>
        /// Remove permanentemente todas as reservas da memória do sistema.
        /// </summary>
        public static void LimparTodos()
        {
            reservas.Clear();
        }

        #endregion

        #endregion
    }
}
