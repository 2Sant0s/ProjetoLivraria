using System;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class TipoLivro
    {
        public decimal til_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }

        public TipoLivro(decimal adcIdTipoLivro, string asDescricaoTipoLivro)
        {
            this.til_id_tipo_livro = adcIdTipoLivro;
            this.til_ds_descricao = asDescricaoTipoLivro;
        }

    }
}