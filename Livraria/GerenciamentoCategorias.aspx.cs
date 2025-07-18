using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoCategorias : Page
    {
        TipoLivroDAO ioTipoLivroDAO = new TipoLivroDAO();

        public TipoLivro CategoriasSessao
        {
            get { return (TipoLivro)Session["SessionCategoriaSelecionada"]; }
            set { Session["SessionCategoriaSelecionada"] = value; }
        }

        public BindingList<TipoLivro> ListaCategorias
        {
            get
            {
                if ((BindingList<TipoLivro>)ViewState["ViewStateListaCategoria"] == null)
                    this.CarregaDados();
                return (BindingList<TipoLivro>)ViewState["ViewStateListaCategoria"];
            }
            set
            {
                ViewState["ViewStateListaCategoria"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaCategorias = ioTipoLivroDAO.BuscaTipoLivro();
                this.gvGerenciamentoCategoria.DataSource = ListaCategorias.OrderBy(loTipoLivro => loTipoLivro.til_id_tipo_livro);
                this.gvGerenciamentoCategoria.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar categorias de livro.');</script>");

            }
        }
        protected void BtnNovaCategoria_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            try
            {
                //corrigir nomenclatura 
                //  this.til_id_tipo_livro = adcIdTipoLivro;
                // this.til_ds_descricao = asDescricaoTipoLivro;
                decimal ldcIdTipoLivro = this.ListaCategorias.OrderByDescending(a => a.til_id_tipo_livro).First().til_id_tipo_livro + 1;
                string lsDescricaoTipoLivro = this.txbCadastroCategoria.Text;

                TipoLivro loTipoLivro = new TipoLivro(ldcIdTipoLivro, lsDescricaoTipoLivro);
                this.ioTipoLivroDAO.InsereTipoLivro(loTipoLivro);

                HttpContext.Current.Response.Write("<script> alert('Categoria cadastrada com sucesso!'); </script>");
                //Response.Redirect(Request.RawUrl, false); // ???
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastro da categoria'); </script>");
                throw new Exception(ex.Message);
            }
            this.txbCadastroCategoria.Text = string.Empty;
        }
        protected void gvGerenciamentoCategoria_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal idTipoLivro = Convert.ToDecimal(e.Keys["til_id_tipo_livro"]);
                string CategoriaTipoLivro = e.NewValues["til_ds_descricao"].ToString();

                if (string.IsNullOrEmpty(CategoriaTipoLivro))
                {
                    HttpContext.Current.Response.Write("<script> alert('Informe a categoria do livro.'); </script>");
                    return;
                }

                TipoLivro tipoLivro = new TipoLivro(idTipoLivro, CategoriaTipoLivro);

                int qtdLinhasAfetadas = ioTipoLivroDAO.atualizaTipoLivro(tipoLivro);

                e.Cancel = true;
                this.gvGerenciamentoCategoria.CancelEdit();
                CarregaDados();

            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na atualização da categoria');</script>");
                throw new Exception(ex.Message);
            }
        }
        protected void gvGerenciamentoCategoria_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal tipoLivroId = Convert.ToDecimal(e.Keys["til_id_tipo_livro"]);
                TipoLivro loTipoLivro = this.ioTipoLivroDAO.BuscaTipoLivro(tipoLivroId).FirstOrDefault();

                if (loTipoLivro != null)
                {
                    LivroDAO loLivroDAO = new LivroDAO();
                    if (loLivroDAO.FindLivrosByCategoria(loTipoLivro).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover a categoria selecionado pois existem livros associados a ele.');</script>");
                        e.Cancel = true;
                    }
                    else
                    {
                        this.ioTipoLivroDAO.RemoveTipoLivro(loTipoLivro);
                        e.Cancel = true;
                        this.gvGerenciamentoCategoria.CancelEdit();
                        CarregaDados();
                    }

                }
            }
            catch (Exception ex)
            {
                //investigar o porquê não está disparando o alert
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção da categoria selecionada.')</script>");
                throw new Exception(ex.Message);
            }
        }
        protected void gvGerenciamentoCategoria_CustomButtonCallBack(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal TipoLivroId = Convert.ToDecimal(gvGerenciamentoCategoria.GetRowValues(e.VisibleIndex, "til_id_tipo_livro"));
            var tipoLivro = ioTipoLivroDAO.BuscaTipoLivro(TipoLivroId).FirstOrDefault();

            if (e.ButtonID == "btnEditorInfo")
            {

            }
            else if (e.ButtonID == "btnLivros")
            {
                Session["SessionCategoriaSelecionada"] = CategoriasSessao;

                gvGerenciamentoCategoria.JSProperties["cpRedirectToLivros"] = true;
            }
        }

        protected void RedirectLivros(String idTipoLivroString, string controlID)
        {
            switch (controlID)
            {
                case "btnLivros":
                    decimal id = Convert.ToDecimal(idTipoLivroString);
                    CategoriasSessao = this.ioTipoLivroDAO.BuscaTipoLivro(id).First();

                    Response.Redirect("/Livraria/GerenciamentoLivros.aspx");
                    break;
                default: break;
            }

        }
    }
}

