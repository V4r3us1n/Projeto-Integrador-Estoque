using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Estoque.Classes.Entidades;
using Estoque.Classes.Exceptions;
using System.IO;

namespace Estoque.Classes
{
    public class CrudCategoriaProduto
    {
        private Dictionary<int, CategoriaProduto> listaCategoriaProduto = new Dictionary<int, CategoriaProduto>();
        private List<string[]> historicoCategoria = new List<string[]>();

        public CrudCategoriaProduto()
        {
            CarregarDadosCategoriaProduto();
            CarregarDadosHistoricoCategoriaProduto();
        }
        private string caminhoBD = "BancoDeDados/BD_CategoriaProduto.txt";
        private string caminhoBD_Historico = "BancoDeDados/BD_HistoricoCategoriaProduto.txt";

        public Dictionary<int, CategoriaProduto> ExibirListaCategoriaProduto()
        {
            return listaCategoriaProduto;
        }

        public List<string[]> RelatorioCategoria()
        {
            return historicoCategoria;
        }
        //Cadastrar Categoria Produto
        public bool CadastrarCategoriaProduto()
        {
            string nomeCategoriaProduto = "";
            Console.Write("Digite o Nome da Nova Categoria de Produto: ");
            nomeCategoriaProduto = Console.ReadLine();
            if (ValidarNomeCategoriaProduto(nomeCategoriaProduto) != false)
            {
                if (ValidarCadastroCategoriaProduto(nomeCategoriaProduto) != false)
                {
                    CategoriaProduto novaCategoriaProduto = new CategoriaProduto(nomeCategoriaProduto);
                    listaCategoriaProduto.Add(novaCategoriaProduto.IdCategoriaProduto, novaCategoriaProduto);
                    string[] historico = new string[] {$"{novaCategoriaProduto.IdCategoriaProduto}", $"{novaCategoriaProduto.NomeCategoriaProduto}", $"{novaCategoriaProduto.DataCadastro}", $"{novaCategoriaProduto.DataExclusao}"};
                    historicoCategoria.Add(historico);
                    Console.WriteLine("[AVISO!] Categoria Cadastrada com Sucesso!");

                    SalvarInformacoesDoHistorico();
                    SalvarInformacoesDoBanco();
                    Console.ReadKey();
                    return true;
                }
            }
            Console.ReadKey();
            return false;
        }

        //Listar Categorias de Produto
        public void ListarCategoriaProduto()
        {
            Console.WriteLine("========== LISTA DE CATEGORIAS ==========");
            foreach (KeyValuePair<int, CategoriaProduto> categoriaProduto in listaCategoriaProduto)
            {
                Console.WriteLine($"ID: {categoriaProduto.Key}");
                Console.WriteLine($"Nome: {categoriaProduto.Value.NomeCategoriaProduto}");
                Console.WriteLine("=========================================");
            }
            Console.WriteLine("");
            Console.ReadKey();
        }

        //Consultar Categoria Produto
        public bool ConsultarCategoriaProduto(CrudProduto listaProdutos)
        {
            int idCategoriaProdutoConsulta = 0;
            Console.Write("Digite o ID da Categoria que você Deseja Consultar: ");
            if (ValidarCategoriaProduto(ref idCategoriaProdutoConsulta, listaCategoriaProduto) != false)
            {
                Console.WriteLine("========== INFO. CATEGORIA CONSULTADA ==========");
                Console.WriteLine($"Id: {listaCategoriaProduto[idCategoriaProdutoConsulta].IdCategoriaProduto}");
                Console.WriteLine($"Nome: {listaCategoriaProduto[idCategoriaProdutoConsulta].NomeCategoriaProduto}");
                Console.WriteLine($"Quantidade de Produtos na Categoria: {CalcularQuantidadeDeProdutosNaCategoria(idCategoriaProdutoConsulta, listaProdutos)}");
                Console.WriteLine("================================================");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("ID inválido!");
            }
            Console.ReadKey();
            return false;
        }

