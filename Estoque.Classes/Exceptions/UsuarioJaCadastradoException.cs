using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class UsuarioJaCadastradoException : Exception
    {
        public UsuarioJaCadastradoException() : base("[ERRO!] Usuário Já Cadastrado!")
        {

        }
    }
}