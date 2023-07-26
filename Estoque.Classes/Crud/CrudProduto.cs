using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Classes.Entidades;
using Estoque.Classes.Exceptions;
using System.IO;

namespace Estoque.Classes
{
    public class CrudProduto
    {
        private Dictionary<int, Produto1> listaProdutos = new Dictionary<int, Produto1>();
        private List<string[]> historicoProdutos = new List<string[]>();

        public CrudProduto()
        {
            CarregarDados_Produto();
            CarregarDados_HistoricoProduto();
        }

        private string caminhoBD_Produtos = "BancoDeDados/BD_Produtos.txt";
        private string caminhoBD_HistoricoProduto = "BancoDeDados/BD_HistoricoProduto.txt";

        //Exibir Lista Produtos
        public Dictionary<int, Produto1> ExibirListaProdutos()
        {
            return listaProdutos;
        }

        public List<string[]> RelatorioProduto()
        {
           
            return historicoProdutos;
        }

        //Cadastran Produto
        public void CadastrarProduto(CrudCategoriaProduto categorias)
        {

            if (ValidarSeHaCategoriasCadastradas(categorias))

            {
                string nomeProduto = "";


                Console.Write("Digite  o Nome do Produto: "); 
                nomeProduto = Console.ReadLine();

                ExibirTodasAsCategorias(categorias);
                Console.Write("Digite o Id da Categoria do Produto: ");
                int categoriaProdutoId = -1;
                int qtdeProduto = 0;

                if (ValidarCategoriaProduto(ref categoriaProdutoId, categorias.ExibirListaCategoriaProduto()) != false)
                {

                    var produto = new Produto1(nomeProduto, categoriaProdutoId, qtdeProduto);

                    listaProdutos.Add(produto.IdProduto, produto);

                    string[] historico = new string[]
                    {
                        $"{produto.IdProduto}",
                        $"{produto.Nome}",
                        $"{produto.CategoriaProdutoId}",
                        $"{produto.QuantidadeProduto}",
                        $"{produto.DataCadastro}",
                        $"{produto.DataExclusao}"
                    };
                    historicoProdutos.Add(historico);

                    SalvarInformacoesDoBD_Produtos();
                    SalvarInformacoesDoBD_HistoricoProduto();
                    Console.WriteLine("Produto Cadastrado com sucesso!");
                    Console.ReadKey();
                }
            }
            Console.ReadKey();
        }

        //Listar Produto
        public void ListarProdutos()
        {
            Console.WriteLine("========== LISTA DE PRODUTOS ==========");
            foreach (var produto in ExibirListaProdutos())
            {
                Console.WriteLine($"[ID: {produto.Key}] \n[Nome: {produto.Value.Nome}] \n" +
                    $"[Quantidade: {produto.Value.QuantidadeProduto}] \n" +
                    $"[Categoria {produto.Value.CategoriaProdutoId}]");
                Console.WriteLine("=========================================");
            }
            Console.WriteLine("");
            Console.ReadKey();
        }

        //Consultar Produto
        public void ConsultarProduto()
        {
            string produtoConsulta = "";

            Console.WriteLine("========== CONSULTANDO PRODUTO ==========");
            Console.Write("Digite o Nome do produto:");
            produtoConsulta = Console.ReadLine();

            int idProdutoConsultado = 0;

            //Produto1 produto = listaProdutos.Value.Where(x => x.Nome == produtoConsulta).FirstOrDefault();

            if (ValidarProdutoConsulta(produtoConsulta, ref idProdutoConsultado) != false)
            {
                Produto1 produtoConsultado = listaProdutos[idProdutoConsultado];

                Console.WriteLine("");
                Console.WriteLine("===== INFORMAÇÕES DO PRODUTO =====");
                Console.WriteLine($"Nome: {produtoConsultado.Nome}");
                Console.WriteLine($"ID: {produtoConsultado.IdProduto}");
                Console.WriteLine($"Categoria: {produtoConsultado.CategoriaProdutoId}");
                Console.WriteLine($"Quantidade: {produtoConsultado.QuantidadeProduto}");
                Console.WriteLine("==================================");
                Console.WriteLine("");
                Console.ReadKey();
            }
            Console.ReadKey();
        }

        //Salvando Informações
        public void SalvarInfo()
        {
            SalvarInformacoesDoBD_Produtos();
            SalvarInformacoesDoBD_HistoricoProduto();
        }

