// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 10/11/2025
// Description: Classe que representa uma reserva
// -------------------------------------------------

using System;
using Exceptions;

namespace BO
{
    /// <summary>
    /// Representa uma reserva efetuada por um cliente para um determinado alojamento.
    /// Gere o período de estadia, o número de quartos e o estado do ciclo de vida da reserva.
    /// </summary>
    [Serializable]
    public class Reserva
    {
        #region Attributes
        
        int id;
        static int proximoId = 1;
        Cliente cliente;
        Alojamento alojamento;
        int numQuartos;
        DateTime dataCheckIn;
        DateTime dataCheckOut;
        EstadoReserva estado;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Reserva"/> associando um cliente a um alojamento.
        /// Por defeito, a reserva é criada com o estado <see cref="EstadoReserva.Pendente"/>.
        /// </summary>
        /// <param name="cliente">Cliente que efetua a reserva.</param>
        /// <param name="alojamento">Alojamento a ser reservado.</param>
        /// <param name="numQuartos">Número de quartos pretendidos.</param>
        /// <param name="dataCheckIn">Data de início da estadia.</param>
        /// <param name="dataCheckOut">Data de fim da estadia.</param>
        public Reserva(Cliente cliente, Alojamento alojamento, int numQuartos, DateTime dataCheckIn, DateTime dataCheckOut)
        {
            this.id = proximoId++;
            this.cliente = cliente;
            this.alojamento = alojamento;
            this.numQuartos = numQuartos;
            this.dataCheckIn = dataCheckIn;
            this.dataCheckOut = dataCheckOut;
        }

        #endregion

        #region Properties        

        /// <summary>
        /// Obtém o identificador único da reserva.
        /// </summary>
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        /// <summary>
        /// Obtém ou define o cliente associado à reserva.
        /// </summary>
        /// <value>Instância de <see cref="Cliente"/>.</value>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o cliente for nulo.</exception>
        public Cliente Cliente
        {
            get { return cliente; }
            set
            {
                if(value== null )
                    throw new ClienteInvalidoException();

                cliente = value;
            }
        }

        /// <summary>
        /// Obtém ou define o alojamento reservado.
        /// </summary>
        /// <value>Instância de <see cref="Alojamento"/>.</value>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada se o alojamento for nulo.</exception>
        public Alojamento Alojamento
        {
            get { return alojamento; }
            set
            {
                if(value== null )
                    throw new AlojamentoInvalidoException();

                alojamento = value;
            }
        }

        /// <summary>
        /// Obtém ou define o número de quartos reservados. 
        /// Valida se o número não excede a capacidade do alojamento.
        /// </summary>
        /// <value>Quantidade de quartos.</value>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada se o valor for inválido ou exceder o disponível.</exception>
        public int NumQuartos
        {
            get { return numQuartos; }
            set
            {
                if (value <= 0)
                    throw new AlojamentoInvalidoException("Número de quartos inválido.");
                if (alojamento != null && value > alojamento.NumQuartos)
                    throw new AlojamentoInvalidoException("Número de quartos excede os disponibilizados por parte do alojamento.");

                numQuartos = value;
            }
        }

        /// <summary>
        /// Obtém o número total de noites calculado entre o Check-in e o Check-out.
        /// </summary>
        public int Noites
        {
            get
            {
                return (dataCheckOut - DataCheckIn).Days;
            }
        }

        /// <summary>
        /// Obtém ou define a data de check-in.
        /// </summary>
        /// <value>Data de início da reserva.</value>
        /// <exception cref="Exceptions.DatasInvalidasException">Lançada se a data for anterior ao dia atual.</exception>
        public DateTime DataCheckIn
        {
            get { return dataCheckIn; }
            set
            {
                if (value.Date < DateTime.Today)
                    throw new DatasInvalidasException("Data de check-in não pode ser no passado.");

                dataCheckIn = value;
            }
        }

        /// <summary>
        /// Obtém ou define a data de check-out. Deve ser posterior à data de check-in.
        /// </summary>
        /// <value>Data de fim da reserva.</value>
        /// <exception cref="Exceptions.DatasInvalidasException">Lançada se a data for igual ou anterior ao check-in.</exception>
        public DateTime DataCheckOut
        {
            get{ return dataCheckOut; }
            set
            {
                if(value.Date > DataCheckIn.Date)
                    throw new DatasInvalidasException("Data de check-out deve ser depois data check-in");

                dataCheckOut = value;
            }
        }

        /// <summary>
        /// Obtém o estado atual da reserva (Pendente, Confirmada, Cancelada, Finalizada).
        /// </summary>
        public EstadoReserva Estado
        {
            get { return estado; }
            private set { estado = value; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Retorna uma representação textual detalhada da reserva.
        /// </summary>
        public override string ToString()
        {
            return $"Reserva: {Id} | Cliente: {Cliente.Nome} | NIF: {Cliente.Nif} | Alojamento: {Alojamento.Nome} | Check-In: {DataCheckIn}" +
                $" | Check-Out: {DataCheckOut} | Noites: {Noites} | Quartos: {NumQuartos} | Estado: {Estado}";
        }

        /// <summary>
        /// Compara se duas reservas são iguais através do seu Identificador Único (ID).
        /// </summary>
        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Reserva))
                return false;

            Reserva other = obj as Reserva;

            return this.Id == other.Id;
        }

        /// <summary>
        /// Retorna o HashCode baseado no ID da reserva.
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion

        #region OtherMethods

        /// <summary>
        /// Calcula o valor total da reserva com base no preço por noite do alojamento e quantidade de quartos.
        /// </summary>
        /// <returns>Valor total em decimal. Retorna 0 se o alojamento não estiver definido.</returns>
        public decimal CalcularValorTotal()
        {
            if(alojamento == null)
                return 0;

            return alojamento.PrecoPorNoite * NumQuartos * Noites;
        }

        /// <summary>
        /// Altera o estado da reserva para Confirmada.
        /// </summary>
        public void ConfirmarReserva()
        {
            Estado = EstadoReserva.Confirmada;
        }

        /// <summary>
        /// Altera o estado da reserva para Cancelada.
        /// </summary>
        public void CancelarReserva()
        {
            Estado = EstadoReserva.Cancelada;
        }

        /// <summary>
        /// Altera o estado da reserva para Finalizada.
        /// </summary>
        public void FinalizarReserva()
        {
            Estado = EstadoReserva.Finalizada;
        }
        #endregion

        #region Enum

        /// <summary>
        /// Enumeração dos estados possíveis de uma reserva.
        /// </summary>
        public enum EstadoReserva
        {
            Pendente,
            Confirmada,
            Cancelada,
            Finalizada
        }

        #endregion

        #endregion
    }
}
