using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class RespostaInvalidaException : Exception
    {
        public RespostaInvalidaException(string msg) : base(msg)
        {

        }
    }
}