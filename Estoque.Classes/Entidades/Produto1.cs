using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Entidades
{
    public class Produto1
    {
        public DateTime DataCadastro { get; private set; }
        public DateTime DataExclusao = new DateTime(0);
        public int IdProduto { get; private set; }
        public int CategoriaProdutoId { get; private set; }
        public string Nome { get; private set; }
        public int QuantidadeProduto = 0;
        private static int quantidadeProdutosCadastrados = 0;

        public Produto1(string nome, int categoriaProdutoId, int qtde)
        {
            AdicionarProduto();
            IdProduto = quantidadeProdutosCadastrados;
            CategoriaProdutoId = categoriaProdutoId;
            QuantidadeProduto = qtde;
            Nome = nome;
            DataCadastro = DateTime.Now;
        }

        private void AdicionarProduto()
        {
            quantidadeProdutosCadastrados++;
        }

        public void AdicionarDataCadastro(DateTime dataCadastro)
        {
            DataCadastro = dataCadastro;
        }

        public void AdicionarDataExclusao(DateTime dataExclusao)
        {
            DataExclusao = dataExclusao;
        }

        public void AdicionarUnidadeProdutoNoEstoque(int quantidadeEntradaProduto)
        {
            QuantidadeProduto += quantidadeEntradaProduto;
        }

        public void TirarUnidadeProdutoDoEstoque(int quantidadeSaidaProduto)
        {
            QuantidadeProduto -= quantidadeSaidaProduto;
        }

        public override string ToString()
        {
            return $"{IdProduto};{Nome};{CategoriaProdutoId};{QuantidadeProduto};{quantidadeProdutosCadastrados};{DataCadastro};{DataExclusao}";
        }

        public string Exibir()
        {
            Console.WriteLine($"ID: {IdProduto}");
            Console.WriteLine($"Nome: {Nome}");
            return "===================================";
        }
    }
}