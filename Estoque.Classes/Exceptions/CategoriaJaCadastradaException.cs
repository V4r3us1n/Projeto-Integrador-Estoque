using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes
{
    public class CategoriaJaCadastradaException : Exception
    {
        public CategoriaJaCadastradaException() : base("[ERRO!] Categoria de Produto Já Cadastrada!")
        {

        }
    }
}