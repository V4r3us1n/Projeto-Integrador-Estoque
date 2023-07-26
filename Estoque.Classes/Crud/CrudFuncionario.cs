using Estoque.Classes.Entidades;
using Estoque.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Estoque.Classes
{
    public class CrudFuncionario
    {
        private Dictionary<string, UserFuncionario> listaFuncionarios = new Dictionary<string, UserFuncionario>();

        public CrudFuncionario()
        {
            CarregarDadosFuncionarios();
        }
        public Dictionary<string, UserFuncionario> ExibirListaFuncionarios()
        {
            return listaFuncionarios;
        }

        private string caminhoBD_Funcionarios = "BancoDeDados/BD_Funcionarios.txt";

        //Gerando Relatório
        public void GeraRelatorioFuncionario()
        {
            if (listaFuncionarios.Count == 0)
            {
                Console.WriteLine("[AVISO] Nenhum funcionário foi cadastrado.");

            }
            else
            {
                foreach (var item in listaFuncionarios)
                {
                    Console.WriteLine("==================RELATÓRIO===============");
                    Console.WriteLine($"Nome: {item.Value.NomeUsuario}");
                    Console.WriteLine($"Senha: {item.Value.Senha}");
                    Console.WriteLine("=========================================");
                }

            }
            Console.WriteLine("");
            Console.ReadKey();
        }

        //Adicionando Funcionario
        public void AdicionarUserFuncionario()
        {
            string nomeUsuario = "";
            string senhaUsuario = "";
            bool continuarCadastro = false;
            string respostaContinuarCadastro = "";

            do
            {
                Console.WriteLine("========== ADICIONANDO NOVO USUÁRIO ==========");

                Console.Write("Digite o Nome do Usuário: ");
                bool usuarioValido = ValidarNovoNomeUsuario(ref nomeUsuario);

                if (usuarioValido == false)
                {
                    continuarCadastro = true;

                    do
                    {
                        Console.WriteLine("Deseja Continuar o Cadastro? [S/N]");
                    } while (ValidarRespostaSN(ref respostaContinuarCadastro) == false);

                    if (respostaContinuarCadastro.ToUpper() == "N")
                    {
                        continuarCadastro = false;
                    }

                }
                else
                {
                    nomeUsuario = nomeUsuario.ToLower();

                    do
                    {
                        Console.Write("Digite a Senha do Usuário: ");
                    } while (ValidarSenhaUsuario(ref senhaUsuario) == false);

                    UserFuncionario novoFuncionario = new UserFuncionario(nomeUsuario, senhaUsuario);
                    listaFuncionarios.Add(nomeUsuario, novoFuncionario);
                    SalvarInformacoesDoBD_Funcionarios();

                    Console.WriteLine("==============================================");

                    Console.WriteLine("[AVISO!] Funcionário adicionado com sucesso!");
                    continuarCadastro = false;
                    Console.WriteLine("");
                }
            } while (continuarCadastro == true);

            Console.ReadKey();
        }

        //Listando Funcionarios
        public void ListarFuncionarios()
        {
            Console.WriteLine("===== LISTA USUÁRIOS =====");

            foreach (KeyValuePair<string, UserFuncionario> pair in listaFuncionarios)
            {
                Console.WriteLine($"Nome: {pair.Value.NomeUsuario}");
            }

            Console.WriteLine("==========================");
            Console.ReadKey();
        }

        //Consultando Funcionário
        public void ConsultarFuncionario()
        {
            string usuarioConsulta = "";
            bool usuarioEncontrado = false;

            Console.WriteLine("========== CONSULTANDO USUÁRIO ==========");
            Console.Write("Digite o Nome de Usuário: ");
            usuarioConsulta = Console.ReadLine();

            foreach (KeyValuePair<string, UserFuncionario> pair in listaFuncionarios)
            {
                if (pair.Key == usuarioConsulta.ToLower())
                {
                    usuarioEncontrado = true;
                    Console.WriteLine("");
                    Console.WriteLine("===== INFORMAÇÕES DO USUÁRIO =====");
                    Console.WriteLine($"Nome Usuário: {pair.Key}");
                    Console.WriteLine($"Senha Usuário: {pair.Value.Senha}");
                    Console.WriteLine("==================================");
                    Console.WriteLine("");
                }
            }

            if (usuarioEncontrado == false)
            {
                Console.WriteLine("[AVISO!] USUÁRIO NÃO ENCONTRADO!");
            }

            Console.WriteLine("=========================================");
            Console.ReadKey();
        }

        //Excluindo Funcionario
        public void ExcluirFuncionario()
        {
            string usuarioExcluir = "";

            Console.WriteLine("========== EXCLUINDO USUÁRIO ==========");
            Console.Write("Digite o Nome do Usuário que você deseja Excluir: ");

            if (ValidarUsuarioExcluir(ref usuarioExcluir) == true)
            {
                listaFuncionarios.Remove(usuarioExcluir);
                SalvarInformacoesDoBD_Funcionarios();
                Console.WriteLine("[AVISO!] Usuário Excluído com Sucesso!");
                Console.ReadKey();
            }
            Console.ReadKey();
        }

        //VALIDAÇÕES USUÁRIO
        private bool ValidarNovoNomeUsuario(ref string nomeUsuario)
        {
            bool nomeUsuarioJaCadastrado = false;

            try
            {
                nomeUsuario = Console.ReadLine();

                foreach (KeyValuePair<string, UserFuncionario> pair in listaFuncionarios)
                {
                    if (nomeUsuario == pair.Value.NomeUsuario)
                    {
                        nomeUsuarioJaCadastrado = true;
                    }
                }

                if (nomeUsuarioJaCadastrado == true || nomeUsuario.ToLower() == "root")
                {
                    throw new UsuarioJaCadastradoException();
                }

                if (nomeUsuario.Length < 5 || nomeUsuario.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("[ERRO!] O NOME DE USUÁRIO DEVE TER NO MÍNIMO 5 CARACTERES E NO MÁXIMO 50!");
                }

                return true;
            }
            catch (UsuarioJaCadastradoException ex)
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

        private bool ValidarSenhaUsuario(ref string senhaUsuario)
        {
            try
            {
                senhaUsuario = Console.ReadLine();

                if (senhaUsuario.Length < 5 || senhaUsuario.Length > 20)
                {
                    throw new ArgumentOutOfRangeException("[ERRO!] A SENHA DEVE TER NO MÍNIMO 5 CARACTERES E NO MÁXIMO 20!");
                }

                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarRespostaSN(ref string respostaSN)
        {
            try
            {
                respostaSN = Console.ReadLine();

                if (respostaSN.ToUpper() != "S" && respostaSN.ToUpper() != "N")
                {
                    throw new RespostaInvalidaException("[ERRO!] Resposta Inválida!");
                }

                return true;
            }
            catch (RespostaInvalidaException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidarUsuarioExcluir(ref string usuario)
        {
            bool usuarioCadastrado = false;

            try
            {
                usuario = Console.ReadLine();

                foreach (KeyValuePair<string, UserFuncionario> pair in listaFuncionarios)
                {
                    if (pair.Key == usuario.ToLower())
                    {
                        usuarioCadastrado = true;
                    }
                }

                if (usuarioCadastrado == false)
                {
                    throw new UsuarioNaoCadastradoException();
                }

                return true;
            }
            catch (UsuarioNaoCadastradoException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Salvar Informações no Banco
        private void SalvarInformacoesDoBD_Funcionarios()
        {
            if (!File.Exists(caminhoBD_Funcionarios))
            {
                File.Delete(caminhoBD_Funcionarios);
            }

            using (Stream saida = File.Open(caminhoBD_Funcionarios, FileMode.Create))
            {
                using (StreamWriter escritor = new StreamWriter(saida))
                {
                    foreach (KeyValuePair<string, UserFuncionario> funcionario in listaFuncionarios)
                    {
                        escritor.WriteLine(funcionario.Value.ToString());
                    }
                }
            }
        }

        //Carregar Dados dos Funcionários
        private void CarregarDadosFuncionarios()
        {
            if (!File.Exists(caminhoBD_Funcionarios))
            {
                using (Stream criandoBD = File.Open(caminhoBD_Funcionarios, FileMode.Create))
                {

                }
            }
            else
            {
                Dictionary<string, UserFuncionario> listaFuncionariosTemp = new Dictionary<string, UserFuncionario>();

                using (Stream bancoDeDados = File.Open(caminhoBD_Funcionarios, FileMode.Open))
                {
                    using (StreamReader leitor = new(bancoDeDados))
                    {
                        string linha = leitor.ReadLine();

                        while (linha != null)
                        {

                            bool userFuncionarioExiste = false;

                            string[] funcionarioString = linha.Split(";");

                            do
                            {
                                UserFuncionario funcionario = new UserFuncionario(funcionarioString[0], funcionarioString[1]);

                                if (funcionario.NomeUsuario == funcionarioString[0])
                                {
                                    userFuncionarioExiste = true;
                                    listaFuncionariosTemp.Add(funcionario.NomeUsuario, funcionario);
                                }                             
                            } while (userFuncionarioExiste == false);

                            linha = leitor.ReadLine();
                        }
                    }
                }

                listaFuncionarios = listaFuncionariosTemp;
            }
        }
    }
}
