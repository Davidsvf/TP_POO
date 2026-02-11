// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 08/11/2025
// Description: Classe concreta que representa uma casa de alojamento
// -------------------------------------------------

using System;
using Exceptions;

namespace BO
{

    /// <summary>
    /// Representa uma casa utilizada como alojamento turístico.
    /// Herda as características base da classe <see cref="Alojamento"/>.
    /// </summary>
    /// <seealso cref="BO.Alojamento" />
    [Serializable]
    public class Casa : Alojamento
    {
        #region Attributes

        bool temJardim;
        bool temPiscina;
        bool temGaragem;
        int numPisos;

        #endregion

        #region Methods

        #region Constructors        

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Casa"/> com valores por defeito.
        /// </summary>
        /// <param name="nome">Nome da casa.</param>
        /// <param name="localizacao">Localização geográfica.</param>
        /// <param name="numQuartos">Número de quartos disponíveis.</param>
        /// <param name="preco">Preço cobrado por noite.</param>
        public Casa(string nome, string localizacao, int numQuartos, decimal preco) : base(nome, localizacao, numQuartos, preco)
        {
            this.temGaragem = false;
            this.temPiscina = false;
            this.temJardim = false;
            this.numPisos = 1;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Casa"/> com características detalhadas.
        /// </summary>
        /// <param name="nome">Nome da casa.</param>
        /// <param name="localizacao">Localização geográfica.</param>
        /// <param name="numQuartos">Número de quartos disponíveis.</param>
        /// <param name="preco">Preço cobrado por noite.</param>
        /// <param name="temJardim">Indica se a casa possui jardim.</param>
        /// <param name="temPiscina">Indica se a casa possui piscina.</param>
        /// <param name="temGaragem">Indica se a casa possui garagem.</param>
        /// <param name="numPisos">Número total de pisos da habitação.</param>
        public Casa(string nome, string localizacao, int numQuartos, decimal preco, bool temJardim, bool temPiscina, bool temGaragem, int numPisos) : base(nome, localizacao, numQuartos, preco)
        {
            this.temJardim = temJardim;
            this.temPiscina = temPiscina;
            this.temGaragem = temGaragem;
            NumPisos = numPisos;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Obtém ou define se a casa possui um espaço de jardim.
        /// </summary>
        /// <value><c>true</c> se tem jardim; caso contrário, <c>false</c>.</value>
        public bool TemJardim
        {
            get { return this.temJardim; }
            set {  this.temJardim = value; }
        }

        /// <summary>
        /// Obtém ou define se a casa possui piscina privada ou partilhada.
        /// </summary>
        /// <value><c>true</c> se tem piscina; caso contrário, <c>false</c>.</value>
        public bool TemPiscina
        {
            get { return this.temPiscina; }
            set { this.temPiscina = value; }
        }

        /// <summary>
        /// Obtém ou define se a casa possui garagem ou lugar de estacionamento.
        /// </summary>
        /// <value><c>true</c> se tem garagem; caso contrário, <c>false</c>.</value>
        public bool TemGaragem
        {
            get { return this.temGaragem;}
            set {  this.temGaragem = value;}
        }

        /// <summary>
        /// Obtém ou define o número de pisos da casa.
        /// </summary>
        /// <value>Quantidade de pisos (mínimo 1, máximo 5).</value>
        /// <exception cref="AlojamentoInvalidoException">Lançada se o número de pisos for inferior a 1 ou superior a 5.</exception>
        public int NumPisos
        {
            get { return this.numPisos; }
            set
            {
                if (value <= 0)
                    throw new AlojamentoInvalidoException($"A casa deve ter pelo menos 1 piso. Valor fornecido: {value}");

                if (value > 5)
                    throw new AlojamentoInvalidoException($"Número excessivo de pisos para uma casa. Valor fornecido: {value}");

                this.numPisos = value;
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Converte a instância da casa numa representação textual, listando as suas amenidades.
        /// </summary>
        /// <returns>Uma string formatada com os dados base e características da casa.</returns>
        public override string ToString()
        {
            string caracteristicas = "";

            if (TemJardim)
                caracteristicas += "Jardim, ";
            if (TemGaragem)
                caracteristicas += "Garagem, ";
            if (TemPiscina)
                caracteristicas += "Piscina, ";

            caracteristicas += $"{NumPisos} pisos";

            return $"{base.ToString()} | Caracteristicas: {caracteristicas}";
        }
        #endregion

        #endregion
    }
}
