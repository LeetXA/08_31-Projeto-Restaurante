using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_31___Projeto_Restaurante
{
    public class Pedido
    {
        public int Id { get; private set; }
        public string Cliente { get; private set; }

        private Item[] itens = new Item[10];
        private int quantidadeItens = 0;

        public Pedido(int id, string cliente)
        {
            Id = id;
            Cliente = cliente;
        }

        public bool AdicionarItem(Item item)
        {
            if (item == null || quantidadeItens >= itens.Length)
                return false;

            itens[quantidadeItens] = item;
            quantidadeItens++;

            return true;
        }

        public bool RemoverItem(Item item)
        {
            if (item == null)
                return false;

            for (int i = 0; i < quantidadeItens; i++)
            {
                if (itens[i].Id == item.Id)
                {
                    for (int j = i; j < quantidadeItens - 1; j++)
                    {
                        itens[j] = itens[j + 1];
                    }

                    quantidadeItens--;
                    itens[quantidadeItens] = null;

                    return true;
                }
            }

            return false;
        }

        public Item BuscarItem(int id)
        {
            for (int i = 0; i < quantidadeItens; i++)
            {
                if (itens[i].Id == id)
                    return itens[i];
            }

            return null;
        }

        public double CalcularTotal()
        {
            double total = 0;

            for (int i = 0; i < quantidadeItens; i++)
            {
                total += itens[i].Preco;
            }

            return total;
        }

        public string DadosDoPedido()
        {
            StringBuilder dados = new StringBuilder();

            dados.AppendLine($"Pedido: {Id}");
            dados.AppendLine($"Cliente: {Cliente}");
            dados.AppendLine("Itens:");

            if (quantidadeItens == 0)
            {
                dados.AppendLine("Nenhum item adicionado.");
            }
            else
            {
                for (int i = 0; i < quantidadeItens; i++)
                {
                    dados.AppendLine(
                        $"  {itens[i].Id} - {itens[i].Descricao} - " +
                        $"R$ {itens[i].Preco:F2}");
                }
            }

            dados.AppendLine($"Total: R$ {CalcularTotal():F2}");

            return dados.ToString();
        }

    }
}
