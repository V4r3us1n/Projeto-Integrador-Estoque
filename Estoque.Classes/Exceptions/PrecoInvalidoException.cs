using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class PrecoInvalidoException : Exception
    {
        public PrecoInvalidoException() : base("[ERRO!] Preço Inválido!")
        {
            
        }
    }
}