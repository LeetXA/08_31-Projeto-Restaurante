using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_31___Projeto_Restaurante
{
    public class RestauranteController
    {
        private Restaurante restaurante = new Restaurante();
        private int proximoItem = 1;

        public void ExecutarMenu()
        {
            int opcao;

            do
            {
                Console.WriteLine("\n===== RESTAURANTE =====");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("1 - Criar novo pedido");
                Console.WriteLine("2 - Adicionar item ao pedido");
                Console.WriteLine("3 - Remover item do pedido");
                Console.WriteLine("4 - Consultar pedido");
                Console.WriteLine("5 - Cancelar pedido");
                Console.WriteLine("6 - Listar todos os pedidos");
                Console.Write("Escolha uma opção: ");

                opcao = LerInteiro();

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Sistema encerrado.");
                        break;

                    case 1:
                        CriarPedido();
                        break;

                    case 2:
                        AdicionarItem();
                        break;

                    case 3:
                        RemoverItem();
                        break;

                    case 4:
                        ConsultarPedido();
                        break;

                    case 5:
                        CancelarPedido();
                        break;

                    case 6:
                        ListarPedidos();
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != 0);
        }

        private int LerInteiro()
        {
            int valor;

            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Digite um número inteiro válido: ");
            }

            return valor;
        }

        private double LerPreco()
        {
            double preco;

            while (!double.TryParse(
                Console.ReadLine(),
                out preco
            ) || preco < 0)
            {
                Console.Write("Digite um preço válido: R$ ");
            }

            return preco;
        }


        private void CriarPedido()
        {
            if (restaurante.QuantidadePedidos() >= 50)
            {
                Console.WriteLine("Limite de 50 pedidos atingido.");
                return;
            }

            Console.Write("Nome do cliente: ");
            string cliente = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cliente))
            {
                Console.WriteLine("O nome do cliente é obrigatório.");
                return;
            }

            int id = restaurante.ProximoId();
            Pedido pedido = new Pedido(id, cliente.Trim());

            if (restaurante.NovoPedido(pedido))
            {
                Console.WriteLine($"Pedido {id} criado com sucesso!");
            }
        }

        private void AdicionarItem()
        {
            Console.Write("ID do pedido: ");
            int idPedido = LerInteiro();

            Pedido pedido = restaurante.BuscarPedido(idPedido);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            Console.Write("Descrição do item: ");
            string descricao = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine("A descrição é obrigatória.");
                return;
            }

            Console.Write("Preço do item: R$ ");
            double preco = LerPreco();

            Item item = new Item(
                proximoItem,
                descricao.Trim(),
                preco
            );

            if (pedido.AdicionarItem(item))
            {
                proximoItem++;
                Console.WriteLine("Item adicionado com sucesso.");
            }
            else
            {
                Console.WriteLine("Não foi possível adicionar o item.");
            }
        }

        private void RemoverItem()
        {
            Console.Write("ID do pedido: ");
            int idPedido = LerInteiro();

            Pedido pedido = restaurante.BuscarPedido(idPedido);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            Console.Write("ID do item que deseja remover: ");
            int idItem = LerInteiro();

            Item item = pedido.BuscarItem(idItem);

            if (item != null && pedido.RemoverItem(item))
            {
                Console.WriteLine("Item removido com sucesso.");
            }
            else
            {
                Console.WriteLine("Item não encontrado no pedido.");
            }
        }

        private void ConsultarPedido()
        {
            Console.Write("ID do pedido: ");
            int id = LerInteiro();

            Pedido pedido = restaurante.BuscarPedido(id);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            Console.WriteLine("\n" + pedido.DadosDoPedido());
        }

        private void CancelarPedido()
        {
            Console.Write("ID do pedido que deseja cancelar: ");
            int id = LerInteiro();

            if (restaurante.CancelarPedido(id))
            {
                Console.WriteLine("Pedido cancelado com sucesso.");
            }
            else
            {
                Console.WriteLine("Pedido não encontrado.");
            }
        }

        private void ListarPedidos()
        {
            Pedido[] pedidos = restaurante.ListarPedidos();

            if (pedidos.Length == 0)
            {
                Console.WriteLine("Nenhum pedido cadastrado.");
                return;
            }

            double somaGeral = 0;

            Console.WriteLine("\n===== HISTÓRICO DE PEDIDOS =====");

            foreach (Pedido pedido in pedidos)
            {
                double total = pedido.CalcularTotal();

                Console.WriteLine(
                    $"Pedido {pedido.Id} | Cliente: {pedido.Cliente} | " +
                    $"Total: R$ {total:F2}");

                somaGeral += total;
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Quantidade: {pedidos.Length}");
            Console.WriteLine($"Soma geral do dia: R$ {somaGeral:F2}");
        }
    }
}
