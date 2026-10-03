using System;

namespace ProjetoBiblioteca
{
    class Program
    {
        static Livros biblioteca = new Livros();

        static void Main(string[] args)
        {
            int opcao = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("          SISTEMA DE BIBLIOTECA         ");
                Console.WriteLine("========================================");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar livro");
                Console.WriteLine("2. Pesquisar livro (sintético)");
                Console.WriteLine("3. Pesquisar livro (analítico)");
                Console.WriteLine("4. Adicionar exemplar");
                Console.WriteLine("5. Registrar empréstimo");
                Console.WriteLine("6. Registrar devolução");
                Console.WriteLine("========================================");
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine();
                    switch (opcao)
                    {
                        case 0:
                            Console.WriteLine("Encerrando a aplicação...");
                            break;
                        case 1:
                            AdicionarLivro();
                            break;
                        case 2:
                            PesquisarLivroSintetico();
                            break;
                        case 3:
                            PesquisarLivroAnalitico();
                            break;
                        case 4:
                            AdicionarExemplar();
                            break;
                        case 5:
                            RegistrarEmprestimo();
                            break;
                        case 6:
                            RegistrarDevolucao();
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, digite um número válido.");
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        static void AdicionarLivro()
        {
            Console.Write("Informe o ISBN: ");
            int isbn = int.Parse(Console.ReadLine());

            if (biblioteca.Pesquisar(new Livro(isbn)) != null)
            {
                Console.WriteLine("Erro: Já existe um livro cadastrado com este ISBN.");
                return;
            }

            Console.Write("Informe o Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Informe o Autor: ");
            string autor = Console.ReadLine();

            Console.Write("Informe a Editora: ");
            string editora = Console.ReadLine();

            Livro novoLivro = new Livro(isbn, titulo, autor, editora);
            biblioteca.Adicionar(novoLivro);

            Console.WriteLine("\nLivro cadastrado com sucesso!");
        }

        static void PesquisarLivroSintetico()
        {
            Console.Write("Informe o ISBN do livro: ");
            int isbn = int.Parse(Console.ReadLine());

            Livro livro = biblioteca.Pesquisar(new Livro(isbn));

            if (livro != null)
            {
                Console.WriteLine($"\nTítulo: {livro.Titulo}");
                Console.WriteLine($"Autor: {livro.Autor}");
                Console.WriteLine($"Editora: {livro.Editora}");
                Console.WriteLine($"Total de Exemplares: {livro.QtdeExemplares()}");
                Console.WriteLine($"Exemplares Disponíveis: {livro.QtdeDisponiveis()}");
                Console.WriteLine($"Total de Empréstimos: {livro.QtdeEmprestimos()}");
                Console.WriteLine($"Percentual de Disponibilidade: {livro.PercDisponibilidade():F2}%");
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void PesquisarLivroAnalitico()
        {
            Console.Write("Informe o ISBN do livro: ");
            int isbn = int.Parse(Console.ReadLine());

            Livro livro = biblioteca.Pesquisar(new Livro(isbn));

            if (livro != null)
            {
                // Dados Sintéticos
                Console.WriteLine($"\nTítulo: {livro.Titulo}");
                Console.WriteLine($"Autor: {livro.Autor}");
                Console.WriteLine($"Editora: {livro.Editora}");
                Console.WriteLine($"Total de Exemplares: {livro.QtdeExemplares()}");
                Console.WriteLine($"Exemplares Disponíveis: {livro.QtdeDisponiveis()}");
                Console.WriteLine($"Total de Empréstimos: {livro.QtdeEmprestimos()}");
                Console.WriteLine($"Percentual de Disponibilidade: {livro.PercDisponibilidade():F2}%");

                // Detalhes Analíticos
                Console.WriteLine("\n--- DETALHES DOS EXEMPLARES ---");
                if (livro.Exemplares.Count == 0)
                {
                    Console.WriteLine("Nenhum exemplar cadastrado para este livro.");
                }
                else
                {
                    foreach (var exemplar in livro.Exemplares)
                    {
                        Console.WriteLine($"\n[Exemplar Tombo: {exemplar.Tombo}] - Status: {(exemplar.Disponivel() ? "Disponível" : "Emprestado")}");
                        Console.WriteLine($"Total de empréstimos deste exemplar: {exemplar.QtdeEmprestimos()}");

                        if (exemplar.Emprestimos.Count > 0)
                        {
                            Console.WriteLine("  Histórico de Empréstimos:");
                            foreach (var emp in exemplar.Emprestimos)
                            {
                                string dtDev = emp.DtDevolucao == DateTime.MinValue ? "Em aberto" : emp.DtDevolucao.ToString("dd/MM/yyyy HH:mm");
                                Console.WriteLine($"   - Empréstimo: {emp.DtEmprestimo:dd/MM/yyyy HH:mm} | Devolução: {dtDev}");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void AdicionarExemplar()
        {
            Console.Write("Informe o ISBN do livro: ");
            int isbn = int.Parse(Console.ReadLine());

            Livro livro = biblioteca.Pesquisar(new Livro(isbn));

            if (livro != null)
            {
                Console.Write("Informe o número de Tombo do exemplar: ");
                int tombo = int.Parse(Console.ReadLine());

                Exemplar exemplar = new Exemplar(tombo);
                livro.AdicionarExemplar(exemplar);

                Console.WriteLine("\nExemplar adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void RegistrarEmprestimo()
        {
            Console.Write("Informe o ISBN do livro: ");
            int isbn = int.Parse(Console.ReadLine());

            Livro livro = biblioteca.Pesquisar(new Livro(isbn));

            if (livro != null)
            {
                // Busca um exemplar que esteja disponível
                Exemplar exemplarDisponivel = livro.Exemplares.Find(e => e.Disponivel());

                if (exemplarDisponivel != null)
                {
                    if (exemplarDisponivel.Emprestar())
                    {
                        Console.WriteLine($"\nEmpréstimo registrado com sucesso para o exemplar Tombo: {exemplarDisponivel.Tombo}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNão há exemplares disponíveis para este livro no momento.");
                }
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void RegistrarDevolucao()
        {
            Console.Write("Informe o ISBN do livro: ");
            int isbn = int.Parse(Console.ReadLine());

            Livro livro = biblioteca.Pesquisar(new Livro(isbn));

            if (livro != null)
            {
                // Busca um exemplar que esteja emprestado (não disponível)
                Exemplar exemplarEmprestado = livro.Exemplares.Find(e => !e.Disponivel());

                if (exemplarEmprestado != null)
                {
                    if (exemplarEmprestado.Devolver())
                    {
                        Console.WriteLine($"\nDevolução registrada com sucesso para o exemplar Tombo: {exemplarEmprestado.Tombo}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNão existem empréstimos em aberto para os exemplares deste livro.");
                }
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }
    }
}