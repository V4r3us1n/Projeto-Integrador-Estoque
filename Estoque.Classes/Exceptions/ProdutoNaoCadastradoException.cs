using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class ProdutoNaoCadastradoException : Exception
    {
        public ProdutoNaoCadastradoException() : base("[ERRO!] Produto Não Cadastrado!")
        {
            
        }
    }
}