// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 11/12/2025
// Description: Regras de negócio para a gestão das Reservas
// -------------------------------------------------

using System;
using System.Collections.Generic;
using Dados;
using BO;
using Exceptions;

namespace Regras
{
    /// <summary>
    /// Fornece serviços e lógica de negócio para a gestão de reservas.
    /// Inclui validações complexas de disponibilidade de quartos e períodos de estadia.
    /// </summary>
    public class ServicoReservas
    {
        #region OtherMethods

        /// <summary>
        /// Calcula o total de quartos já reservados para um alojamento específico num determinado intervalo de datas.
        /// </summary>
        /// <param name="a">O alojamento a verificar.</param>
        /// <param name="checkIn">Data de início pretendida.</param>
        /// <param name="checkOut">Data de fim pretendida.</param>
        /// <returns>O número total de quartos ocupados nesse período.</returns>
        static int ContarQuartosOcupados(Alojamento a, DateTime checkIn, DateTime checkOut)
        {
            int quartosReservados = 0;

            foreach (Reserva r in Reservas.ListarReservas())
            {
                if (r.Alojamento == a && checkIn.Date < checkOut.Date && checkOut.Date > checkIn.Date)
                {
                    quartosReservados += r.NumQuartos;
                }
            }

            return quartosReservados;
        }

        /// <summary>
        /// Valida se uma reserva pode ser efetuada com base nas datas e na capacidade do alojamento.
        /// </summary>
        /// <param name="reserva">A instância de <see cref="Reserva"/> a validar.</param>
        /// <returns><c>true</c> se houver disponibilidade total.</returns>
        /// <exception cref="Exceptions.ReservaInvalidaException">Lançada se a reserva for nula ou o número de quartos for inválido.</exception>
        /// <exception cref="Exceptions.DatasInvalidasException">Lançada se o check-in for no passado ou posterior ao check-out.</exception>
        /// <exception cref="Exceptions.AlojamentoIndesponivelException">Lançada se a ocupação máxima for excedida.</exception>
        public static bool VerificarDisponiblidade(Reserva reserva)
        {
            if (reserva == null) 
                throw new ReservaInvalidaException();

            if (reserva.DataCheckIn.Date < DateTime.Today || reserva.DataCheckOut.Date <= reserva.DataCheckIn.Date)
                throw new DatasInvalidasException();

            if(reserva.NumQuartos <= 0) 
                throw new ReservaInvalidaException("Numero de quartos inválido.");

            int quartosReservados = ContarQuartosOcupados(reserva.Alojamento, reserva.DataCheckIn, reserva.DataCheckOut);

            int quartosDisponiveis = reserva.Alojamento.NumQuartos - quartosReservados;

            if (quartosDisponiveis < reserva.NumQuartos) 
                throw new AlojamentoIndesponivelException($"Não há quartos sufecientes disponiveis. Apenas {quartosDisponiveis} quartos disponiveis.");

            return true;      
        }

        /// <summary>
        /// Efetua o registo de uma nova reserva após validação rigorosa de disponibilidade.
        /// </summary>
        /// <param name="reserva">Objeto <see cref="Reserva"/> a criar.</param>
        /// <returns><c>true</c> se a reserva for guardada com sucesso.</returns>
        public static bool CriarReserva(Reserva reserva)
        { 
            VerificarDisponiblidade(reserva);

            return Reservas.CriarReserva(reserva);
        }

        /// <summary>
        /// Remove uma reserva ativa do sistema.
        /// </summary>
        /// <param name="reserva">A reserva a anular.</param>
        /// <returns><c>true</c> se for cancelada com sucesso.</returns>
        /// <exception cref="Exceptions.ReservaInvalidaException">Lançada se a reserva não existir ou for nula.</exception>
        public static bool CancelarReserva(Reserva reserva)
        {
            if(reserva==null) 
                throw new ReservaInvalidaException("Reserva não popde ser nula.");

            if (!Reservas.ListarReservas().Contains(reserva))
                throw new ReservaInvalidaException("REserva não encontrada");

            return Reservas.CancelarReserva(reserva);
        }

        /// <summary>
        /// Obtém a listagem completa de reservas, organizada cronologicamente por data de Check-in.
        /// </summary>
        /// <returns>Lista de reservas ordenada.</returns>
        public static List<Reserva> ListarReservas()
        {
            List<Reserva> lista = Reservas.ListarReservas();

            if (lista != null)
            {
                lista.Sort(new OrdenarReservasPorData());
            }

            return lista;
        }

        /// <summary>
        /// Filtra as reservas associadas a um cliente específico via NIF.
        /// </summary>
        /// <param name="nif">NIF do cliente.</param>
        /// <returns>Lista de reservas filtrada e ordenada.</returns>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o NIF for nulo ou vazio.</exception>
        public static List<Reserva> ProcurarReservasPorCliente(string nif)
        {
            if (string.IsNullOrWhiteSpace(nif))
                throw new ClienteInvalidoException("Nif inválido.");

            return Reservas.ProcurarReservasPorCliente(nif);
        }

        /// <summary>
        /// Filtra as reservas registadas para um alojamento específico.
        /// </summary>
        /// <param name="nome">Nome único do alojamento.</param>
        /// <returns>Lista de reservas filtrada e ordenada.</returns>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada se o nome do alojamento for inválido.</exception>
        public static List<Reserva> ProcurarReservasPorAlojamento(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new AlojamentoInvalidoException("Nome inválido.");

            return Reservas.ProcurarReservasPorAlojamento(nome);
        }

        #endregion
    }
}
