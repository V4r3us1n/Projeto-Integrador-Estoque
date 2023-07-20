using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class PrecoVendaInvalidoException : Exception
    {
        public PrecoVendaInvalidoException() : base("[ERRO!] O PREÇO DE VENDA NÃO PODE SER INFERIOR A 1.5x O PREÇO PELO QUAL FOI COMPRADO!")
        {

        }
    }
}