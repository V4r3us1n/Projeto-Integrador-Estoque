using System;
using Estoque.Classes;
using System.Collections.Generic;
using Estoque.Classes.Entidades;
using Estoque.Classes.Exceptions;

namespace Estoque.ConsoleApp
{
    class Program
    {
        static UserAdiministrador userRoot = new UserAdiministrador("root", "root");
        static CrudCategoriaProduto listaCategorias = new CrudCategoriaProduto();
        static CrudFuncionario listaFuncionarios = new CrudFuncionario();
        static CrudProduto listaProdutos = new CrudProduto();
        static HistoricoMovimentoEstoque estoque = new HistoricoMovimentoEstoque();

        static void Main(string[] args)
        {
            bool sairDoPrograma = false;
            string nomeUsuario = "";
            string senhaUsuario = "";
            bool sairMenuPrincipal = false;
            int opcaoMenuPrincipal = 0;
            bool sairMenuEstoque = false;
            int opcaoMenuEstoque = 0;

            DateTime dataTeste = new DateTime(2024, 10, 13);

            Console.Clear();

            //PASSANDO INFORMAÇÕES SOBRE O USUÁRIO ROOT
            ExibirDadosUsuarioRoot();
            Console.ReadKey();


            do
            {
                //EXIBINDO MENU LOGIN
                ExibirMenuLogin(ref nomeUsuario, ref senhaUsuario);
                Console.Clear();
                Console.Clear();

                if (ValidarUsuario(ref nomeUsuario, ref senhaUsuario) == true)
                {
                    if (nomeUsuario.ToLower() == userRoot.NomeUsuario)
                    {
                        do
                        {
                            sairMenuPrincipal = false;

                            do
                            {
                                ExibirMenuPrincipalRoot();
                                Console.Write("Digite a Opção Desejada: ");
                            } while (ValidarOpcao(ref opcaoMenuPrincipal, 6) == false);
                            Console.Clear();

                            switch (opcaoMenuPrincipal)
                            {
                                case 1:
                                    sairMenuEstoque = false;
                                    opcaoMenuEstoque = 0;

                                    //Redirecionando para Menu Estoque
                                    Console.WriteLine("[AVISO!] Você está sendo Redirecionado para o Menu Estoque...");
                                    Console.ReadKey();
                                    do
                                    {
                                        do
                                        {
                                            ExibirMenuEstoque();
                                            Console.Write("Digite a Opção Desejada: ");
                                        } while (ValidarOpcao(ref opcaoMenuEstoque, 3) == false);

                                        switch (opcaoMenuEstoque)
                                        {
                                            case 1:
                                                //Movimento de Entrada no Estoque
                                                estoque.EntradaEstoque(listaProdutos);
                                                Console.ReadKey();

                                                break;
                                            case 2:
                                                //Movimento de Saída no Estoque
                                                estoque.SaidaEstoque(listaProdutos);
                                                Console.ReadKey();

                                                break;
                                            case 3:
                                                //Voltando ao Menu Principal
                                                Console.WriteLine("[AVISO!] Você será redirecionado ao Menu Principal...");
                                                Console.ReadKey();
                                                sairMenuEstoque = true;

                                                break;
                                        }
                                        Console.ReadKey();
                                        Console.Clear();
                                    } while (sairMenuEstoque == false);

                                    break;
                                case 2:
                                    bool sairMenuFuncionario = false;
                                    int opcaoMenuFuncionario = 0;

                                    //Redirecionando para Menu Funcionários
                                    Console.WriteLine("[AVISO!] Você está sendo Redirecionado para o Menu Funcionários...");
                                    Console.ReadKey();
                                    do
                                    {
                                        do
                                        {
                                            ExibirMenuFuncionarios();
                                            Console.Write("Digite a Opção Desejada: ");
                                        } while (ValidarOpcao(ref opcaoMenuFuncionario, 5) == false);

                                        switch (opcaoMenuFuncionario)
                                        {
                                            case 1:
                                                //Adicionando Novo Funcionário
                                                listaFuncionarios.AdicionarUserFuncionario();

                                                break;
                                            case 2:
                                                //Listando Todos os Funcionários
                                                listaFuncionarios.ListarFuncionarios();

                                                break;
                                            case 3:
                                                //Consultando Funcionário
                                                listaFuncionarios.ConsultarFuncionario();

                                                break;
                                            case 4:
                                                //Excluindo Funcionário
                                                listaFuncionarios.ExcluirFuncionario();

                                                break;
                                            case 5:
                                                //Voltando ao Menu Principal
                                                Console.WriteLine("[AVISO!] Você será redirecionado ao Menu Principal...");
                                                Console.ReadKey();
                                                sairMenuFuncionario = true;

                                                break;
                                        }
                                        Console.Clear();
                                    } while (sairMenuFuncionario == false);

                                    break;
                                case 3:
                                    bool sairMenuProduto = false;
                                    int opcaoMenuProduto = 0;

                                    //Redirecionando para Menu Produtos
                                    Console.WriteLine("[AVISO!] Você está sendo Redirecionado para o Menu Produtos...");
                                    Console.ReadKey();

                                    do
                                    {
                                        do
                                        {
                                            ExibirMenuProdutos();
                                            Console.Write("Digite a Opção Desejada: ");
                                        } while (ValidarOpcao(ref opcaoMenuProduto, 5) == false);

                                        switch (opcaoMenuProduto)
                                        {
                                            case 1:
                                                //Adicionando Novo Produto...

                                                if (listaProdutos.ValidarSeHaCategoriasCadastradas(listaCategorias))
                                                {
                                                    listaProdutos.CadastrarProduto(listaCategorias);
                                                    break;
                                                }

                                                else
                                                {
                                                    Console.WriteLine("Nenhuma Categoria foi cadastrada, redirecionando para cadastro de categoria...\n");
                                                    listaCategorias.CadastrarCategoriaProduto();
                                                    listaProdutos.CadastrarProduto(listaCategorias);
                                                }

                                                break;
                                            case 2:
                                                //Listando Produtos...
                                                listaProdutos.ListarProdutos();

                                                break;
                                            case 3:
                                                //Consultando Produto...
                                                listaProdutos.ConsultarProduto();

                                                break;
                                            case 4:
                                                //Excluindo Produto...
                                                listaProdutos.ExcluirProduto();

                                                break;
                                            case 5:
                                                //Voltando ao Menu Principal
                                                Console.WriteLine("[AVISO!] Você será redirecionado ao Menu Principal...");
                                                Console.ReadKey();
                                                sairMenuProduto = true;

                                                break;
                                        }
                                        Console.Clear();
                                    } while (sairMenuProduto == false);

                                    break;
                                case 4:
                                    bool sairMenuCategoriaProduto = false;
                                    int opcaoMenuCategoriaProduto = 0;

                                    //Redirecionando para Menu Categoria
                                    Console.WriteLine("[AVISO!] Você está sendo Redirecionado para o Menu Categoria...");
                                    Console.ReadKey();

                                    do
                                    {
                                        do
                                        {
                                            ExibirMenuCategoriaProduto();
                                            Console.Write("Digite a Opção Desejada: ");
                                        } while (ValidarOpcao(ref opcaoMenuCategoriaProduto, 5) == false);

                                        switch (opcaoMenuCategoriaProduto)
                                        {
                                            case 1:
                                                //Adicionando Novo Produto...
                                                listaCategorias.CadastrarCategoriaProduto();

                                                break;
                                            case 2:
                                                //Listando Produtos...
                                                listaCategorias.ListarCategoriaProduto();

                                                break;
                                            case 3:
                                                //Consultando Produto...
                                                listaCategorias.ConsultarCategoriaProduto(listaProdutos);

                                                break;
                                            case 4:
                                                //Excluindo Produto...
                                                listaCategorias.ExcluirCategoriaProduto(listaProdutos.ExibirListaProdutos());

                                                break;
                                            case 5:
                                                //Voltando ao Menu Principal
                                                Console.WriteLine("[AVISO!] Você será redirecionado ao Menu Principal...");
                                                Console.ReadKey();
                                                sairMenuCategoriaProduto = true;

                                                break;
                                        }
                                        Console.Clear();
                                    } while (sairMenuCategoriaProduto == false);

                                    break;
                                case 5:
                                    bool sairMenuRelatorio = false;
                                    int opcaoMenuRelatorio = 0;
                                    //Redirecionando Para Menu Relatório
                                    do
                                    {
                                        do
                                        {
                                            ExibirMenuRelatorio();
                                            Console.Write("Digite a Opção Desejada: ");
                                        } while (ValidarOpcao(ref opcaoMenuRelatorio, 5) == false);

                                        switch (opcaoMenuRelatorio)
                                        {
                                            case 1:
                                                //Exibindo Relatório de Estoque...
                                                estoque.GerarHistoricoEntrada();

                                                break;
                                            case 2:
                                                //Exibindo Relatório de Estoque...

                                                estoque.GerarHistoricoSaida();

                                                break;

                                            case 3:
                                                // //Exibindo Relatório de Funcionários...
                                                listaFuncionarios.GeraRelatorioFuncionario();
                                                break;

                                            case 4:
                                                estoque.GeraRelatorioGeral(listaProdutos, listaCategorias);
                                                break;

                                            case 5:
                                                //Voltando ao Menu Principal
                                                Console.WriteLine("[AVISO!] Você será redirecionado ao Menu Principal... ");
                                                Console.ReadKey();
                                                sairMenuRelatorio = true;
                                                break;

                                        }
                                        Console.Clear();
                                    } while (sairMenuRelatorio == false);

                                    break;
                                case 6:
                                    ////Redirecionando para Menu Login
                                    Console.WriteLine("[AVISO!] Saindo...");
                                    sairMenuPrincipal = true;

                                    break;
                            }
                            Console.Clear();

                        } while (sairMenuPrincipal == false);
                    }
                    else
                    {
                        do
                        {
                            do
                            {
                                ExibirMenuPrincipal();
                                Console.Write("Digite a Opção Desejada: ");
                            } while (ValidarOpcao(ref opcaoMenuPrincipal, 2) == false);

                            switch (opcaoMenuPrincipal)
                            {
                                case 1:
                                    sairMenuPrincipal = false;
                                    sairMenuEstoque = false;
                                    opcaoMenuEstoque = 0;

                                    //Redirecionando para Menu Estoque
                                    Console.WriteLine("[AVISO!] Você está sendo Redirecionado para o Menu Estoque...");
                                    Console.ReadKey();
                                    do
                                    {
                                        do
                                        {
                                            ExibirMenuEstoque();
                                            Console.Write("Digite a Opção Desejada: ");
                                        } while (ValidarOpcao(ref opcaoMenuEstoque, 3) == false);

                                        switch (opcaoMenuEstoque)
                                        {
                                            case 1:
                                                //Movimento de Entrada no Estoque
                                                estoque.EntradaEstoque(listaProdutos);

                                                break;
                                            case 2:
                                                //Movimento de Saída no Estoque
                                                estoque.SaidaEstoque(listaProdutos);

                                                break;
                                            case 3:
                                                //Voltando ao Menu Principal
                                                Console.WriteLine("[AVISO!] Você será redirecionado ao Menu Principal...");
                                                Console.ReadKey();
                                                sairMenuEstoque = true;

                                                break;
                                        }
                                        Console.Clear();
                                    } while (sairMenuEstoque == false);

                                    break;
                                case 2:
                                    ////Redirecionando para Menu Login
                                    Console.WriteLine("[AVISO!] Saindo...");
                                    sairMenuPrincipal = true;

                                    break;
                            }
                            Console.Clear();
                        } while (sairMenuPrincipal == false);
                    }
                }

            } while (sairDoPrograma == false);
        }

