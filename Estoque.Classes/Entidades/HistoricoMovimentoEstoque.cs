using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Classes.Exceptions;
using Estoque.Classes.Interfaces;
using System.IO;

namespace Estoque.Classes.Entidades
{
    public class HistoricoMovimentoEstoque : IMovimentoEstoque
    {
        private List<ItemProduto> historicoSaida = new List<ItemProduto>();
        private List<ItemProduto> historicoEntrada = new List<ItemProduto>();

        private List<ItemProduto> historicoMovimentoProduto = new List<ItemProduto>();
       
       private string caminhoBD_HistoricoMovimentoProduto = "BancoDeDados/BD_HistoricoMovimentoProduto.txt";

        public HistoricoMovimentoEstoque()
        {
            CarregarDadosHistoricoMovimentoProduto();
        }

        public void AdicionarHistoricoEntrada(ItemProduto estoque)
        {
            historicoEntrada.Add(estoque);
            historicoMovimentoProduto.Add(estoque);
            SalvarInformacoesHistoricoMovimentoProduto();
        }
        public void AdicionarHistoricoSaida(ItemProduto estoque)
        {
            historicoSaida.Add(estoque);
            historicoMovimentoProduto.Add(estoque);
            SalvarInformacoesHistoricoMovimentoProduto();
        }

        //Movimento de Entrada

        public void EntradaEstoque(CrudProduto listaProdutos, string nomeUsuario)
        {
            Console.WriteLine("========== ENTRADA DE ESTOQUE ==========");

            int produtoId = 0;
            int quantidadeEntradaProduto = 0;
            double precoCompraProduto = 0;

            listaProdutos.ListarProdutos();

            Console.Write("\nDigite o ID do Produto que está entrando: ");
            if (ValidarIdProduto(ref produtoId, listaProdutos) != false)
            {
                Console.Write("\nDigite a quantidade de produtos que foi comprado: ");
                if (ValidarQuantidadeCompraProduto(ref quantidadeEntradaProduto) != false)
                {
                    Console.Write("Digite o preço pelo qual cada produto  foi comprado: R$");
                    if (ValidarPrecoProduto(ref precoCompraProduto) != false)
                    {
                        foreach (KeyValuePair<int, Produto1> pair in listaProdutos.ExibirListaProdutos())
                        {
                            if (pair.Key == produtoId)
                            {
                                pair.Value.AdicionarUnidadeProdutoNoEstoque(quantidadeEntradaProduto);
                                Console.WriteLine("\n[AVISO!] Compra efetuada com Sucesso!");
                                var unidade = new ItemProduto(pair.Value.Nome, pair.Key, quantidadeEntradaProduto, precoCompraProduto, nomeUsuario); 

                                AdicionarHistoricoEntrada(unidade);
                                listaProdutos.SalvarInfo();
                            }
                        }
                    }
                }
            }
            Console.ReadKey();
        }

        //Movimento de Saída
        public void SaidaEstoque(CrudProduto listaProdutos, string nomeUsuario)
        {
            Console.WriteLine("========== SAÍDA DE ESTOQUE ==========");

            int idProduto = 0;
            int quantidadeProdutoSaida = 0;
            int quantidadeProdutoEmEstoque = 0;
            double precoVendaProduto = 0;

            listaProdutos.ListarProdutos();

            Console.Write("\nDigite o ID do Produto que está saindo: ");
            if (ValidarIdProduto(ref idProduto, listaProdutos) != false)
            {
                foreach (KeyValuePair<int, Produto1> pair in listaProdutos.ExibirListaProdutos())
                {
                    if (pair.Key == idProduto)
                    {
                        quantidadeProdutoEmEstoque = pair.Value.QuantidadeProduto;
                        break;
                    }
                }

                Console.Write("Digite o a quantidade de produtos que saíram: ");
                if (ValidarQuantidadeSaidaProduto(ref quantidadeProdutoSaida, quantidadeProdutoEmEstoque) != false)
                {
                    Console.Write("Digite o preço pelo qual cada produto foi vendido: R$");
                    if (ValidarPrecoProduto(ref precoVendaProduto) != false)
                    {

                    }
                    foreach (KeyValuePair<int, Produto1> pair in listaProdutos.ExibirListaProdutos())
                    {
                        if (pair.Key == idProduto)
                        {
                            pair.Value.TirarUnidadeProdutoDoEstoque(quantidadeProdutoSaida);

                            Console.WriteLine("[AVISO!] Venda efetuada com Sucesso!");

                            var produto = new ItemProduto(pair.Value.Nome, pair.Key, quantidadeProdutoSaida, precoVendaProduto, nomeUsuario);

                            AdicionarHistoricoSaida(produto);
                            listaProdutos.SalvarInfo();
                        }
                    }
                }
            }    
            Console.ReadKey();
        }

