using System;

namespace ProjetoBiblioteca
{
    public class Emprestimo
    {
        public DateTime DtEmprestimo { get; set; }
        public DateTime DtDevolucao { get; set; }

        public Emprestimo(DateTime dtEmprestimo)
        {
            DtEmprestimo = dtEmprestimo;
            DtDevolucao = DateTime.MinValue; // Indica que ainda não foi devolvido
        }
    }
}