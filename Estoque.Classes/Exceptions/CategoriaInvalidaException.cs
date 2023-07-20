using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes
{
    public class CategoriaInvalidaException : Exception
    {
        public CategoriaInvalidaException() : base("[ERRO!] Categoria Inválida!")
        {

        }
    }
}