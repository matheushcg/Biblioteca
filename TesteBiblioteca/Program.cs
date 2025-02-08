using TesteBiblioteca.Entities;

namespace TesteBiblioteca
{
    class Program
    {
        #region VARIÁVEIS
        public static List<Usuario> usuarios = new List<Usuario>();
        public static List<Livro> livros = new List<Livro>();
        public static List<Emprestimo> emprestimos = new List<Emprestimo>();
        #endregion

        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine("Bem vindo a nossa Biblioteca");
                Console.WriteLine("\nEscolha uma opção");
                Console.WriteLine("1 - Cadastrar um livro");
                Console.WriteLine("2 - Cadastrar um usuário");
                Console.WriteLine("3 - Cadastrar um empréstimo de um livro");
                Console.WriteLine("4 - Devolver um Livro");
                Console.WriteLine("5 - Listar Livros Disponíveis");
                Console.WriteLine("6 - Histórico de Emprestimos");
                Console.WriteLine("7 - Listar Usuários cadastrados");
                Console.WriteLine("8 - Listar Livros cadastrados");
                Console.WriteLine("9 - Sair");

                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        CadastrarLivro();
                        break;

                    case "2":
                        CadastrarUsuario();
                        break;

                    case "3":
                        CadastrarEmprestimoLivro();
                        break;

                    case "4":
                        DevolverLivro();
                        break;

                    case "5":
                        ListarLivrosDisponiveis();
                        break;

                    case "6":
                        ListarHistoricoEmprestimosRealizados();
                        break;

                    case "7":
                        ListarUsuariosCadastrados();
                        break;

                    case "8":
                        ListarLivrosCadastrados();
                        break;

                    case "9":
                        return;

