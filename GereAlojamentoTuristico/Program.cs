//-------------------------------------------------
//Author: David Faria
// Student: 31517
// Date: 02 / 02 / 2026
// Description: Aplicação de gestão de alojamento turístico
// -------------------------------------------------

using System;
using System.Collections.Generic;
using BO;
using Regras;
using Exceptions;

namespace GestaoAlojamentoTuristico
{
    class Program
    {
        const string FICHEIRO_DADOS = "dados_sistema.bin";

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                MostrarMenuPrincipal();
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuAlojamentos();
                        break;
                    case "2":
                        MenuClientes();
                        break;
                    case "3":
                        MenuReservas();
                        break;
                    case "4":
                        GuardarDados();
                        break;
                    case "5":
                        CarregarDados();
                        break;
                    case "0":
                        sair = true;
                        Console.WriteLine("\nA encerrar o sistema...");
                        GuardarDados();
                        break;
                    default:
                        Console.WriteLine("\nOpcao invalida! Tente novamente.");
                        break;
                }

                if (!sair)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }

        }

        static void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("   SISTEMA DE GESTAO DE ALOJAMENTO TURISTICO");
            Console.WriteLine("=================================================");
            Console.WriteLine("\n1. Gestao de Alojamentos");
            Console.WriteLine("2. Gestao de Clientes");
            Console.WriteLine("3. Gestao de Reservas");
            Console.WriteLine("4. Guardar Dados");
            Console.WriteLine("5. Carregar Dados");
            Console.WriteLine("0. Sair");
            Console.Write("\nEscolha uma opcao: ");
        }

        #region CLIENTES

        static void MenuClientes()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== GESTAO DE CLIENTES ===\n");
                Console.WriteLine("1. Inserir Cliente");
                Console.WriteLine("2. Remover Cliente");
                Console.WriteLine("3. Listar Todos os Clientes");
                Console.WriteLine("4. Procurar Cliente por NIF");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha uma opcao: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        InserirCliente();
                        break;
                    case "2":
                        RemoverCliente();
                        break;
                    case "3":
                        ListarClientes();
                        break;
                    case "4":
                        ProcurarCliente();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcao invalida!");
                        break;
                }
                
                if (!voltar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
        static void InserirCliente()
        {
            Console.Clear();
            Console.WriteLine("=== INSERIR CLIENTE ===\n");

            try
            {
                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                Console.Write("NIF: ");
                string nif = Console.ReadLine();

                Console.Write("Telemovel: ");
                string telemovel = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Data de Nascimento (dd/MM/yyyy): ");
                DateTime dataNasc = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Console.Write("Morada (opcional, Enter para pular): ");
                string morada = Console.ReadLine();

                Cliente cliente;

                if (string.IsNullOrEmpty(morada))
                {
                    cliente = new Cliente(nome, nif, telemovel, email, dataNasc);
                }
                else
                {
                    cliente = new Cliente(nome, nif, telemovel, email, dataNasc, morada);
                }

                if (ServicoClientes.InserirCliente(cliente))
                {
                    Console.WriteLine("\nCliente inserido com sucesso!");
                }

            }
            catch (ClienteInvalidoException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (ClienteDuplicadoException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao inserir cliente: {ex.Message}");
            }
        }

        static void RemoverCliente()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVER CLIENTE ===\n");

            Console.Write("NIF do cliente a remover: ");
            string nif = Console.ReadLine();

            try
            {
                if (ServicoClientes.RemoverCliente(nif))
                {
                    Console.WriteLine("\nCliente removido com sucesso!");
                }

            }
            catch (ClienteInvalidoException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao remover cliente: {ex.Message}");
            }
        }

        static void ListarClientes()
        {
            Console.Clear();
            Console.WriteLine("=== TODOS OS CLIENTES ===\n");

            List<Cliente> clientes = ServicoClientes.ListarClientes();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente registado.");
            }
            else
            {
                foreach (var cliente in clientes)
                {
                    Console.WriteLine(cliente);
                }
                Console.WriteLine($"\nTotal: {clientes.Count} clientes");
            }
        }

        static void ProcurarCliente()
        {
            Console.Clear();
            Console.WriteLine("=== PROCURAR CLIENTE ===\n");

            Console.Write("NIF do cliente: ");
            string nif = Console.ReadLine();

            try
            {
                Cliente cliente = ServicoClientes.ProcurarClientePorNif(nif);

                if (cliente != null)
                {
                    Console.WriteLine("\nCliente encontrado:");
                    Console.WriteLine(cliente.ObterInformacaoCompleta());
                }
                else
                {
                    Console.WriteLine("\nCliente nao encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
        }

        #endregion#region ALOJAMENTOS

        #region  ALOJAMENTOS
        static void MenuAlojamentos()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== GESTAO DE ALOJAMENTOS ===\n");
                Console.WriteLine("1. Inserir Alojamento");
                Console.WriteLine("2. Remover Alojamento");
                Console.WriteLine("3. Listar Todos os Alojamentos");
                Console.WriteLine("4. Listar Alojamentos Disponiveis");
                Console.WriteLine("5. Procurar Alojamento por Nome");
                Console.WriteLine("6. Listar por Tipo");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha uma opcao: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        InserirAlojamento();
                        break;
                    case "2":
                        RemoverAlojamento();
                        break;
                    case "3":
                        ListarTodosAlojamentos();
                        break;
                    case "4":
                        ListarAlojamentosDisponiveis();
                        break;
                    case "5":
                        ProcurarAlojamento();
                        break;
                    case "6":
                        ListarPorTipo();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcao invalida!");
                        break;
                }

                if (!voltar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void InserirAlojamento()
        {
            Console.Clear();
            Console.WriteLine("=== INSERIR ALOJAMENTO ===\n");
            Console.WriteLine("Tipo de alojamento:");
            Console.WriteLine("1. Casa");
            Console.WriteLine("2. Apartamento");
            Console.WriteLine("3. Hotel");
            Console.Write("\nEscolha o tipo: ");

            string tipo = Console.ReadLine();

            try
            {
                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                Console.Write("Localizacao: ");
                string localizacao = Console.ReadLine();

                Console.Write("Numero de quartos: ");
                int quartos = int.Parse(Console.ReadLine());

                Console.Write("Preco por noite (EUR): ");
                decimal preco = decimal.Parse(Console.ReadLine());

                Alojamento alojamento = null;

                switch (tipo)
                {
                    case "1":
                        Console.Write("Tem jardim? (S/N): ");
                        bool jardim = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem piscina? (S/N): ");
                        bool piscina = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem garagem? (S/N): ");
                        bool garagem = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Numero de pisos: ");
                        int pisos = int.Parse(Console.ReadLine());

                        alojamento = new Casa(nome, localizacao, quartos, preco, jardim, piscina, garagem, pisos);
                        break;

                    case "2":
                        Console.Write("Andar: ");
                        int andar = int.Parse(Console.ReadLine());

                        Console.Write("Tem elevador? (S/N): ");
                        bool elevador = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem varanda? (S/N): ");
                        bool varanda = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem ar condicionado? (S/N): ");
                        bool ac = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Numero de casas de banho: ");
                        int wc = int.Parse(Console.ReadLine());

                        alojamento = new Apartamento(nome, localizacao, quartos, preco, andar, elevador, varanda, ac, wc);
                        break;

                    case "3":
                        Console.Write("Numero de estrelas (1-5): ");
                        int estrelas = int.Parse(Console.ReadLine());

                        Console.Write("Tem piscina? (S/N): ");
                        bool piscinaHotel = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem restaurante? (S/N): ");
                        bool restaurante = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem spa? (S/N): ");
                        bool spa = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem ginasio? (S/N): ");
                        bool ginasio = Console.ReadLine().ToUpper() == "S";

                        Console.Write("Tem estacionamento? (S/N): ");
                        bool estacionamento = Console.ReadLine().ToUpper() == "S";

                        alojamento = new Hotel(nome, localizacao, quartos, preco, estrelas, piscinaHotel, restaurante, spa, ginasio, estacionamento);
                        break;

                    default:
                        Console.WriteLine("\nTipo invalido!");
                        return;
                }

                if (ServicoAlojamentos.InserirAlojamento(alojamento))
                {
                    Console.WriteLine("\nAlojamento inserido com sucesso!");
                }
            }
            catch (AlojamentoInvalidoException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (AlojamentoDuplicadoException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao inserir alojamento: {ex.Message}");
            }
        }

        static void RemoverAlojamento()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVER ALOJAMENTO ===\n");

            Console.Write("Nome do alojamento a remover: ");
            string nome = Console.ReadLine();

            try
            {
                if (ServicoAlojamentos.RemoverAlojamento(nome))
                {
                    Console.WriteLine("\nAlojamento removido com sucesso!");
                }
            }
            catch (AlojamentoInvalidoException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao remover alojamento: {ex.Message}");
            }
        }

        static void ListarTodosAlojamentos()
        {
            Console.Clear();
            Console.WriteLine("=== TODOS OS ALOJAMENTOS ===\n");

            List<Alojamento> alojamentos = ServicoAlojamentos.ListarAlojamentos();

            if (alojamentos.Count == 0)
            {
                Console.WriteLine("Nenhum alojamento registado.");
            }
            else
            {
                foreach (var aloj in alojamentos)
                {
                    Console.WriteLine(aloj);
                    Console.WriteLine("\n");
                }
                Console.WriteLine($"\nTotal: {alojamentos.Count} alojamentos");
            }
        }

        static void ListarAlojamentosDisponiveis()
        {
            Console.Clear();
            Console.WriteLine("=== ALOJAMENTOS DISPONIVEIS ===\n");

            List<Alojamento> disponiveis = ServicoAlojamentos.ListarAlojamentosDisponiveis();

            if (disponiveis.Count == 0)
            {
                Console.WriteLine("Nenhum alojamento disponivel.");
            }
            else
            {
                foreach (var aloj in disponiveis)
                {
                    Console.WriteLine(aloj);
                    Console.WriteLine("\n");
                }
                Console.WriteLine($"\nTotal: {disponiveis.Count} alojamentos disponiveis");
            }
        }

        static void ProcurarAlojamento()
        {
            Console.Clear();
            Console.WriteLine("=== PROCURAR ALOJAMENTO ===\n");

            Console.Write("Nome do alojamento: ");
            string nome = Console.ReadLine();

            try
            {
                Alojamento aloj = ServicoAlojamentos.ProcurarAlojamentoPorNome(nome);

                if (aloj != null)
                {
                    Console.WriteLine("\nAlojamento encontrado:");
                    Console.WriteLine(aloj);
                }
                else
                {
                    Console.WriteLine("\nAlojamento nao encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
        }

        static void ListarPorTipo()
        {
            Console.Clear();
            Console.WriteLine("=== LISTAR POR TIPO ===\n");
            Console.WriteLine("1. Casas");
            Console.WriteLine("2. Apartamentos");
            Console.WriteLine("3. Hoteis");
            Console.Write("\nEscolha o tipo: ");

            string opcao = Console.ReadLine();
            string tipo = "";

            switch (opcao)
            {
                case "1":
                    tipo = "Casa";
                    break;
                case "2":
                    tipo = "Apartamento";
                    break;
                case "3":
                    tipo = "Hotel";
                    break;
                default:
                    Console.WriteLine("\nOpcao invalida!");
                    return;
            }

            try
            {
                List<Alojamento> alojamentos = ServicoAlojamentos.ListarAlojamentosPorTipo(tipo);

                if (alojamentos.Count == 0)
                {
                    Console.WriteLine($"\nNenhum(a) {tipo} registado(a).");
                }
                else
                {
                    Console.WriteLine($"\n=== {tipo.ToUpper()}S ===\n");
                    foreach (var aloj in alojamentos)
                    {
                        Console.WriteLine(aloj);
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine($"\nTotal: {alojamentos.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
        }

        #endregion

        #region RESERVAS

        static void MenuReservas()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("=== GESTAO DE RESERVAS ===\n");
                Console.WriteLine("1. Criar Reserva");
                Console.WriteLine("2. Cancelar Reserva");
                Console.WriteLine("3. Listar Todas as Reservas");
                Console.WriteLine("4. Listar Reservas por Cliente");
                Console.WriteLine("5. Listar Reservas por Alojamento");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha uma opcao: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CriarReserva();
                        break;
                    case "2":
                        CancelarReserva();
                        break;
                    case "3":
                        ListarReservas();
                        break;
                    case "4":
                        ListarReservasPorCliente();
                        break;
                    case "5":
                        ListarReservasPorAlojamento();
                        break;
                    case "0":
                        voltar = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcao invalida!");
                        break;
                }

                if (!voltar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void CriarReserva()
        {
            Console.Clear();
            Console.WriteLine("=== CRIAR RESERVA ===\n");

            try
            {
                Console.Write("NIF do cliente: ");
                string nif = Console.ReadLine();

                Cliente cliente = ServicoClientes.ProcurarClientePorNif(nif);
                if (cliente == null)
                {
                    Console.WriteLine("\nCliente nao encontrado!");
                    return;
                }

                Console.Write("Nome do alojamento: ");
                string nomeAloj = Console.ReadLine();

                Alojamento alojamento = ServicoAlojamentos.ProcurarAlojamentoPorNome(nomeAloj);
                if (alojamento == null)
                {
                    Console.WriteLine("\nAlojamento nao encontrado!");
                    return;
                }

                Console.Write("Numero de quartos: ");
                int quartos = int.Parse(Console.ReadLine());

                Console.Write("Data Check-In (dd/MM/yyyy): ");
                DateTime checkIn = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Console.Write("Data Check-Out (dd/MM/yyyy): ");
                DateTime checkOut = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Reserva reserva = new Reserva(cliente, alojamento, quartos, checkIn, checkOut);

                if (ServicoReservas.CriarReserva(reserva))
                {
                    Console.WriteLine("\nReserva criada com sucesso!");
                    Console.WriteLine($"Valor total: {reserva.CalcularValorTotal()}EUR");
                    Console.WriteLine($"Numero de noites: {reserva.Noites}");
                }
            }
            catch (DatasInvalidasException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (AlojamentoIndesponivelException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao criar reserva: {ex.Message}");
            }
        }

        static void CancelarReserva()
        {
            Console.Clear();
            Console.WriteLine("=== CANCELAR RESERVA ===\n");

            List<Reserva> reservas = ServicoReservas.ListarReservas();

            if (reservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva registada.");
                return;
            }

            foreach (var r in reservas)
            {
                Console.WriteLine(r);
            }

            Console.Write("\nID da reserva a cancelar: ");
            int id = int.Parse(Console.ReadLine());

            Reserva reserva = reservas.Find(r => r.Id == id);

            if (reserva != null)
            {
                try
                {
                    reserva.CancelarReserva();
                    Console.WriteLine("\nReserva cancelada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\nReserva nao encontrada.");
            }
        }

        static void ListarReservas()
        {
            Console.Clear();
            Console.WriteLine("=== TODAS AS RESERVAS ===\n");

            List<Reserva> reservas = ServicoReservas.ListarReservas();

            if (reservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva registada.");
            }
            else
            {
                foreach (var reserva in reservas)
                {
                    Console.WriteLine(reserva);
                    Console.WriteLine($"   Valor Total: {reserva.CalcularValorTotal()}EUR\n");
                }
                Console.WriteLine($"Total: {reservas.Count} reservas");
            }
        }

        static void ListarReservasPorCliente()
        {
            Console.Clear();
            Console.WriteLine("=== RESERVAS POR CLIENTE ===\n");

            Console.Write("NIF do cliente: ");
            string nif = Console.ReadLine();

            try
            {
                List<Reserva> reservas = ServicoReservas.ProcurarReservasPorCliente(nif);

                if (reservas.Count == 0)
                {
                    Console.WriteLine("\nNenhuma reserva encontrada para este cliente.");
                }
                else
                {
                    Console.WriteLine();
                    foreach (var reserva in reservas)
                    {
                        Console.WriteLine(reserva);
                    }
                    Console.WriteLine($"\nTotal: {reservas.Count} reservas");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
        }

        static void ListarReservasPorAlojamento()
        {
            Console.Clear();
            Console.WriteLine("=== RESERVAS POR ALOJAMENTO ===\n");

            Console.Write("Nome do alojamento: ");
            string nome = Console.ReadLine();

            try
            {
                List<Reserva> reservas = ServicoReservas.ProcurarReservasPorAlojamento(nome);

                if (reservas.Count == 0)
                {
                    Console.WriteLine("\nNenhuma reserva encontrada para este alojamento.");
                }
                else
                {
                    Console.WriteLine();
                    foreach (var reserva in reservas)
                    {
                        Console.WriteLine(reserva);
                    }
                    Console.WriteLine($"\nTotal: {reservas.Count} reservas");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
        }   
        #endregion

        #region FICHEIROS
        static void GuardarDados()
        {
            try
            {
                ServicoFicheiros.GuardarDados(FICHEIRO_DADOS);
                Console.WriteLine("\nDados guardados com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao guardar dados: {ex.Message}");
            }
        }

        static void CarregarDados()
        {
            try
            {
                if (ServicoFicheiros.CarregarDados(FICHEIRO_DADOS))
                {
                    Console.WriteLine("\nDados carregados com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nFicheiro nao encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao carregar dados: {ex.Message}");
            }
        }
        #endregion

        
    }
}














