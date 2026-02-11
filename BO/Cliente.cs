// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 10/11/2025
// Description: Classe que representa um cliente
// -------------------------------------------------

using System;
using System.Linq;
using Exceptions;

namespace BO
{

    /// <summary>
    /// Representa um cliente no sistema de gestão de alojamento turístico.
    /// Contém validações rigorosas para NIF, contacto telefónico, email e idade mínima.
    /// </summary>
    /// <seealso cref="System.IComparable{BO.Cliente}" />
    [Serializable]
    public class Cliente : IComparable<Cliente>
    {
        #region Attributes

        string nome;
        string nif;
        string telemovel;
        string email;
        DateTime dataNasc;
        string morada;
        DateTime dataCriacao;


        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Cliente"/> com os dados obrigatórios.
        /// </summary>
        /// <param name="nome">Nome completo do cliente.</param>
        /// <param name="nif">Número de Identificação Fiscal (9 dígitos).</param>
        /// <param name="telemovel">Contacto telefónico (9 dígitos, iniciado por 9).</param>
        /// <param name="email">Endereço de correio eletrónico válido.</param>
        /// <param name="dataNasc">Data de nascimento (mínimo 18 anos).</param>
        public Cliente(string nome, string nif, string telemovel, string email, DateTime dataNasc)
        {
            this.dataCriacao = DateTime.Now;

            Nome = nome;
            Nif = nif;
            Telemovel = telemovel;
            Email = email;
            DataNasc = dataNasc;

            this.morada = "";

        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Cliente"/> incluindo a morada.
        /// </summary>
        /// <param name="nome">Nome completo do cliente.</param>
        /// <param name="nif">Número de Identificação Fiscal.</param>
        /// <param name="telemovel">Contacto telefónico.</param>
        /// <param name="email">Endereço de correio eletrónico.</param>
        /// <param name="dataNasc">Data de nascimento.</param>
        /// <param name="morada">Endereço de residência do cliente.</param>
        public Cliente(string nome, string nif, string telemovel, string email, DateTime dataNasc, string morada)
        {
            this.dataCriacao= DateTime.Now;

            Nome = nome;
            Nif = nif;
            Telemovel = telemovel;
            Email = email;
            DataNasc = dataNasc;

            this.morada = morada;
        }



        #endregion

        #region Properties

        /// <summary>
        /// Obtém ou define o nome do cliente.
        /// </summary>
        /// <value>O nome do cliente.</value>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o nome estiver vazio.</exception>
        public string Nome
        {
            get { return nome; }
            set 
            {
                if(string.IsNullOrEmpty(value))
                    throw new ClienteInvalidoException("Vazio.");

                nome = value; 
            }
        }

        /// <summary>
        /// Obtém ou define o NIF (Número de Identificação Fiscal).
        /// </summary>
        /// <value>String de 9 dígitos numéricos.</value>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se não tiver 9 dígitos ou contiver caracteres não numéricos.</exception>
        public string Nif
        {
            get { return nif; }
            set 
            {
                if(string.IsNullOrEmpty(value))
                    throw new ClienteInvalidoException("Vazio.");
                if (value.Length != 9)
                    throw new ClienteInvalidoException("Nif tem de conter 9 digitos.");
                if (!value.All(char.IsDigit))
                    throw new ClienteInvalidoException("Nif tem de conter apenas digitos.");

                nif = value; 
            }
        }

        /// <summary>
        /// Obtém ou define o número de telemóvel.
        /// </summary>
        /// <value>Número de telemóvel (Formato PT).</value>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se não começar por '9', tiver espaços ou formato inválido.</exception>
        public string Telemovel
        {
            get { return telemovel; }
            set 
            {
                if(string.IsNullOrEmpty(value))
                    throw new ClienteInvalidoException("Vazio.");
                if (value.Contains(" ") || value.Contains("\n"))
                    throw new ClienteInvalidoException("Número de telemovel não pode ter espaços.");
                if (value.Length != 9)
                    throw new ClienteInvalidoException("Número de telemovel tem de conter 9 digitos.");
                if (!value.All(char.IsDigit))
                    throw new ClienteInvalidoException("Número de telemovel tem de conter apenas digitos.");
                if (!value.StartsWith("9"))
                    throw new ClienteInvalidoException("Número de telomevel portugues deve começar com 9.");

                telemovel = value; 
            }
        }

        /// <summary>
        /// Obtém ou define o email, validando a presença de '@' único e domínio.
        /// O valor é automaticamente convertido para minúsculas.
        /// </summary>
        /// <value>Endereço de email formatado.</value>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o formato do email for inválido.</exception>
        public string Email
        {
            get { return email; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ClienteInvalidoException("Vazio.");
                if (value.Contains(" ") || value.Contains("\n"))
                    throw new ClienteInvalidoException("Email não pode ter espaços.");
                
                int primeiroArroba = value.IndexOf("@");
                int ultimoArroba = value.LastIndexOf("@");

                if (primeiroArroba != ultimoArroba)
                    throw new ClienteInvalidoException("O email deve conter somente um @.");
                if (primeiroArroba == 0 || primeiroArroba == value.Length - 1)
                    throw new ClienteInvalidoException("Email mal formatado.");

                bool temPontoDepoisArroba = false;

                for(int i = primeiroArroba +1; i<value.Length; i++)
                {
                    if (value[i] == '.')
                    {
                        temPontoDepoisArroba = true;
                        break;
                    }
                }

                if (!temPontoDepoisArroba)
                    throw new ClienteInvalidoException("Email tem de conter um '.' depois do @ (@exemplo.com).");

                email = value.ToLower(); 
            }
        }

        /// <summary>
        /// Obtém ou define a data de nascimento. Valida maioridade (18 anos).
        /// </summary>
        /// <value>Data de nascimento.</value>
        /// <exception cref="Exceptions.ClienteInvalidoException">Lançada se o cliente tiver menos de 18 ou mais de 120 anos.</exception>
        public DateTime DataNasc
        {
            get { return dataNasc; }
            set
            {
                if (value == null)
                    throw new ClienteInvalidoException("Data de nascimento obrigatória.");
                if (value > DateTime.Today)
                    throw new ClienteInvalidoException("Data de nascimento não pode ser no futuro.");

                int idade = CalcularIdade(value);

                if (idade < 18)
                    throw new ClienteInvalidoException("Cliente deve ter pelo menos 18 anos.");
                if (idade > 120)
                    throw new ClienteInvalidoException("Data de nascimento inválida.");

                dataNasc = value;
            }
        }

        /// <summary>
        /// Obtém a idade atual do cliente calculada com base na data de nascimento.
        /// </summary>
        public int Idade
        {
            get
            {
                if(dataNasc == null)
                    return 0;

                return CalcularIdade(DataNasc);
            }
        }

        /// <summary>
        /// Obtém ou define a morada do cliente.
        /// </summary>
        public string Morada
        {
            get { return morada; }
            set { morada = value; }
        }

        /// <summary>
        /// Obtém a data em que o cliente foi registado no sistema.
        /// </summary>
        public DateTime DataCriacao
        {
            get { return dataCriacao; }
            private set { dataCriacao = value; }
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Retorna uma string que representa o cliente atual.
        /// </summary>
        /// <returns>Dados principais do cliente e morada (se existir).</returns>
        public override string ToString()
        {
            string textoMorada = "";
            if (!string.IsNullOrEmpty(morada))
                textoMorada += $"| Morada: {Morada}";

            return $"Cliente: {Nome} | NIF: {Nif} | Telemovél: {Telemovel} | Email: {Email} | Idade: {Idade} anos {textoMorada}"; 
        }

        /// <summary>
        /// Determina se dois clientes são iguais com base no NIF.
        /// </summary>
        public override bool Equals(object obj)
        {
            if(obj==null || !(obj is Cliente other))
                return false;

            return nif == other.nif;
        }

        /// <summary>
        /// Retorna o código hash baseado no NIF.
        /// </summary>
        public override int GetHashCode()
        {
            return nif.GetHashCode();
        }

        #endregion

        #region OtherMethods

        /// <summary>
        /// Calcula a idade de uma pessoa baseada numa data fornecida.
        /// </summary>
        /// <param name="datanasc">A data de nascimento.</param>
        /// <returns>O número de anos completos.</returns>
        private int CalcularIdade(DateTime datanasc)
        {
            int idade = DateTime.Now.Year - datanasc.Year;

            if (DateTime.Today < datanasc.AddYears(idade))
                idade--;

            return idade;
        }

        /// <summary>
        /// Obtém um relatório detalhado dos dados do cliente.
        /// </summary>
        /// <returns>String com toda a informação do perfil.</returns>
        public string ObterInformacaoCompleta()
        {
            string info = $"===== INFORMAÇÃO DO CLIENTE =====\n";
            info += $"Nome: {Nome}\n";
            info += $"NIF: {Nif}\n";
            info += $"Telemóvel: {Telemovel}\n";
            info += $"Email: {Email}\n";

            if (dataNasc != null)
            {
                info += $"Data de Nascimento: {dataNasc:dd/MM/yyyy}\n";
                info += $"Idade: {Idade} anos\n";
            }

            if (!string.IsNullOrEmpty(morada))
                info += $"Morada: {Morada}\n";

            info += $"Data de Criação: {dataCriacao:dd/MM/yyyy HH:mm}\n";
            info += $"================================";

            return info;
        }

        /// <summary>
        /// Compara o cliente atual com outro por ordem alfabética de nome.
        /// </summary>
        /// <param name="other">O outro cliente a comparar.</param>
        /// <returns>Resultado da comparação.</returns>
        public int CompareTo(Cliente cliente)
        {
            if (cliente == null)
                return 1;

            return Nome.CompareTo(cliente.Nome);
        }


        #endregion


        #endregion
    }
}