                    default:
                        Console.WriteLine("Opção inválida, deseja continuar? Digite 1 para sim ou 2 para não");
                        string resposta = Console.ReadLine();
                        if (resposta == "2") return;
                        break;
                }
            }

            #region MÉTODOS DE LEITURA DE VALORES
            string LerEntrada(string mensagem)
            {
                while (true)
                {
                    Console.WriteLine(mensagem);
                    string resposta = Console.ReadLine();
                    if (!string.IsNullOrEmpty(resposta))
                        return resposta;

                    Console.WriteLine("Dado inválido, por favor digite novamente");
                }
            }

            Status RetornaStatusLivro(string mensagem)
            {
                while (true)
                {
                    Console.WriteLine(mensagem);
                    string resposta = Console.ReadLine();

                    if (resposta == "1" || resposta == "2")
                    {
                        if (resposta == "1")
                            return Status.Emprestado;
                        else
                            return Status.Disponivel;
                    }
                    else
                    {
                        Console.WriteLine("Desculpe, não entendi");
                    }
                }
            }
            #endregion

            #region OPÇÕES DO CONSOLE
            void CadastrarUsuario()
            {
                Console.Clear();
                Usuario usuario = new Usuario();

                usuario.Id = usuarios.Count > 0 ? usuarios.Last().Id + 1 : 1;
                usuario.Nome = LerEntrada("Digite o nome do usuário");
                usuario.Sexo = LerEntrada("Digite o Sexo");
                usuario.Endereco = new Endereco();
                usuario.Endereco.Rua = LerEntrada("Digite o nome da Rua");
                usuario.Endereco.Numero = Convert.ToInt32(LerEntrada("Digite o número da residência"));
                usuario.Endereco.Bairro = LerEntrada("Digite o nome do bairro");
                usuario.Endereco.Cidade = LerEntrada("Digite o nome da cidade");
                usuario.Endereco.Cep = LerEntrada("Digite o CEP");

                usuarios.Add(usuario);

                Console.WriteLine($"Usuário {usuario.Nome} cadastrado com sucesso");
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }

            void CadastrarLivro()
            {
                Console.Clear();
                Livro livro = new Livro();

                livro.Id = livros.Count > 0 ? livros.Last().Id + 1 : 1;
                livro.Titulo = LerEntrada("Digite o titulo do Livro");
                livro.Autor = LerEntrada("Digite o nome do autor do livro");
                livro.ISBN = LerEntrada("Digite o ISBN do Livro");
                livro.Status = Status.Disponivel;

                livros.Add(livro);

                Console.WriteLine($"Livro {livro.Titulo} cadastrado com sucesso");
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }

            void CadastrarEmprestimoLivro()
            {
                Console.Clear();
                Livro livro = new Livro();

                string codigoLivro = LerEntrada("Digite o código do livro que se deseja realizar o emprestimo");

                livro = livros.Where(x => x.Id.ToString() == codigoLivro).FirstOrDefault();

                if (livro != null && livro.Status != Status.Emprestado)
                {
                    string codigoUsuario = LerEntrada("Digite o código do usuário que se deseja realizar o emprestimo");

                    var usuario = usuarios.Where(x => x.Id.ToString() == codigoUsuario).FirstOrDefault();

                    if (usuario != null)
                    {
                        Emprestimo emprestimo = new Emprestimo();
                        livro.Status = Status.Emprestado;

                        emprestimo.Id = emprestimos.Count > 0 ? emprestimos.Last().Id + 1 : 1;
                        emprestimo.Livro = livro;
                        emprestimo.Usuario = usuario;
                        emprestimo.DataEmprestimo = DateTime.Now;

                        emprestimos.Add(emprestimo);

                        Console.WriteLine($"Emprestimo do livro {emprestimo.Livro.Titulo} cadastrado para o usuário {emprestimo.Usuario.Nome} com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Usuário não existente, por favor tente novamente");
                    }
                }
                else
                {
                    Console.WriteLine("Livro já emprestado ou não existente");
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }

            void DevolverLivro()
            {
                Console.Clear();
                Livro livro = new Livro();

                string codigoDevolucao = LerEntrada("Digite o código do livro que se deseja realizar devolução");

                livro = livros.Where(x => x.Id.ToString() == codigoDevolucao).FirstOrDefault();

                if (livro != null && livro.Status == Status.Emprestado)
                {
                    livro.Status = Status.Disponivel;
                    Console.WriteLine("Livro devolvido com sucesso");
                }
                else
                {
                    Console.WriteLine("Livro não encontrado, ou já está disponível");
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }

            void ListarLivrosDisponiveis()
            {
                Console.Clear();
                var livrosDisponiveis = livros.Where(x => x.Status == Status.Disponivel).ToList();

                if (livrosDisponiveis.Count > 0)
                {
                    Console.WriteLine("Livros Disponíveis:\n");
                    foreach (var livro in livrosDisponiveis)
                    {
                        Console.WriteLine($"Código: {livro.Id}, Titulo:{livro.Titulo}, Autor: {livro.Autor}, ISBN: {livro.ISBN}, Status: {livro.Status}");
                    }
                }
                else
                {
                    Console.WriteLine("Não existem livros disponíveis no momento");
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }

            void ListarHistoricoEmprestimosRealizados()
            {
                if (emprestimos.Count > 0)
                {
                    Console.WriteLine("Histórico de Emprestimos Realizados: \n");

                    foreach(var emprestimo in emprestimos)
                    {
                        Console.WriteLine($"Nome do usuário: {emprestimo.Usuario.Nome}, Livro Emprestado: {emprestimo.Livro.Titulo}, No dia: {emprestimo.DataEmprestimo.ToString("dd/MM/yyyy")}, as {emprestimo.DataEmprestimo.ToString("hh:mm tt")}");
                    }
                }
                else
                {
                    Console.WriteLine("Você não possui emprestimos cadastrados");
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }

            void ListarUsuariosCadastrados()
            {
                Console.Clear();
                if (usuarios.Count > 0)
                {
                    Console.WriteLine("Usuários Cadastrados:\n");
                    foreach (var usuario in usuarios)
                    {
                        Console.WriteLine($"Código: {usuario.Id}, Nome:{usuario.Nome}, Endereço: {usuario.Endereco.Rua}, número: {usuario.Endereco.Numero}, bairro: {usuario.Endereco.Bairro}, cidade: {usuario.Endereco.Cidade}");
                    }
                }
                else
                {
                    Console.WriteLine("Você não tem usuários cadastrados");
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }

            void ListarLivrosCadastrados()
            {
                Console.Clear();
                if (livros.Count > 0)
                {
                    Console.WriteLine("Livros Cadastrados:\n");
                    foreach (var livro in livros)
                    {
                        Console.WriteLine($"Código: {livro.Id}, Titulo:{livro.Titulo}, Autor: {livro.Autor}, ISBN: {livro.ISBN}, Status: {livro.Status}");
                    }
                }
                else
                {
                    Console.WriteLine("Você não tem livros cadastrados");
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                Console.ReadKey();
            }
            #endregion
        }
    }
}