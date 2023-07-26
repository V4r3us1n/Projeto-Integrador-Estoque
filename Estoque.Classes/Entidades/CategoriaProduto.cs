using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes
{
    public class CategoriaProduto
    {
        public int IdCategoriaProduto {get; set;}
        public string NomeCategoriaProduto {get; private set;}
        private static int quantidadeCategoriasProduto = 0;
        public DateTime DataCadastro;
        public DateTime DataExclusao = new DateTime(0);

        public CategoriaProduto (string nomeCategoriaProduto)
        {
            DataCadastro = DateTime.Now;
            NomeCategoriaProduto = nomeCategoriaProduto;
            AdicionarNovaCategoriaProduto();
            IdCategoriaProduto = quantidadeCategoriasProduto;
        }

        public void AdicionarDataCadastro(DateTime dataCadastro)
        {
            DataCadastro = dataCadastro;
        }

        public void AdicionarDataExclusao(DateTime dataExclusao)
        {
            DataExclusao = dataExclusao;
        }

        private void AdicionarNovaCategoriaProduto()
        {
            quantidadeCategoriasProduto++;
        }

        public override string ToString()
        {
            return $"{IdCategoriaProduto};{NomeCategoriaProduto};{quantidadeCategoriasProduto};{DataCadastro};{DataExclusao}";
        }
    }
}