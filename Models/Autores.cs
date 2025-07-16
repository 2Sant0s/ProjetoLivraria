using System;

namespace ProjetoLivraria.Models
{
    [Serializable] // questão do Session/ViewState (transf. de dados) *pesquisar*
    public class Autores
    {
        public decimal aut_id_autor { get; set; }
        public string aut_nm_nome { get; set; }
        public string aut_nm_sobrenome { get; set; }
        public string aut_ds_email { get; set; }
        public string aut_ds_resumo { get; set; }

        public Autores(decimal adcIdAutor, string asNomeAutor, string asSobrenomeAutor, string asEmailAutor)
        {
            this.aut_id_autor = adcIdAutor;
            this.aut_nm_nome = asNomeAutor;
            this.aut_nm_sobrenome = asSobrenomeAutor;
            this.aut_ds_email = asEmailAutor;
        }

        public Autores()
        {
        }
    }
}