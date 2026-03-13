// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 08/11/2025
// Description: Classe concreta que representa um apartamento
// -------------------------------------------------

using System;
using System.Security.Policy;
using Exceptions;

namespace BO
{

    /// <summary>
    /// Representa um apartamento disponível para alojamento turístico.
    /// Herda da classe base <see cref="Alojamento"/>.
    /// </summary>
    /// <seealso cref="BO.Alojamento" />
    [Serializable]
    public class Apartamento : Alojamento
    {
        #region Attributes

        int andar;
        bool temElevador;
        bool temVaranda;
        bool temAC;
        int numWC;

        #endregion

        #region Methods

        #region Constructors        

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Apartamento"/> com valores base e definições internas por defeito.
        /// </summary>
        /// <param name="nome">Nome do apartamento.</param>
        /// <param name="localizacao">Localização geográfica.</param>
        /// <param name="numQuartos">Número de quartos.</param>
        /// <param name="preco">Preço por noite.</param>
        public Apartamento(string nome, string localizacao, int numQuartos, decimal preco) : base(nome, localizacao, numQuartos, preco)
        {
            this.andar = 1;
            this.temElevador = false;
            this.temVaranda = false;
            this.temAC = false;
            this.numWC = 1;

        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Apartamento"/> com todas as características especificadas.
        /// </summary>
        /// <param name="nome">Nome do apartamento.</param>
        /// <param name="localizacao">Localização geográfica.</param>
        /// <param name="numQuartos">Número de quartos.</param>
        /// <param name="preco">Preço por noite.</param>
        /// <param name="andar">Andar onde se situa o apartamento.</param>
        /// <param name="temElevador">Indica se o edifício possui elevador.</param>
        /// <param name="temVaranda">Indica se o apartamento possui varanda.</param>
        /// <param name="temAc">Indica se o apartamento possui ar condicionado.</param>
        /// <param name="numWC">Número de casas de banho.</param>
        public Apartamento(string nome, string localizacao, int numQuartos, decimal preco, int andar, bool temElevador, bool temVaranda, bool temAc, int numWC) : base(nome, localizacao, numQuartos, preco)
        {
            Andar = andar;
            this.temElevador= temElevador;
            this.temVaranda= temVaranda;
            this.temAC= temAc;
            NumWC = numWC;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Obtém ou define o andar do apartamento.
        /// </summary>
        /// <value>O andar (mínimo 0, máximo 50).</value>
        /// <exception cref="AlojamentoInvalidoException">Lançada se o andar for negativo ou superior a 50.</exception>
        public int Andar
        {
            get { return this.andar; }
            set {
                if (value <= 0)
                    throw new AlojamentoInvalidoException($"O andar do apartamento não pode ser abaixo do 0. Valor fornecido: {value}");
                if (value > 50)
                    throw new AlojamentoInvalidoException($"Valor excessivo para andar de um apartamento. Valor fornecido: {value}");

                this.andar = value; 
            }
        }

        /// <summary>
        /// Obtém ou define se o apartamento tem acesso por elevador.
        /// </summary>
        /// <value><c>true</c> se tem elevador; caso contrário, <c>false</c>.</value>
        public bool TemElevador
        {
            get { return this.temElevador; }
            set { temElevador = value; }
        }

        /// <summary>
        /// Obtém ou define se o apartamento possui varanda.
        /// </summary>
        /// <value><c>true</c> se tem varanda; caso contrário, <c>false</c>.</value>
        public bool TemVaranda
        {
            get { return this.temVaranda;}
            set { temVaranda = value;}
        }

        /// <summary>
        /// Obtém ou define se o apartamento possui Ar Condicionado (AC).
        /// </summary>
        /// <value><c>true</c> se tem AC; caso contrário, <c>false</c>.</value>
        public bool TemAC
        {
            get { return this.temAC;}
            set { temAC = value;}
        }

        // <summary>
        /// Obtém ou define o número de casas de banho (WC).
        /// </summary>
        /// <value>Número de WC (mínimo 1, máximo 5).</value>
        /// <exception cref="AlojamentoInvalidoException">Lançada se o número de WC for inferior a 1 ou superior a 5.</exception>
        public int NumWC
        {
            get { return numWC; }
            set {
                if (value <= 0)
                    throw new AlojamentoInvalidoException($"O apartamento tem de ter pelo menos 1 casa de banho. Valor fornecido: {value}");
                if (value > 5)
                    throw new AlojamentoInvalidoException($"Número excessivo de casas de banho. Valor fornecido: {value}");

                numWC = value; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Converte a instância do apartamento numa string, incluindo as características específicas do edifício.
        /// </summary>
        /// <returns>Uma representação textual completa do apartamento.</returns>
        public override string ToString()
        {
            string caracteristicas = "";

            caracteristicas += $"{Andar} andar";

            if (TemElevador)
                caracteristicas += "Elevador, ";
            if (TemVaranda)
                caracteristicas += "Varanda, ";
            if (TemAC)
                caracteristicas += "Ar Condicionado, ";

            caracteristicas += $"{NumWC} casas de banho";

            return $"{base.ToString()} | Caracteristicas: {caracteristicas}";
        }
        #endregion

        #endregion
    }
}
