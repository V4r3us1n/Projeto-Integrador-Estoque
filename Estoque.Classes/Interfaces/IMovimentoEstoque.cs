using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Interfaces
{
    public interface IMovimentoEstoque
    {
        void EntradaEstoque(CrudProduto listaProdutos, string nomeUsuario);
        void SaidaEstoque(CrudProduto listaProdutos, string nomeUsuario);
    }
}