        //Gerando Relatórios
        public void GerarHistoricoEntrada()
        {
            if (historicoEntrada.Count == 0)
            {
                Console.WriteLine("[AVISO] O estoque ainda não teve nenhuma movimentação de entrada");
            }
            else
            {
                Console.WriteLine("========== RELATÓRIO MOVIMENTO ENTRADA ==========");
                foreach (var item in historicoEntrada)
                {
                    Console.WriteLine($"DATA DO MOVIMENTO: {item.DataMovimento}");
                    Console.WriteLine($"USUÁRIO QUE REALIZOU O MOVIMENTO: {item.NomeUsuario}");
                    Console.WriteLine($"ID Movimento: {item.IdItemProduto}");
                    Console.WriteLine($"ID Produto: {item.ProdutoId}");
                    Console.WriteLine($"Nome: {item.NomeProduto}");
                    Console.WriteLine($"Preço: R${item.PrecoItemProduto.ToString("F")}");
                    Console.WriteLine($"Quantidade: {item.QuantidadeMovimentoProduto}");
                    double valorTotal = Double.Parse($"{item.QuantidadeMovimentoProduto}");
                    valorTotal *= item.PrecoItemProduto;
                    Console.WriteLine($"Valor Total: R${valorTotal.ToString("F")}");
                    Console.WriteLine("=================================================");
                }
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public void GerarHistoricoSaida()
        {
            if (historicoSaida.Count == 0)
            {
                Console.WriteLine("[AVISO] O estoque ainda não teve nenhuma movimentação de saída");

            }
            else
            {
                Console.WriteLine("========== RELATÓRIO MOVIMENTO SAÍDA ==========");
                foreach (var item in historicoSaida)
                {
                    Console.WriteLine($"DATA DO MOVIMENTO: {item.DataMovimento}");
                    Console.WriteLine($"USUÁRIO QUE REALIZOU O MOVIMENTO: {item.NomeUsuario}");
                    Console.WriteLine($"ID Movimento: {item.IdItemProduto}");
                    Console.WriteLine($"ID Produto: {item.ProdutoId}");
                    Console.WriteLine($"Nome: {item.NomeProduto}");
                    Console.WriteLine($"Preço: R${item.PrecoItemProduto.ToString("F")}");
                    Console.WriteLine($"Quantidade: {item.QuantidadeMovimentoProduto}");
                    double valorTotal = Double.Parse($"{item.QuantidadeMovimentoProduto}");
                    valorTotal *= item.PrecoItemProduto;
                    Console.WriteLine($"Valor Total: R${valorTotal.ToString("F")}");
                    Console.WriteLine("===============================================");
                }

            }
            Console.WriteLine();
            Console.ReadKey();
        }
        
        public void GeraRelatorioGeral(CrudProduto produto, CrudCategoriaProduto categoria)
        {
            var historicoProduto = produto.RelatorioProduto();
            var historicoCategoria = categoria.RelatorioCategoria();

            Console.WriteLine("==================PRODUTOS===============");
            foreach (var item in historicoProduto)
            {
                if (DateTime.Parse(item[5]) != new DateTime(0))
                {
                    Console.WriteLine($"Data Exclusão: {item[5]}");
                }
                else
                {
                    Console.WriteLine($"Data de Cadastro: {item[4]}");
                }
                Console.WriteLine($"Nome: {item[1]}");
                Console.WriteLine($"ID: {item[0]}");
                Console.WriteLine("=========================================");
            }

            Console.WriteLine("==================CATEGORIAS===============");
            foreach (var item in historicoCategoria)
            {
                if (DateTime.Parse(item[3]) != new DateTime(0))
                {
                    Console.WriteLine($"Data Exclusão: {item[3]}");
                }
                else
                {
                    Console.WriteLine($"Data de Cadastro: {item[2]}");
                }
                Console.WriteLine($"Nome da Categoria: {item[1]}");
                Console.WriteLine($"ID: {item[0]}");
                Console.WriteLine("=========================================");
            }
            Console.ReadKey();
        }

        //Validações
        private bool ValidarIdProduto(ref int idProduto, CrudProduto listaProdutos)
        {
            try
            {
                idProduto = int.Parse(Console.ReadLine());

                foreach (KeyValuePair<int, Produto1> pair in listaProdutos.ExibirListaProdutos())
                {
                    if (pair.Key == idProduto)
                    {
                        return true;
                    }
                }

                throw new ArgumentException("[ERRO!] ID DE PRODUTO NÃO CADASTRADO!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarQuantidadeCompraProduto(ref int quantidadeCompraProduto)
        {
            try
            {
                quantidadeCompraProduto = int.Parse(Console.ReadLine());

                if (quantidadeCompraProduto < 1)
                {
                    throw new MovimentoProdutoMenorQue1Exception();
                }

                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (MovimentoProdutoMenorQue1Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarPrecoProduto(ref double precoProduto)
        {
            try
            {
                precoProduto = double.Parse(Console.ReadLine());

                if (precoProduto < 0.01)
                {
                    throw new ArgumentException("[ERRO!] O VALOR DE COMPRA NÃO PODE SER INFERIOR A R$0,01!");
                }
                else if ($"{precoProduto}".Length > precoProduto.ToString("F").Length)
                {
                    throw new NotFiniteNumberException("[ERRO!] SÓ PODE SER COLOCADO 2 CASAS DECIMAIS!");
                }

                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (NotFiniteNumberException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarQuantidadeSaidaProduto(ref int quantidadeSaidaProduto, int quantidadeProdutoEmEstoque)
        {
            try
            {
                quantidadeSaidaProduto = int.Parse(Console.ReadLine());

                if (quantidadeSaidaProduto < 1)
                {
                    throw new MovimentoProdutoMenorQue1Exception();
                }
                else if (quantidadeSaidaProduto > quantidadeProdutoEmEstoque)
                {
                    throw new ProdutoSaidaMaiorQueTotalEmEstoqueException();
                }

                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ProdutoSaidaMaiorQueTotalEmEstoqueException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (MovimentoProdutoMenorQue1Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Salvando Informações no Banco de Dados
        private void SalvarInformacoesHistoricoMovimentoProduto()
        {
            if (!File.Exists(caminhoBD_HistoricoMovimentoProduto))
            {
                File.Delete(caminhoBD_HistoricoMovimentoProduto);
            }
            else
            {
                using (Stream saida = File.Open(caminhoBD_HistoricoMovimentoProduto, FileMode.Create))
                {
                    using (StreamWriter escritor = new StreamWriter(saida))
                    {
                        string tipoMovimento = "";
                        int contador = 0;

                        foreach (ItemProduto itemMovimentoProduto in historicoMovimentoProduto)
                        {
                            foreach (ItemProduto itemMovimentoEntrada in historicoEntrada)
                            {
                                if (itemMovimentoEntrada.IdItemProduto == itemMovimentoProduto.IdItemProduto)
                                {
                                    tipoMovimento = "Entrada";
                                    escritor.WriteLine($"{historicoMovimentoProduto[contador]};{tipoMovimento}");
                                    break;
                                }
                                else
                                {
                                    tipoMovimento = "Saida";
                                    escritor.WriteLine($"{historicoMovimentoProduto[contador]};{tipoMovimento}");
                                }
                            }
                            contador++;
                        }
                    }
                }
            }
        }

        //Carregando Dados de Movimento de Produto
        private void CarregarDadosHistoricoMovimentoProduto()
        {
            if (!File.Exists(caminhoBD_HistoricoMovimentoProduto))
            {
                using (Stream criandoBD = File.Open(caminhoBD_HistoricoMovimentoProduto, FileMode.Create))
                {

                }
            }
            else
            {
                List<ItemProduto> historicoMovimentoEntradaProdutoTemp = new List<ItemProduto>();
                List<ItemProduto> historicoMovimentoSaidaProdutoTemp = new List<ItemProduto>();
                List<ItemProduto> historicoMovimentoProdutoTemp = new List<ItemProduto>();

                using (Stream bancoDeDados = File.Open(caminhoBD_HistoricoMovimentoProduto, FileMode.Open))
                {
                    using (StreamReader leitor = new(bancoDeDados))
                    {
                        string linha = leitor.ReadLine();

                        while (linha != null)
                        {
                            string[] movimentoProdutoString = linha.Split(";");

                            ItemProduto itemProdutoTemp = new ItemProduto
                            (
                                movimentoProdutoString[2],
                                Int32.Parse(movimentoProdutoString[1]),
                                Int32.Parse(movimentoProdutoString[3]),
                                Double.Parse(movimentoProdutoString[4]),
                                movimentoProdutoString[6]
                            );

                            itemProdutoTemp.AdicionarDataMovimento(DateTime.Parse(movimentoProdutoString[5]));

                            if (movimentoProdutoString[7] == "Entrada")
                            {
                                historicoMovimentoEntradaProdutoTemp.Add(itemProdutoTemp);
                            }
                            else
                            {
                                historicoMovimentoSaidaProdutoTemp.Add(itemProdutoTemp);
                            }
                            historicoMovimentoProdutoTemp.Add(itemProdutoTemp);

                            linha = leitor.ReadLine();
                        }
                    }
                }

                historicoEntrada = historicoMovimentoEntradaProdutoTemp;
                historicoSaida = historicoMovimentoSaidaProdutoTemp;
                historicoMovimentoProduto = historicoMovimentoProdutoTemp;
            }
        }
    }
}