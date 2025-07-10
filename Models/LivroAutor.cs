using System;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class LivroAutor
    {
        public decimal lia_id_autor { get; set; }
        public decimal lia_id_livro { get; set; }
        public decimal lia_pc_royalty { get; set; }
        //public Autores(decimal adcIdAutor, string asNomeAutor, string asSobrenomeAutor, string asEmailAutor)

        public LivroAutor(decimal adcIdAutor, decimal adcIdLivro, decimal asRoyaltyAutor)
        {
            this.lia_id_autor = adcIdAutor;
            this.lia_id_livro = adcIdLivro;
            this.lia_pc_royalty = asRoyaltyAutor;
        }
    }
}