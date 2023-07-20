using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Classes.Interfaces
{
    public interface IMovimentoEstoque
    {
        bool EntradaEstoque(CrudProduto listaProdutos);
        bool SaidaEstoque(CrudProduto listaProdutos);
    }
}