        //Excluir Categoria Produto
        public bool ExcluirCategoriaProduto(Dictionary<int, Produto1> listaProdutos)
        {
            string categoriaExcluir = "";

            Console.WriteLine("========== EXCLUINDO CATEGORIA ==========");
            ListarCategoriaProduto();
            Console.Write("Digite o nome da categoria que você deseja Excluir: ");

            if (ValidarCategoriaExcluir(ref categoriaExcluir, listaProdutos) == true)
            {
                KeyValuePair<int, CategoriaProduto> categList = listaCategoriaProduto.Where(x => x.Value.NomeCategoriaProduto == categoriaExcluir).FirstOrDefault();
                if (!ValidarSeCategoriaNaoTemProdutos(categList.Value.IdCategoriaProduto, listaProdutos) == true)
                {
                    categList.Value.DataExclusao = DateTime.Now;
                    string[] historico = new string[] 
                    {
                        $"{categList.Value.IdCategoriaProduto}",
                        $"{categList.Value.NomeCategoriaProduto}",
                        $"{categList.Value.DataCadastro}",
                        $"{categList.Value.DataExclusao}"
                    };
                    
                    int posicaoCategoriaExcluir = -1;
                    int contador = 0;

                    foreach (string[] categoria in historicoCategoria)
                    {
                        if (categoria[0] == historico[0])
                        {
                            posicaoCategoriaExcluir = contador;
                            break;
                        }
                        contador++;
                    }

                    historicoCategoria[posicaoCategoriaExcluir] = historico;

                    listaCategoriaProduto.Remove(categList.Key);
                    Console.Write("[AVISO] Categoria removida com sucesso.");

                    SalvarInformacoesDoHistorico();
                    SalvarInformacoesDoBanco();
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    Console.Write("[AVISO] A categoria não pode ser removida pois há produtos cadastrados.");
                }
                
            }
            Console.ReadKey();
            return false;
        }

