using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class MovimentoProdutoMenorQue1Exception : Exception
    {
        public MovimentoProdutoMenorQue1Exception() : base("[ERRO!] DEVE TER AO MENOS 1 PRODUTO PARA O MOVIMENTO SER EFETUADO!")
        {

        }
    }
}