        //Exibindo Dados Usuário Root
        static void ExibirDadosUsuarioRoot()
            {
                Console.WriteLine("=============== ATENÇÃO ===============");
                Console.WriteLine("| Usuário Adiministrador: root        |");
                Console.WriteLine("| Senha Usuário Administrador: root   |");
                Console.WriteLine("=======================================");
            }

        //EXIBINDO MENUS
        static void ExibirMenuLogin(ref string nomeUsuario, ref string senhaUsuario)
        {
            Console.WriteLine("===== LOGIN ESTOQUE =====");
            Console.Write("Usuário: ");
            nomeUsuario = Console.ReadLine();
            Console.Write("Senha: ");
            senhaUsuario = Console.ReadLine();
            Console.WriteLine("=========================");
            Console.WriteLine("");
        }

        static void ExibirMenuPrincipalRoot()
        {
            Console.WriteLine("\n========== MENU PRINCIPAL ==========");
            Console.WriteLine("||  [1] Estoque                   ||");
            Console.WriteLine("||  [2] Funcionários              ||");
            Console.WriteLine("||  [3] Produtos                  ||");
            Console.WriteLine("||  [4] Categorias de Produto     ||");
            Console.WriteLine("||  [5] Relatórios                ||");
            Console.WriteLine("||  [6] Sair                      ||");
            Console.WriteLine("====================================\n");
        }

