using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_31___Projeto_Restaurante
{
    public class Restaurante
    {
        private int proxPedido = 1;
        private Pedido[] pedidos = new Pedido[50];
        private int quantidadePedidos = 0;


        public bool NovoPedido(Pedido pedido)
        {
            if (pedido == null || quantidadePedidos >= pedidos.Length)
                return false;

            // Armazena o próprio pedido recebido.
            pedidos[quantidadePedidos] = pedido;

            quantidadePedidos++;
            proxPedido++;

            return true;
        }


        public int ProximoId()
        {
            return proxPedido;
        }

        public Pedido BuscarPedido(int id)
        {
            for (int i = 0; i < quantidadePedidos; i++)
            {
                if (pedidos[i].Id == id)
                    return pedidos[i];
            }

            return null;
        }

        public bool CancelarPedido(int id)
        {
            for (int i = 0; i < quantidadePedidos; i++)
            {
                if (pedidos[i].Id == id)
                {
                    for (int j = i; j < quantidadePedidos - 1; j++)
                    {
                        pedidos[j] = pedidos[j + 1];
                    }

                    quantidadePedidos--;
                    pedidos[quantidadePedidos] = null;

                    return true;
                }
            }

            return false;
        }

        public Pedido[] ListarPedidos()
        {
            Pedido[] resultado = new Pedido[quantidadePedidos];

            for (int i = 0; i < quantidadePedidos; i++)
            {
                resultado[i] = pedidos[i];
            }

            return resultado;
        }

        public int QuantidadePedidos()
        {
            return quantidadePedidos;
        }

    }
}
