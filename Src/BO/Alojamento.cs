// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 08/11/2025
// Description: Classe abstrata base para tipos de alojamento
// -------------------------------------------------

using System;
using System.Security.Policy;
using Exceptions;



namespace BO
{

    /// <summary>
    /// Classe abstrata que representa um alojamento genérico.
    /// Serve de base para tipos específicos de alojamento, como Casa, Apartamento ou Hotel.
    /// </summary>
    /// <seealso cref="BO.IReservavel" />
    /// <seealso cref="System.IComparable&lt;BO.Alojamento&gt;" />
    [Serializable]
    public abstract class Alojamento : IReservavel, IComparable<Alojamento>
    {
        #region Attributes

        int id;
        static int proximoId = 1;
        string nome;
        string localizacao;
        int numQuartos;
        decimal precoPorNoite;
        bool disponivel;
        DateTime dataCriacao;
        DateTime dataUltimaAtualizacao;

        #endregion

        #region Methods

        #region Constructors                    
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Alojamento"/> com valores por defeito.
        /// Incrementa automaticamente o identificador único.
        /// </summary>
        protected Alojamento()
        {
            id = proximoId++;
            nome = "";
            localizacao = "";
            numQuartos = 0;
            precoPorNoite = 0;
            disponivel = true;
            dataCriacao= DateTime.Now;
            dataUltimaAtualizacao= DateTime.Now;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Alojamento"/> com parâmetros específicos.
        /// </summary>
        /// <param name="nome">Nome do alojamento.</param>
        /// <param name="localizacao">Localização geográfica do alojamento.</param>
        /// <param name="numQuartos">Número de quartos disponíveis.</param>
        /// <param name="preco">Preço cobrado por cada noite.</param>
        protected Alojamento(string nome, string localizacao, int numQuartos, decimal preco)
        {
            this.id = proximoId++;
            this.nome = nome;
            this.localizacao= localizacao;
            this.numQuartos = numQuartos;   
            this.precoPorNoite = preco;
            this.disponivel = true;
            this.dataCriacao= DateTime.Now;
            this.dataUltimaAtualizacao = DateTime.Now;
        }

        #endregion

        #region Properties     

        /// <summary>
        /// Obtém o identificador único do alojamento.
        /// </summary>
        /// <value>O ID numérico único.</value>
        public int Id
        {
            get { return id; }
            private set {
                 id = value; }
        }

        /// <summary>
        /// Obtém ou define o nome do alojamento. Atualiza a data de última atualização.
        /// </summary>
        /// <value>O nome descritivo do alojamento.</value>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada quando o nome é nulo ou vazio.</exception>
        public string Nome
        {
            get { return nome; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new AlojamentoInvalidoException("Nome inválido.");
                
                nome = value;
                dataUltimaAtualizacao = DateTime.Now;
            }
        }

        // <summary>
        /// Obtém ou define a localização do alojamento.
        /// </summary>
        /// <value>A morada ou cidade do alojamento.</value>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada quando a localização é nula ou vazia.</exception>
        public string Localizacao
        {
            get { return localizacao; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new AlojamentoInvalidoException("Localização inválida.");

                localizacao = value;
                dataUltimaAtualizacao = DateTime.Now;
            }
        }

        /// <summary>
        /// Obtém ou define o número de quartos.
        /// </summary>
        /// <value>Quantidade total de quartos.</value>
        /// <exception cref="Exceptions.AlojamentoInvalidoException">Lançada quando o número de quartos é inferior ou igual a zero.</exception>
        public int NumQuartos
        {
            get { return numQuartos; }
            set 
            {
                if (value <= 0)
                    throw new AlojamentoInvalidoException("Número de quartos deve ser maioe que zero.");

                numQuartos = value;
                dataUltimaAtualizacao = DateTime.Now;
            }
        }

        /// <summary>
        /// Obtém ou define o preço por noite do alojamento.
        /// </summary>
        /// <value>Preço por noite.</value>
        /// <exception cref="AlojamentoInvalidoException">Lançada quando o preço é inferior ou igual a zero.</exception>
        public decimal PrecoPorNoite
        {
            get { return precoPorNoite; }
            set 
            {
                if (value <= 0)
                    throw new AlojamentoInvalidoException("Preço por noite deve ser maior que zero.");

                precoPorNoite = value;
                dataUltimaAtualizacao = DateTime.Now;
            }
        }

        /// <summary>
        /// Obtém ou define o estado de disponibilidade do alojamento.
        /// </summary>
        /// <value><c>true</c> se disponível; caso contrário, <c>false</c>.</value>
        public bool Disponivel
        {
            get { return disponivel;}
            set { disponivel = value; } 
        }

        /// <summary>
        /// Obtém a data e hora em que o alojamento foi criado no sistema.
        /// </summary>
        public DateTime DataCriacao
        {
            get { return dataCriacao; }
            private set { dataCriacao = value; }
        }

        /// <summary>
        /// Obtém a data e hora da última alteração feita aos dados do alojamento.
        /// </summary>
        public DateTime DataUltimaAtualizacao
        {
            get { return dataUltimaAtualizacao; }
            private set { dataUltimaAtualizacao = value; }
        }

        #endregion

        #region Overrides        
        /// <summary>
        /// Converte a instância do alojamento numa representação textual legível.
        /// </summary>
        /// <returns>Uma string contendo os detalhes principais do alojamento.</returns>
        public override string ToString()
        {
            return $"{GetType().Name}: | {Nome} | {Localizacao} | {NumQuartos} quartos | {PrecoPorNoite}€/noite | Disponivel: {Disponivel}";
        }

        /// <summary>
        /// Determina se dois alojamentos são iguais com base no ID.
        /// </summary>
        /// <param name="obj">O objeto a comparar com a instância atual.</param>
        /// <returns><c>true</c> se forem do mesmo tipo e possuírem o mesmo ID; caso contrário, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
                return false;

            Alojamento other = obj as Alojamento;

            if(other == null) return false;

            return (id == other.id);
        }

        /// <summary>
        /// Serve como função de hash para o tipo Alojamento.
        /// </summary>
        /// <returns>Um código hash baseado no identificador único.</returns>
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        #endregion

        #region OtherMethods


        /// <summary>
        /// Compara a instância atual com outro alojamento, ordenando-os por nome.
        /// </summary>
        /// <param name="alojamento">O alojamento a comparar.</param>
        /// <returns>Um inteiro que indica a posição relativa na ordenação.</returns>
        public int CompareTo(Alojamento alojamento)
        {
            if(alojamento==null) return 1;
            return Nome.CompareTo(alojamento.Nome);
        }

        /// <summary>
        /// Calcula o custo total de uma estadia multiplicando as noites pelo preço unitário.
        /// </summary>
        /// <param name="noites">Número de noites da estadia.</param>
        /// <returns>Valor total da reserva.</returns>
        /// <exception cref="Exception">Lançada se o número de noites for inferior ou igual a zero.</exception>
        public decimal CalcularPrecoTotal(int noites)
        {
            if (noites <= 0)
                throw new Exception("Número de noites inválido.");
        
            return noites * PrecoPorNoite;
        }


        /// <summary>
        /// Altera o estado de disponibilidade do alojamento (ex: após uma reserva).
        /// </summary>
        /// <param name="estado">Novo estado (true = disponível, false = ocupado).</param>
        public void AlterarDisponibilidade(bool estado)
        {
            Disponivel = estado;
        }

        #endregion

        #endregion
    }
}
