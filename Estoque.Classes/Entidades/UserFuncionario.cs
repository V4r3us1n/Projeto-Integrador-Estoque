using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Entidades
{
    public class UserFuncionario : Usuario
    {
        public UserFuncionario(string usuario, string senha)
        {
            NomeUsuario = usuario;
            Senha = senha;
        }

        public override string ToString()
        {
            return $"{NomeUsuario};{Senha}";
        }
    }
}