        static void ExibirMenuPrincipal()
        {
            Console.WriteLine("\n===== MENU PRINCIPAL =====");
            Console.WriteLine("|| [1] Estoque          ||");
            Console.WriteLine("|| [2] Sair             ||");
            Console.WriteLine("==========================\n");
        }

        static void ExibirMenuEstoque()
        {
            Console.WriteLine("\n========== MENU ESTOQUE ==========");
            Console.WriteLine("|| [1] Entradade Estoque        ||");
            Console.WriteLine("|| [2] Saída de Estoque         ||");
            Console.WriteLine("|| [3] Voltar                   ||");
            Console.WriteLine("==================================\n");
        }

        static void ExibirMenuFuncionarios()
        {
            Console.WriteLine("\n========== MENU FUNCIONÁRIO ==========");
            Console.WriteLine("|| [1] Adicionar Funcionário        ||");
            Console.WriteLine("|| [2] Listar Funcionários          ||");
            Console.WriteLine("|| [3] Consultar Funcionário        ||");
            Console.WriteLine("|| [4] Excluir Funcionário          ||");
            Console.WriteLine("|| [5] Voltar                       ||");
            Console.WriteLine("======================================\n");
        }

        static void ExibirMenuProdutos()
        {
            Console.WriteLine("\n========== MENU PRODUTO ==========");
            Console.WriteLine("|| [1] Adicionar Produto        ||");
            Console.WriteLine("|| [2] Listar Produtos          ||");
            Console.WriteLine("|| [3] Consultar Produto        ||");
            Console.WriteLine("|| [4] Excluir Produto          ||");
            Console.WriteLine("|| [5] Voltar                   ||");
            Console.WriteLine("==================================\n");
        }

