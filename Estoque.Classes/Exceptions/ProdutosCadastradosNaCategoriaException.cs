using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Exceptions
{
    public class ProdutosCadastradosNaCategoriaException : Exception
    {
        public ProdutosCadastradosNaCategoriaException() : base("[ERRO!] Não é possível excluir a Categoria pois há Produtos vinculados a ela!")
        {

        }
    }
}