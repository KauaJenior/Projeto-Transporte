using System;
using System.Linq;
using ProjetoTransporte;

namespace ProjetoTransporte
{
    class Program
    {
        static GestorFrota gestor = new GestorFrota();

        static void Main(string[] args)
        {
            InicializarPadrao();

            int opcao = -1;

            while (opcao != 0)
            {
                MostrarMenu();
                opcao = LerInt("Opção: ");
                Console.WriteLine();

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Finalizando o sistema...");
                        break;

                    case 1:
                        CadastrarVeiculo();
                        break;

                    case 2:
                        CadastrarGaragem();
                        break;

                    case 3:
                        IniciarJornada();
                        break;

                    case 4:
                        EncerrarJornada();
                        break;

                    case 5:
                        LiberarViagem();
                        break;

                    case 6:
                        ListarVeiculosGaragem();
                        break;

                    case 7:
                        QuantidadeViagens();
                        break;

                    case 8:
                        ListarViagens();
                        break;

                    case 9:
                        PassageirosTransportados();
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                Console.WriteLine();
            }
        }

        // ============================================================
        // CONFIG INICIAL PADRÃO (8 vans + 2 garagens)
        // ============================================================

        static void InicializarPadrao()
        {
            gestor.CadastrarGaragem(new Garagem(1, "Congonhas"));
            gestor.CadastrarGaragem(new Garagem(2, "Guarulhos"));

            for (int i = 1; i <= 8; i++)
                gestor.CadastrarVeiculo(new Veiculo(i, $"VAN-{i:D2}", 15));

            Console.WriteLine("Sistema carregado com 8 vans e 2 garagens.");
            Console.WriteLine("Use a opção 3 para iniciar a jornada antes de liberar viagens.\n");
        }

        // ============================================================
        // MENU
        // ============================================================

        static void MostrarMenu()
        {
            Console.WriteLine("===== PROJETO TRANSPORTE – PILHA =====");
            Console.WriteLine("0. Finalizar");
            Console.WriteLine("1. Cadastrar veículo");
            Console.WriteLine("2. Cadastrar garagem");
            Console.WriteLine("3. Iniciar jornada");
            Console.WriteLine("4. Encerrar jornada");
            Console.WriteLine("5. Liberar viagem (origem > destino)");
            Console.WriteLine("6. Listar veículos em garagem");
            Console.WriteLine("7. Informar quantidade de viagens (origem > destino)");
            Console.WriteLine("8. Listar viagens (origem > destino)");
            Console.WriteLine("9. Informar passageiros transportados (origem > destino)");
            Console.WriteLine("======================================");
        }

        // ============================================================
        // MÉTODOS DE LEITURA
        // ============================================================

        static int LerInt(string txt)
        {
            Console.Write(txt);
            int valor;
            while (!int.TryParse(Console.ReadLine(), out valor))
                Console.Write("Valor inválido. " + txt);
            return valor;
        }

        static string LerTexto(string txt)
        {
            Console.Write(txt);
            return Console.ReadLine();
        }

        // ============================================================
        // OPÇÕES DO MENU
        // ============================================================

        static void CadastrarVeiculo()
        {
            if (gestor.JornadaIniciada)
            {
                Console.WriteLine("Não é possível cadastrar veículos com jornada iniciada.");
                return;
            }

            int id = LerInt("ID do veículo: ");
            if (gestor.ObterVeiculo(id) != null)
            {
                Console.WriteLine("Já existe um veículo com esse ID.");
                return;
            }

            string placa = LerTexto("Placa: ");
            int cap = LerInt("Capacidade: ");

            gestor.CadastrarVeiculo(new Veiculo(id, placa, cap));

            Console.WriteLine("Veículo cadastrado com sucesso.");
        }

        static void CadastrarGaragem()
        {
            if (gestor.JornadaIniciada)
            {
                Console.WriteLine("Não é possível cadastrar garagens com jornada iniciada.");
                return;
            }

            int id = LerInt("ID da garagem: ");

            if (gestor.ObterGaragem(id) != null)
            {
                Console.WriteLine("Já existe uma garagem com esse ID.");
                return;
            }

            string nome = LerTexto("Nome da garagem: ");
            gestor.CadastrarGaragem(new Garagem(id, nome));

            Console.WriteLine("Garagem cadastrada com sucesso.");
        }

        static void IniciarJornada()
        {
            if (gestor.IniciarJornada())
                Console.WriteLine("Jornada iniciada. Veículos distribuídos entre as garagens.");
            else
                Console.WriteLine("Falha ao iniciar jornada.");
        }

        static void EncerrarJornada()
        {
            var rel = gestor.EncerrarJornada();
            Console.WriteLine("\n=== RELATÓRIO ===");
            foreach (var linha in rel)
                Console.WriteLine(linha);
        }

        static void LiberarViagem()
        {
            int origem = LerInt("ID da garagem de origem: ");
            int destino = LerInt("ID da garagem de destino: ");

            var resultado = gestor.LiberarViagem(origem, destino);

            if (resultado.ok)
                Console.WriteLine("✔ " + resultado.msg);
            else
                Console.WriteLine("✖ ERRO: " + resultado.msg);
        }

        static void ListarVeiculosGaragem()
        {
            int id = LerInt("ID da garagem: ");

            var garagem = gestor.ObterGaragem(id);

            if (garagem == null)
            {
                Console.WriteLine("Garagem não encontrada.");
                return;
            }

            Console.WriteLine(garagem);

            var lista = garagem.ListarVeiculos();

            if (!lista.Any())
            {
                Console.WriteLine("Nenhum veículo estacionado.");
                return;
            }

            Console.WriteLine("Veículos (do primeiro a sair ao último):");
            foreach (var v in lista)
                Console.WriteLine("  " + v);
        }

        static void QuantidadeViagens()
        {
            int o = LerInt("Origem: ");
            int d = LerInt("Destino: ");

            int qtd = gestor.GetQtdViagens(o, d);

            Console.WriteLine($"Quantidade de viagens {o} > {d}: {qtd}");
        }

        static void ListarViagens()
        {
            int o = LerInt("Origem: ");
            int d = LerInt("Destino: ");

            var lista = gestor.GetViagens(o, d);

            if (!lista.Any())
            {
                Console.WriteLine("Nenhuma viagem encontrada.");
                return;
            }

            foreach (var v in lista)
                Console.WriteLine(v);
        }

        static void PassageirosTransportados()
        {
            int o = LerInt("Origem: ");
            int d = LerInt("Destino: ");

            int total = gestor.GetPassageiros(o, d);

            Console.WriteLine($"Total de passageiros transportados {o} > {d}: {total}");
        }
    }
}
