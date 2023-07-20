using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class UsuarioNaoCadastradoException : Exception
    {
        public UsuarioNaoCadastradoException() : base("[ERRO!] Usuário Não Cadastrado!")
        {

        }
    }
}