        //Excluir Produto
        public void ExcluirProduto()
        {
            string produtoExcluir = "";

            Console.WriteLine("========== EXCLUINDO PRODUTO ==========");
            ListarProdutos();
            Console.Write("Digite o nome do produto que você deseja Excluir: ");

            if (ValidarProdutoExcluir(ref produtoExcluir) == true)
            {
                KeyValuePair<int, Produto1> produtoList = listaProdutos.Where(x => x.Value.Nome == produtoExcluir).FirstOrDefault();
                var produto = produtoList.Value;
                produto.DataExclusao = DateTime.Now;
                int contador = 0;
                string[] historico = new string[]
                {
                    $"{produto.IdProduto}",
                    $"{produto.Nome}",
                    $"{produto.CategoriaProdutoId}",
                    $"{produto.QuantidadeProduto}",
                    $"{produto.DataCadastro}",
                    $"{produto.DataExclusao}"
                };

                foreach (string[] produtoRegistro in historicoProdutos)
                {
                    if ($"{produto.IdProduto}" == produtoRegistro[0])
                    {
                        historicoProdutos[contador] = historico;
                        break;
                    }
                    contador++;
                }

                listaProdutos.Remove(produto.IdProduto);
                SalvarInformacoesDoBD_Produtos();
                SalvarInformacoesDoBD_HistoricoProduto();
                Console.WriteLine("[AVISO!] Produto Excluído com Sucesso!");
                Console.ReadKey();
            }
            Console.ReadKey();
        }

        //Exibir Todas as Categorias
        private void ExibirTodasAsCategorias(CrudCategoriaProduto listaCategoriaProduto)
        {
            Console.WriteLine("========== LISTA DE CATEGORIAS ==========");
            foreach (var categoriaProduto in listaCategoriaProduto.ExibirListaCategoriaProduto())
            {
                Console.WriteLine($"[{categoriaProduto.Value.IdCategoriaProduto}] {categoriaProduto.Value.NomeCategoriaProduto}");
            }
            Console.WriteLine("=========================================");
            Console.WriteLine("");
            Console.ReadKey();
        }

        //Salvando Informações no Banco de Dados

        private void SalvarInformacoesDoBD_Produtos()
        {
            if (!File.Exists(caminhoBD_Produtos))
            {
                File.Delete(caminhoBD_Produtos);
            }

            using (Stream saida = File.Open(caminhoBD_Produtos, FileMode.Create))
            {
                using (StreamWriter escritor = new StreamWriter(saida))
                {
                    foreach (KeyValuePair<int, Produto1> produto in listaProdutos)
                    {
                        escritor.WriteLine(produto.Value.ToString());
                    }
                }
            }
        }

        private void SalvarInformacoesDoBD_HistoricoProduto()
        {
            if (!File.Exists(caminhoBD_HistoricoProduto))
            {
                File.Delete(caminhoBD_HistoricoProduto);
            }

            using (Stream saida = File.Open(caminhoBD_HistoricoProduto, FileMode.Create))
            {
                using (StreamWriter escritor = new StreamWriter(saida))
                {
                    foreach (string[] produto in historicoProdutos)
                    {
                        escritor.WriteLine($"{produto[0]}|{produto[1]}|{produto[2]}|{produto[3]}|{produto[4]}|{produto[5]}");
                    }
                }
            }
        }

