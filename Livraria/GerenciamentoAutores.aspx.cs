using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoAutores : Page
    {
        AutoresDAO ioAutoresDAO = new AutoresDAO();
        public BindingList<Autores> ListaAutores
        {
            get
            {
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null)
                    this.CarregaDados();
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];

            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CarregaDados()
        {
            this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
        }
        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                decimal IdcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;
                string isNomeAutor = this.txbCadastroNomeAutor.Text;
                string isSobrenomeAutor = this.txbCadastroEmailAutor.Text;
                string isEmailAutor = this.txbCadastroEmailAutor.Text;

                Autores loAutor = new Autores(IdcIdAutor, isNomeAutor, isSobrenomeAutor, isEmailAutor);
                this.ioAutoresDAO.InsereAutor(loAutor);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!')</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor')</script>");
            }
            this.txbCadastroNomeAutor.Text = String.Empty;
            this.txbCadastroSobrenomeAutor.Text = String.Empty;
            this.txbCadastroEmailAutor.Text = String.Empty;
        }
    }
}