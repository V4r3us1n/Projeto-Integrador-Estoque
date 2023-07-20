using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class SemCategoriasCadastradasException : Exception
    {
        public SemCategoriasCadastradasException() : base("[ERRO!] Deve haver pelo menos 1 Categoria para realizar o Cadastro de um Produto!")
        {

        }
    }
}