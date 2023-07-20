using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class OpcaoInvalidaException : Exception
    {
        public OpcaoInvalidaException() : base("[ERRO!] Opção Inválida!")
        {
            
        }
    }
}