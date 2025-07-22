using System;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Livro
    {
        public decimal liv_id_livro { get; set; }
        public decimal aut_id_autor { get; set; }
        public decimal liv_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }
        public decimal liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public decimal liv_vl_preco { get; set; }
        public decimal liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }

        public string edi_nm_nome { get; set; }
        public int liv_nu_edicao { get; set; }
        public string aut_nm_nome { get; set; } // Novo campo adicionado

        // Construtor original (sem nome do autor)
        public Livro(decimal idLivro, decimal idTipoLivro, decimal idEditor, string titulo, decimal preco, decimal royalty, string resumo, int edicao)
        {
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = titulo;
            this.liv_vl_preco = preco;
            this.liv_pc_royalty = royalty;
            this.liv_ds_resumo = resumo;
            this.liv_nu_edicao = edicao;
        }

        // Construtor com nome do autor
        public Livro(decimal idLivro, decimal idTipoLivro, decimal idEditor, string titulo, decimal preco, decimal royalty, string resumo, int edicao, string nomeAutor)
        {
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = titulo;
            this.liv_vl_preco = preco;
            this.liv_pc_royalty = royalty;
            this.liv_ds_resumo = resumo;
            this.liv_nu_edicao = edicao;
            this.aut_nm_nome = nomeAutor;
        }
        public Livro(decimal idLivro, decimal idTipoLivro, decimal idEditor, string titulo, decimal preco, decimal royalty, string resumo, int edicao, string nomeAutor, string nomeEditor, string til_ds_descricao)
        {
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = titulo;
            this.liv_vl_preco = preco;
            this.liv_pc_royalty = royalty;
            this.liv_ds_resumo = resumo;
            this.liv_nu_edicao = edicao;
            this.aut_nm_nome = nomeAutor;
            this.edi_nm_nome = nomeEditor; // novo campo
            this.til_ds_descricao = til_ds_descricao;
        }
    }
}
