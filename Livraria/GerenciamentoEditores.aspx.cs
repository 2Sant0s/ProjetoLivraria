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
    public partial class GerenciamentoEditores : Page
    {
        EditoresDAO ioEditoresDAO = new EditoresDAO();

        public Editores EditoresSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
        }

        public BindingList<Editores> ListaEditores
        {
            get
            {
                if ((BindingList<Editores>)ViewState["ViewStateListaAutores"] == null)
                    this.CarregaDados();
                return (BindingList<Editores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
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
                this.ListaEditores = ioEditoresDAO.BuscaEditores();
                this.gvGerenciamentoEditores.DataSource = this.ListaEditores.OrderBy(loAutor => loAutor.edi_nm_editor);
                this.gvGerenciamentoEditores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Editores.');</script>");

            }
        }
        protected void BtnNovoEditor_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            try
            {
                //corrigir nomenclatura
                decimal ldcEditor = this.ListaEditores.OrderByDescending(a => a.edi_id_editor).First().edi_id_editor + 1;
                string lsNomeEditor = this.txbCadastroNomeEditor.Text;
                string lsEmailEditor = this.txbCadastroEmailEditor.Text;
                string lsUrlEditor = this.txbCadastroURLEditor.Text;

                Editores loEditor = new Editores(ldcEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);
                this.ioEditoresDAO.InsereEditor(loEditor);
                HttpContext.Current.Response.Write("<script> alert('Editor cadastrado com sucesso!'); </script>");
                //Response.Redirect(Request.RawUrl, false); ???
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastro do editor'); </script>");
                throw new Exception(ex.Message);
            }
            this.txbCadastroNomeEditor.Text = string.Empty;
            this.txbCadastroEmailEditor.Text = string.Empty;
            this.txbCadastroURLEditor.Text = string.Empty;
        }
        protected void gvGerenciamentoEditores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal editorID = Convert.ToDecimal(e.Keys["edi_id_editor"]);
                string nomeEditor = e.NewValues["edi_nm_editor"].ToString();
                string emailEditor = e.NewValues["edi_ds_email"].ToString();
                string urlEditor = e.NewValues["edi_ds_url"].ToString();

                if (string.IsNullOrEmpty(nomeEditor))
                {
                    HttpContext.Current.Response.Write("<script> alert('Informe o nome do editor.'); </script>");
                    return;
                }
                if (string.IsNullOrEmpty(emailEditor))
                {

                    HttpContext.Current.Response.Write("<script> alert('Informe o email do editor.'); </script>");
                    return;
                }
                else if (string.IsNullOrEmpty(urlEditor))
                {
                    HttpContext.Current.Response.Write("<script> alert('Informe o url do editor.'); </script>");
                    return;
                }

                Editores editor = new Editores(editorID, nomeEditor, emailEditor, urlEditor);

                int qtdLinhasAfetadas = ioEditoresDAO.AtualizaEditor(editor);

                e.Cancel = true;
                this.gvGerenciamentoEditores.CancelEdit();
                CarregaDados();

            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na atualização do Editor.');</script>");
                throw new Exception(ex.Message);
            }
        }
        protected void gvGerenciamentoEditores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal editorId = Convert.ToDecimal(e.Keys["edi_id_editor"]);
                Editores loEditor = this.ioEditoresDAO.BuscaEditores(editorId).FirstOrDefault();

                if (loEditor != null)
                {
                    LivroDAO loLivroDAO = new LivroDAO();
                    if (loLivroDAO.FindLivrosByEditor(loEditor).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover o autor selecionado pois existem livros associados a ele.');</script>");
                        e.Cancel = true;
                    }
                    else
                    {
                        this.ioEditoresDAO.RemoveEditor(loEditor);
                        this.CarregaDados();
                        e.Cancel = true;
                    }

                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do editor selecionado.')</script>");
                throw new Exception(ex.Message);
            }
        }
        protected void gvGerenciamentoEditores_CustomButtonCallBack(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal EditorId = Convert.ToDecimal(gvGerenciamentoEditores.GetRowValues(e.VisibleIndex, "edi_id_editor"));
            var editores = ioEditoresDAO.BuscaEditores(EditorId).FirstOrDefault();

            if (e.ButtonID == "btnEditorInfo")
            {

            }
            else if (e.ButtonID == "btnLivros")
            {
                Session["SessionEditorSelecionado"] = editores;

                gvGerenciamentoEditores.JSProperties["cpRedirectToLivros"] = true;
            }
        }

        private void RedirectLivros(String idAutorString, string controlID)
        {
            switch (controlID)
            {
                case "btnLivros":
                    decimal id = Convert.ToDecimal(idAutorString);
                    EditoresSessao = this.ioEditoresDAO.BuscaEditores(id).First();

                    Response.Redirect("/Livraria/GerenciamentoEditores.aspx");
                    break;
                default: break;
            }

        }
    }
}
