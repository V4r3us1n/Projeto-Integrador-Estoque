using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Entidades
{
    public class ItemProduto
    {
        public int IdItemProduto {get;private set;}
        public string NomeProduto{get;private set;}
        public int QuantidadeMovimentoProduto {get; private set;}
        public int ProdutoId {get;private set;}
        public double PrecoItemProduto {get;private set;}
        public string NomeUsuario {get;private set;}
        private static int quantidadeItemProdutosCadatrados = 0;

        public DateTime DataMovimento {get; private set;}

        public ItemProduto(string nomeProduto, int produtoId, int quantidadeMovimentoProduto, double precoItemProduto, string nomeUsuario)
        {
            AdicionarNovoItemProduto();
            IdItemProduto = quantidadeItemProdutosCadatrados;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            QuantidadeMovimentoProduto = quantidadeMovimentoProduto;
            PrecoItemProduto = precoItemProduto;
            DataMovimento = DateTime.Now;
            NomeUsuario = nomeUsuario;
        }

        public void AdicionarDataMovimento(DateTime dataMovimento)
        {
            DataMovimento = dataMovimento;
        }

        private void AdicionarNovoItemProduto()
        {
            quantidadeItemProdutosCadatrados++;
        }

        public override string ToString()
        {
            return $"{IdItemProduto};{ProdutoId};{NomeProduto};{QuantidadeMovimentoProduto};{PrecoItemProduto};{DataMovimento};{NomeUsuario}";
        }
    }
}