        private bool ValidarCategoriaExcluir(ref string categoria, Dictionary<int, Produto1> listaProdutos)
        {
            bool categoriaCadastrada = false;

            try
            {
                categoria = Console.ReadLine();
                int IdCategoria = -1;

                foreach (var pair in listaCategoriaProduto)
                {
                    if (pair.Value.NomeCategoriaProduto == categoria.ToLower())
                    {
                        IdCategoria = pair.Key;
                        categoriaCadastrada = true;
                    }
                }

                if (categoriaCadastrada == false)
                {
                    throw new CategoriaNaoCadastradaException();
                }
                else
                {
                    foreach (KeyValuePair<int, Produto1> produto in listaProdutos)
                    {
                        if (produto.Value.CategoriaProdutoId == IdCategoria)
                        {
                            throw new ProdutosCadastradosNaCategoriaException();
                        }
                    }
                }

                return true;
            }
            catch (CategoriaNaoCadastradaException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ProdutosCadastradosNaCategoriaException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Calcular Quantidade de Produtos Dentro da Categoria
        private int CalcularQuantidadeDeProdutosNaCategoria(int idCategoriaProduto, CrudProduto listaProdutos)
        {
            int quantidadeProdutosNaCategoria = 0;

            foreach (KeyValuePair<int, Produto1> produto in listaProdutos.ExibirListaProdutos())
            {
                if (produto.Value.CategoriaProdutoId == idCategoriaProduto)
                {
                    quantidadeProdutosNaCategoria++;
                }
            }

            return quantidadeProdutosNaCategoria;
        }

        //Validações

        static bool ValidarOpcao(ref int opcao, int opcaoMaxima)
        {
            int opcaoMinima = 1;

            try
            {
                opcao = Int32.Parse(Console.ReadLine());

                if (opcao < opcaoMinima || opcao > opcaoMaxima)
                {
                    throw new OpcaoInvalidaException();
                }

                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (OpcaoInvalidaException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarNomeCategoriaProduto(string nomeCategoriaProduto)
        {
            try
            {
                if (nomeCategoriaProduto is null || nomeCategoriaProduto == "" || nomeCategoriaProduto == " " || nomeCategoriaProduto.Length < 3)
                {
                    throw new FormatException();
                }

                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarCadastroCategoriaProduto(string nomeCategoriaProduto)
        {
            try
            {
                foreach (var categoriaProduto in listaCategoriaProduto)
                {
                    if (nomeCategoriaProduto == categoriaProduto.Value.NomeCategoriaProduto)
                    {
                        throw new CategoriaJaCadastradaException();
                    }
                }

                return true;
            }
            catch (CategoriaJaCadastradaException ex)
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
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (CategoriaInvalidaException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarSeCategoriaNaoTemProdutos(int idCategoria, Dictionary<int, Produto1> listaProdutos)
        {
            var prod = listaProdutos.Where(x => x.Key == idCategoria);

            if (prod.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Salvar Informações no Banco

        private void SalvarInformacoesDoBanco()
        {
            if (!File.Exists(caminhoBD))
            {
                File.Delete(caminhoBD);
            }

            using (Stream saida = File.Open(caminhoBD, FileMode.Create))
            {
                using (StreamWriter escritor = new StreamWriter(saida))
                {
                    foreach (KeyValuePair<int, CategoriaProduto> categoria in listaCategoriaProduto)
                    {
                        escritor.WriteLine(categoria.Value.ToString());
                    }
                }
            }
        }

        private void SalvarInformacoesDoHistorico()
        {
            if (!File.Exists(caminhoBD_Historico))
            {
                File.Delete(caminhoBD_Historico);
            }

            using (Stream saida = File.Open(caminhoBD_Historico, FileMode.Create))
            {
                using (StreamWriter escritor = new StreamWriter(saida))
                {
                    foreach (string[] categoria in historicoCategoria)
                    {
                        escritor.WriteLine($"{categoria[0]}|{categoria[1]}|{categoria[2]}|{categoria[3]}");
                    }
                }
            }
        }

        //Carregar Dados do Banco de Dados
        private void CarregarDadosCategoriaProduto()
        {
            if (!File.Exists(caminhoBD))
            {
                using (Stream criandoBD = File.Open(caminhoBD, FileMode.Create))
                {

                }
            }
            else
            {
                Dictionary<int, CategoriaProduto> listaCategoriaProdutoTemp = new Dictionary<int, CategoriaProduto>();

                using (Stream bancoDeDados = File.Open(caminhoBD, FileMode.Open))
                {
                    using (StreamReader leitor = new(bancoDeDados))
                    {
                        string linha = leitor.ReadLine();

                        while (linha != null)
                        {
                            int IdCategoria = -1;
                            DateTime dataCadastro;
                            DateTime dataExclusao;

                            bool idCategoriaExiste = false;

                            string[] categoriaString = linha.Split(";");

                            do
                            {
                                CategoriaProduto categoriaProduto = new CategoriaProduto(categoriaString[1]);

                                if (categoriaProduto.IdCategoriaProduto == Int32.Parse(categoriaString[0]))
                                {
                                    IdCategoria = categoriaProduto.IdCategoriaProduto;
                                    idCategoriaExiste = true;
                                    listaCategoriaProdutoTemp.Add(categoriaProduto.IdCategoriaProduto, categoriaProduto);
                                }                             
                            } while (idCategoriaExiste == false);

                            dataCadastro = DateTime.Parse(categoriaString[3]);
                            dataExclusao = DateTime.Parse(categoriaString[4]);

                            if (IdCategoria != -1)
                            {
                                listaCategoriaProdutoTemp[IdCategoria].AdicionarDataCadastro(dataCadastro);
                                listaCategoriaProdutoTemp[IdCategoria].AdicionarDataExclusao(dataExclusao);
                            }

                            linha = leitor.ReadLine();
                        }
                    }
                }

                listaCategoriaProduto = listaCategoriaProdutoTemp;
            }
        }

        private void CarregarDadosHistoricoCategoriaProduto()
        {
            if (!File.Exists(caminhoBD_Historico))
            {
                using (Stream criandoBD = File.Open(caminhoBD_Historico, FileMode.Create))
                {

                }
            }
            else
            {
                List<string[]> historicoCategoriaTemp = new List<string[]>();

                using (Stream bancoDeDados = File.Open(caminhoBD_Historico, FileMode.Open))
                {
                    using (StreamReader leitor = new(bancoDeDados))
                    {
                        string linha = leitor.ReadLine();

                        while (linha != null)
                        {
                            string[] historicoCategoriaString = linha.Split("|");

                            string[] historicoTemp = new string[]
                            {
                                historicoCategoriaString[0],
                                historicoCategoriaString[1],
                                historicoCategoriaString[2],
                                historicoCategoriaString[3]
                            };

                            historicoCategoriaTemp.Add(historicoCategoriaString);

                            linha = leitor.ReadLine();
                        }
                    }
                }

                historicoCategoria = historicoCategoriaTemp;
            }
        }
    }
}