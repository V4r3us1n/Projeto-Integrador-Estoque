using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Entidades
{
    public abstract class Usuario
    {
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
    }
}