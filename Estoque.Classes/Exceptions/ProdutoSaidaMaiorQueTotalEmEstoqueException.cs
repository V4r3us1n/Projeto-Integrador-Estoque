using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class ProdutoSaidaMaiorQueTotalEmEstoqueException : Exception
    {
        public ProdutoSaidaMaiorQueTotalEmEstoqueException() : base("[ERRO!] A QUANTIDADE DE PRODUTOS QUE SAEM NÃO PODE SER MAIOR QUE O TOTAL DE PRODUTOS EM ESTOQUE!")
        {

        }
    }
}