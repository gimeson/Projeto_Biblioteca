using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBiblioteca
{
    public class Exemplar
    {
        public int Tombo { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }

        public Exemplar(int tombo)
        {
            Tombo = tombo;
            Emprestimos = new List<Emprestimo>();
        }

        public Exemplar() : this(0) { }

        public bool Disponivel()
        {
            // O exemplar está disponível se não tiver empréstimos ou se o último empréstimo tiver data de devolução registrada
            if (Emprestimos.Count == 0) return true;
            return Emprestimos.Last().DtDevolucao != DateTime.MinValue;
        }

        public bool Emprestar()
        {
            if (Disponivel())
            {
                Emprestimos.Add(new Emprestimo(DateTime.Now));
                return true;
            }
            return false;
        }

        public bool Devolver()
        {
            if (!Disponivel())
            {
                Emprestimos.Last().DtDevolucao = DateTime.Now;
                return true;
            }
            return false;
        }

        public int QtdeEmprestimos()
        {
            return Emprestimos.Count;
        }
    }
}