        static void ExibirMenuCategoriaProduto()
        {
            Console.WriteLine("\n========== MENU CATEGORIA ==========");
            Console.WriteLine("|| [1] Adicionar Categoria        ||");
            Console.WriteLine("|| [2] Listar Categorias          ||");
            Console.WriteLine("|| [3] Consultar Categoria        ||");
            Console.WriteLine("|| [4] Excluir Categoria          ||");
            Console.WriteLine("|| [5] Voltar                     ||");
            Console.WriteLine("====================================\n");
        }

        static void ExibirMenuRelatorio()
        {
            Console.WriteLine("\n=============== MENU RELATÓRIO ===============");
            Console.WriteLine("|| [1] Relatório de Estoque de Entrada        ||");
            Console.WriteLine("|| [2] Relatório de Estoque de Saída          ||");
            Console.WriteLine("|| [3] Relatório de Funcionários              ||");
            Console.WriteLine("|| [4] Relatório Geral                        ||");
            Console.WriteLine("|| [5] Voltar                                 ||");
            Console.WriteLine("================================================\n");
        }

        //VALIDANDO INFORMAÇÕES
        static bool ValidarUsuario(ref string nomeUsuario, ref string senhaUsuario)
        {
            bool usuarioCadastrado = false;
            try
            {
                if (nomeUsuario != "root" || senhaUsuario != "root")
                {
                    foreach (KeyValuePair<string, UserFuncionario> pair in listaFuncionarios.ExibirListaFuncionarios())
                    {
                        if (nomeUsuario.ToLower() == pair.Key.ToLower() && senhaUsuario == pair.Value.Senha)
                        {
                            usuarioCadastrado = true;
                        }
                    }

                    if (usuarioCadastrado == false)
                    {
                        throw new UsuarioNaoCadastradoException();
                    }
                }

                return true;
            }
            catch (UsuarioNaoCadastradoException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        static bool ValidarOpcao(ref int opcao, int opcaoMaxima)
        {
            int opcaoMinima = 1;

            try
            {
                opcao = Int32.Parse(Console.ReadLine());

                if (opcao < opcaoMinima || opcao > opcaoMaxima)
                {
                    throw new ArgumentOutOfRangeException("[ERRO!] OPÇÃO INVÁLIDA!");
                }

                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
