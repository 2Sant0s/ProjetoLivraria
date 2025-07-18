using System;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Livro
    {
        public decimal liv_id_livro { get; set; }
        public decimal liv_id_tipo_livro { get; set; }
        public decimal liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public decimal liv_vl_preco { get; set; }
        public decimal liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }
        public int liv_nu_edicao { get; set; }

        public Livro(decimal adcIdLivro, decimal adcIdTipoLivro, decimal adcIdEditor,
          string asNomeTitulo, decimal asPrecoLivro, decimal asRoyaltyLivro, string asResumoLivro, int asEdicaoLivro)
        {
            this.liv_id_livro = adcIdLivro;
            this.liv_id_tipo_livro = adcIdTipoLivro;
            this.liv_id_editor = adcIdEditor;
            this.liv_nm_titulo = asNomeTitulo;
            this.liv_vl_preco = asPrecoLivro;
            this.liv_pc_royalty = asRoyaltyLivro;
            this.liv_ds_resumo = asResumoLivro;
            this.liv_nu_edicao = asEdicaoLivro;
        }
    }
}