        //Validações
        private bool ValidarProdutoExcluir(ref string produto)
        {
            bool produtoCadastrado = false;

            try
            {
                produto = Console.ReadLine();

                foreach (var pair in listaProdutos)
                {
                    if (pair.Value.Nome == produto.ToLower())
                    {
                        produtoCadastrado = true;
                    }
                }

                if (produtoCadastrado == false)
                {
                    throw new ProdutoNaoCadastradoException();
                }

                return true;
            }
            catch (ProdutoNaoCadastradoException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarProdutoConsulta(string produtoConsulta, ref int idProdutoConsultado)
        {
            bool produtoEncontrado = false;
            try
            {
                foreach (KeyValuePair<int, Produto1> produto in listaProdutos)
                {
                    if (produto.Value.Nome == produtoConsulta)
                    {
                        produtoEncontrado = true;
                        idProdutoConsultado = produto.Key;
                        break;
                    }
                }

                if (produtoEncontrado == false)
                {
                    throw new ProdutoNaoCadastradoException();
                }

                return true;
            }
            catch(ProdutoNaoCadastradoException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private bool ValidarNomeProduto(string nomeProduto)
        {
            try
            {
                if (nomeProduto.Length < 3 || nomeProduto == "" || nomeProduto == " ")
                {
                    throw new NomeInvalidoException("[ERRO!] Nome Inválido!");
                }
                return true;
            }
            catch (NomeInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool ValidarSeHaCategoriasCadastradas(CrudCategoriaProduto listaCategoriasProduto)
        {
            try
            {
                if (listaCategoriasProduto.ExibirListaCategoriaProduto().Count > 0)
                {
                    return true;
                }

                else
                {
                    throw new SemCategoriasCadastradasException();
                }


            }
            catch (SemCategoriasCadastradasException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarCategoriaProduto(ref int categoriaProdutoId, Dictionary<int, CategoriaProduto> listaCategoriasProduto)
        {
            bool categoriaValida = false;

            try
            {
                categoriaProdutoId = Int32.Parse(Console.ReadLine());

                foreach (KeyValuePair<int, CategoriaProduto> categoria in listaCategoriasProduto)
                {
                    if (categoriaProdutoId == categoria.Key)
                    {
                        categoriaValida = true;
                    }
                }

                if (categoriaValida == false)
                {
                    throw new CategoriaInvalidaException();
                }

                return true;
            }
            catch (CategoriaInvalidaException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Carregando Dados
        private void CarregarDados_Produto()
        {
            if (!File.Exists(caminhoBD_Produtos))
            {
                using (Stream criandoBD = File.Open(caminhoBD_Produtos, FileMode.Create))
                {

                }
            }
            else
            {
                Dictionary<int, Produto1> listaProdutosTemp = new Dictionary<int, Produto1>();

                using (Stream bancoDeDados = File.Open(caminhoBD_Produtos, FileMode.Open))
                {
                    using (StreamReader leitor = new(bancoDeDados))
                    {
                        string linha = leitor.ReadLine();

                        while (linha != null)
                        {
                            int IdProduto = -1;
                            DateTime dataCadastro;
                            DateTime dataExclusao;

                            bool idProdutoExiste = false;

                            string[] produtoString = linha.Split(";");

                            do
                            {
                                Produto1 produto = new Produto1(produtoString[1], Int32.Parse(produtoString[2]), Int32.Parse(produtoString[3]));

                                if (produto.IdProduto == Int32.Parse(produtoString[0]))
                                {
                                    IdProduto = produto.IdProduto;
                                    idProdutoExiste = true;
                                    listaProdutosTemp.Add(produto.IdProduto, produto);
                                }                             
                            } while (idProdutoExiste == false);

                            dataCadastro = DateTime.Parse(produtoString[5]);
                            dataExclusao = DateTime.Parse(produtoString[6]);

                            if (IdProduto != -1)
                            {
                                listaProdutosTemp[IdProduto].AdicionarDataCadastro(dataCadastro);
                                listaProdutosTemp[IdProduto].AdicionarDataExclusao(dataExclusao);
                            }

                            linha = leitor.ReadLine();
                        }
                    }
                }

                listaProdutos = listaProdutosTemp;
            }
        }

        private void CarregarDados_HistoricoProduto()
        {
            if (!File.Exists(caminhoBD_HistoricoProduto))
            {
                using (Stream criandoBD = File.Open(caminhoBD_HistoricoProduto, FileMode.Create))
                {

                }
            }
            else
            {
                List<string[]> historicoProdutosTemp = new List<string[]>();

                using (Stream bancoDeDados = File.Open(caminhoBD_HistoricoProduto, FileMode.Open))
                {
                    using (StreamReader leitor = new(bancoDeDados))
                    {
                        string linha = leitor.ReadLine();

                        while (linha != null)
                        {
                            string[] historicoProdutoString = linha.Split("|");

                            string[] historicoTemp = new string[]
                            {
                                historicoProdutoString[0],
                                historicoProdutoString[1],
                                historicoProdutoString[2],
                                historicoProdutoString[3],
                                historicoProdutoString[4]
                            };

                            historicoProdutosTemp.Add(historicoProdutoString);

                            linha = leitor.ReadLine();
                        }
                    }
                }

                historicoProdutos = historicoProdutosTemp;
            }
        }
    }
}