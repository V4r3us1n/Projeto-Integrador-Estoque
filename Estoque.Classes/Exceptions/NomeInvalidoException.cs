using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class NomeInvalidoException : Exception
    {
        public NomeInvalidoException(string msg) : base(msg)
        {

        }
    }
}