// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 08/11/2025
// Description: Classe concreta que representa um hotel
// -------------------------------------------------

using System;
using Exceptions;

namespace BO
{

    /// <summary>
    /// Representa um hotel utilizado como alojamento turístico.
    /// Herda as propriedades base da classe <see cref="Alojamento"/>.
    /// </summary>
    /// <seealso cref="BO.Alojamento" />
    [Serializable]
    public class Hotel : Alojamento
    {
        #region Attributes

        int numEstrelas;
        bool temPiscina;
        bool temRestaurante;
        bool temSpa;
        bool temGinasio;
        bool temWifi;
        bool temEstacionamento;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Hotel"/> com valores por defeito (3 estrelas e Wifi).
        /// </summary>
        /// <param name="nome">Nome do hotel.</param>
        /// <param name="localizacao">Localização geográfica.</param>
        /// <param name="quartos">Número de quartos disponíveis.</param>
        /// <param name="preco">Preço por noite.</param>
        public Hotel(string nome, string localizacao, int quartos, decimal preco) : base(nome, localizacao, quartos, preco)
        {
            this.numEstrelas = 3;
            this.temPiscina = false;
            this.temRestaurante = false;
            this.temSpa = false;    
            this.temGinasio = false;
            this.temWifi = true;
            this.temEstacionamento = false;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Hotel"/> com todas as características especificadas.
        /// </summary>
        /// <param name="nome">Nome do hotel.</param>
        /// <param name="localizacao">Localização geográfica.</param>
        /// <param name="quartos">Número de quartos disponíveis.</param>
        /// <param name="preco">Preço por noite.</param>
        /// <param name="numEstrelas">Classificação em estrelas (1 a 5).</param>
        /// <param name="temPiscina">Indica se possui piscina.</param>
        /// <param name="temRestaurante">Indica se possui serviço de restauração.</param>
        /// <param name="temSpa">Indica se possui SPA.</param>
        /// <param name="temGinasio">Indica se possui ginásio.</param>
        /// <param name="temEstacionamento">Indica se possui estacionamento privativo.</param>
        public Hotel(string nome, string localizacao, int quartos, decimal preco, int numEstrelas, bool temPiscina, bool temRestaurante, bool temSpa, bool temGinasio, bool temEstacionamento) : base(nome, localizacao, quartos, preco)
        {
            NumEstrelas = numEstrelas;
            this.temPiscina=temPiscina;
            this.temRestaurante=temRestaurante;
            this.temSpa=temSpa;
            this.temGinasio=temGinasio;
            this.temWifi= true;
            this.temEstacionamento=temEstacionamento;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Obtém ou define a classificação do hotel em estrelas.
        /// </summary>
        /// <value>Número de estrelas (mínimo 1, máximo 5).</value>
        /// <exception cref="AlojamentoInvalidoException">Lançada se o valor estiver fora do intervalo [1,5].</exception>
        public int NumEstrelas
        {
            get { return numEstrelas; }
            set
            {
                if (value < 1 || value > 5)
                    throw new AlojamentoInvalidoException($"Número de estrelas deve estar entre 1 a 5 estrelas. Valor fornecido: {value}");

                numEstrelas = value;
            }
        }

        /// <summary>
        /// Obtém ou define se o hotel possui piscina.
        /// </summary>
        public bool TemPiscina
        {
            get { return this.temPiscina; }
            set { this.temPiscina = value; }
        }

        /// <summary>
        /// Obtém ou define se o hotel possui restaurante.
        /// </summary>
        public bool TemRestaurante
        {
            get { return this.temRestaurante;}
            set { temRestaurante= value; }
        }

        /// <summary>
        /// Obtém ou define se o hotel possui SPA.
        /// </summary>
        public bool TemSpa
        {
            get { return this.temSpa; }
            set { this.temSpa = value; }
        }

        /// <summary>
        /// Obtém ou define se o hotel possui ginásio.
        /// </summary>
        public bool TemGinasio
        {
            get { return this.temGinasio; }
            set { this.temGinasio = value;}
        }

        /// <summary>
        /// Obtém ou define se o hotel disponibiliza rede Wifi.
        /// </summary>
        public bool TemWifi
        {
            get { return this.temWifi; }
            set { this.temWifi = value; }
        }

        /// <summary>
        /// Obtém ou define se o hotel possui parque de estacionamento.
        /// </summary>
        public bool TemEstacionamento
        {
            get { return this.temEstacionamento; }
            set { temEstacionamento = value; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Retorna uma representação textual das características do hotel e os dados base do alojamento.
        /// </summary>
        /// <returns>String formatada com as estrelas e serviços disponíveis.</returns>
        public override string ToString()
        {
            string caracteristicas = "";

            caracteristicas += $"{NumEstrelas} estrelas, ";

            if (TemPiscina)
                caracteristicas += "Piscina, ";
            if (TemRestaurante)
                caracteristicas += "Restaurante, ";
            if (TemSpa)
                caracteristicas += "Spa, ";
            if (TemGinasio)
                caracteristicas += "Ginásio, ";
            if (TemWifi)
                caracteristicas += "Wifi, ";
            if (TemEstacionamento)
                caracteristicas += "Estacionamento";

            return $"{base.ToString()} | Caracterisitcas: {caracteristicas}";
        }
        #endregion

        #endregion
    }
}
