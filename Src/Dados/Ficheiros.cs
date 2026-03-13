// -------------------------------------------------
// Author: David Faria
// Student: 31517
// Date: 22/12/2025
// Description: Persistencia dos dados num ficheiro binário
// -------------------------------------------------

using System;
using BO;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Exceptions;
using System.Linq.Expressions;

namespace Dados
{
    /// <summary>
    /// Classe responsável pela persistência de dados em suporte físico (ficheiros).
    /// Permite a serialização e desserialização das listas de alojamentos, clientes e reservas.
    /// </summary>
    public class Ficheiros
    {
        /// <summary>
        /// Guarda o estado atual das coleções (Alojamentos, Clientes e Reservas) num ficheiro binário.
        /// </summary>
        /// <param name="nomeficheiro">Caminho ou nome do ficheiro de destino.</param>
        /// <returns><c>true</c> se a operação for concluída com sucesso.</returns>
        /// <exception cref="ArgumentException">Lançada se o nome do ficheiro for nulo ou vazio.</exception>
        /// <exception cref="GuardarDadosException">Lançada em caso de erro de escrita ou falha na serialização.</exception>
        public static bool GuardarDados(string nomeficheiro)
        {
            if (string.IsNullOrEmpty(nomeficheiro))
                throw new ArgumentException("Nome de ficheiro não pode ser vazio.");

            Stream stream = null;

            try
            {
                stream = File.Open(nomeficheiro, FileMode.Create, FileAccess.Write);
                BinaryFormatter bw = new BinaryFormatter();

                bw.Serialize(stream, Alojamentos.ListarAlojamentos());
                bw.Serialize(stream, Clientes.ListarClientes());
                bw.Serialize(stream, Reservas.ListarReservas());

                return true;
            }
            catch(IOException ex)
            {
                throw new GuardarDadosException($"Erro de I/O ao gyardar ficheiro: {ex.Message}");
            }
            catch(Exception ex)
            {
                throw new GuardarDadosException($"Erro ao gauradr dados: {ex.Message}");
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        /// Carrega os dados de um ficheiro binário e repovoa as listas em memória.
        /// Este processo limpa os dados atuais antes de inserir os dados carregados.
        /// </summary>
        /// <param name="nomeficheiro">Caminho ou nome do ficheiro a ler.</param>
        /// <returns><c>true</c> se os dados forem carregados; <c>false</c> se o ficheiro não for encontrado.</returns>
        /// <exception cref="ArgumentException">Lançada se o nome do ficheiro for nulo ou vazio.</exception>
        /// <exception cref="CarregarDadosException">Lançada em caso de erro de leitura ou desserialização.</exception>
        public static bool CarregarDados(string nomeficheiro)
        {
            if (string.IsNullOrEmpty(nomeficheiro))
                throw new ArgumentException("Nome de ficheiro não pode ser vazio.");

            if (!File.Exists(nomeficheiro)) return false;

            Stream stream = null;

            try
            {
                stream = File.Open(nomeficheiro, FileMode.Open, FileAccess.Read);
                BinaryFormatter br = new BinaryFormatter();

                List<Alojamento> alojamentos = (List<Alojamento>)br.Deserialize(stream);
                List<Cliente> clientes = (List<Cliente>)br.Deserialize(stream);
                List<Reserva> reservas = (List<Reserva>)br.Deserialize(stream);

                Alojamentos.LimparTodosAlojamentos();
                Clientes.LimparTodosClientes();
                Reservas.LimparTodos();

                foreach(Alojamento alojamento in alojamentos) Alojamentos.InserirAlojamento(alojamento);
                foreach(Cliente cliente in clientes) Clientes.InserirCliente(cliente) ;
                foreach(Reserva reserva in reservas) Reservas.CriarReserva(reserva) ;

                return true;
            }
            catch(IOException ex) 
            { 
                throw new CarregarDadosException($"Erro de I/O ao carregar ficheiro: {ex.Message}"); 
            }
            catch(Exception ex)
            {
                throw new CarregarDadosException($"Erro ao carregar dados: {ex.Message}");
            }
            finally
            {
                stream.Close();
            }

        }   
        
    }
}
