using Estoque.Classes;
using Estoque.Classes.Entidades;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estoque.Test
{
    public class Tests
    {
        static void Main(string[] args)
        {

            //[Test]
            //public void ExibirListaProdutos_DeveRetornarListaVazia_QuandoNaoHaProdutosCadastrados()
            //{

            //    CrudProduto crudProduto = new CrudProduto();
            //    Dictionary<int, Produto1> listaProdutos = crudProduto.ExibirListaProdutos();
            //    Assert.NotNull(listaProdutos);
            //}

            //[Test]
            //public void verificar_se_esta_efetuando_cadastro_correto()
            //{
            //    CrudProduto crud = new CrudProduto();
            //    CrudCategoriaProduto categorias = new CrudCategoriaProduto();


            //    Produto1 produto = new Produto1("Produto Teste", 1, 10);
            //    crud.CadastrarProduto(categorias);

            //    Produto1 produtoCadastrado = crud.ExibirListaProdutos().Values.FirstOrDefault();
            //    Assert.NotNull(produtoCadastrado);
            //    Assert.Equals(produto.Nome, produtoCadastrado.Nome);
            //    Assert.Equals(produto.CategoriaProdutoId, produtoCadastrado.CategoriaProdutoId);
            //    Assert.Equals(produto.QuantidadeProduto, produtoCadastrado.QuantidadeProduto);
            //}

            //[Test]
            //public void verificar_se_esta_efetuando_consulta()
            //{
            //    CrudProduto crud = new CrudProduto();
            //    Produto1 produto = new Produto1("Produto Teste", 1, 10);
            //    crud.CadastrarProduto(new CrudCategoriaProduto());

            //    Produto1 produtoConsultado = crud.ExibirListaProdutos().Values.FirstOrDefault();
            //    Assert.NotNull(produtoConsultado);
            //    Assert.Equals(produto.IdProduto, produtoConsultado.IdProduto);
            //    Assert.Equals(produto.Nome, produtoConsultado.Nome);
            //    Assert.Equals(produto.CategoriaProdutoId, produtoConsultado.CategoriaProdutoId);
            //    Assert.Equals(produto.QuantidadeProduto, produtoConsultado.QuantidadeProduto);
            //}

            //[Test]
            //public void verificar_se_esta_efetuando_lista()
            //{
            //    CrudProduto crud = new CrudProduto();
            //    crud.CadastrarProduto(new CrudCategoriaProduto());
            //    crud.CadastrarProduto(new CrudCategoriaProduto());


            //    int quantidadeProdutosCadastradosEsperada = 2;

            //    int quantidadeProdutosCadastradosAtual = crud.ExibirListaProdutos().Count;

            //    Assert.Equals(quantidadeProdutosCadastradosEsperada, quantidadeProdutosCadastradosAtual);
            //}

            //[Test]
            //public void verificar_se_esta_efetuando_edicao()
            //{
            //    CrudProduto crud = new CrudProduto();
            //    Produto1 produto = new Produto1("Produto Teste", 1, 10);
            //    crud.CadastrarProduto(new CrudCategoriaProduto());

            //    Produto1 produtoEditado = new Produto1("Produto Editado", 2, 20);
            //    produtoEditado.IdProduto = produto.IdProduto;

            //    crud.EditarProduto(produtoEditado);

            //    Produto1 produtoEditadoConsulta = crud.ExibirListaProdutos().Values.FirstOrDefault();

            //    Assert.NotNull(produtoEditadoConsulta);
            //    Assert.Equals(produtoEditado.Nome, produtoEditadoConsulta.Nome);
            //    Assert.Equals(produtoEditado.CategoriaProdutoId, produtoEditadoConsulta.CategoriaProdutoId);
            //    Assert.Equals(produtoEditado.QuantidadeProduto, produtoEditadoConsulta.QuantidadeProduto);
            //}

            //[Test]
            //public void verificar_se_esta_efetuando_exclusao()
            //{
            //    CrudProduto crud = new CrudProduto();
            //    Produto1 produto = new Produto1("Produto Teste", 1, 10);
            //    crud.CadastrarProduto(new CrudCategoriaProduto());

            //    crud.ExcluirProduto(produto);

            //    int quantidadeProdutosCadastradosEsperada = 0;

            //    int quantidadeProdutosCadastradosAtual = crud.ExibirListaProdutos().Count;

            //    Assert.Equals(quantidadeProdutosCadastradosEsperada, quantidadeProdutosCadastradosAtual);
            //}
        }
    }
}