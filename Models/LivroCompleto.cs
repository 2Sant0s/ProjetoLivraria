using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    public class LivroCompleto
    {
        public decimal liv_id_livro { get; set; }
        public string liv_nm_titulo { get; set; }
        public decimal liv_vl_preco { get; set; }
        public int liv_nu_edicao { get; set; }
        public string nome_editor { get; set; }
        public string nome_categoria { get; set; }
        public decimal liv_pc_royalty { get; set; }
        public string nome_autor { get; set; }
        public string liv_ds_resumo { get; set; }

        public LivroCompleto(decimal idLivro, string titulo, decimal preco, int edicao,
                         string editor, string categoria, decimal royalty,
                         string autor, string resumo)
        {
            liv_id_livro = idLivro;
            liv_nm_titulo = titulo;
            liv_vl_preco = preco;
            liv_nu_edicao = edicao;
            nome_editor = editor;
            nome_categoria = categoria;
            liv_pc_royalty = royalty;
            nome_autor = autor;
            liv_ds_resumo = resumo;
        }
    }
}