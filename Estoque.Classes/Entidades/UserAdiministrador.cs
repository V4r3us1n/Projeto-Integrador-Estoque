using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Entidades
{
    public class UserAdiministrador : Usuario
    {
        public UserAdiministrador(string usuario, string senha)
        {
            NomeUsuario = usuario;
            Senha = senha;
        }
    }
}