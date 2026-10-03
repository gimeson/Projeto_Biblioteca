using System.Collections.Generic;

namespace ProjetoBiblioteca
{
    public class Livros
    {
        public List<Livro> Acervo { get; set; }

        public Livros()
        {
            Acervo = new List<Livro>();
        }

        public void Adicionar(Livro livro)
        {
            Acervo.Add(livro);
        }

        public Livro Pesquisar(Livro livro)
        {
            return Acervo.Find(l => l.Equals(livro));
        }
    }
}