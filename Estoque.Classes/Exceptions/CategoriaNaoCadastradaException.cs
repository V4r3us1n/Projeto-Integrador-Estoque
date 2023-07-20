using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class CategoriaNaoCadastradaException : Exception
    {
        public CategoriaNaoCadastradaException() : base("[ERRO!] Categoria Não Cadastrada!")
        {
            
        }
    }
}