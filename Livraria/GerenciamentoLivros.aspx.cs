using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.Web.Internal.XmlProcessor;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoLivros : Page
    {
        LivroDAO ioLivrosDAO = new LivroDAO();
        EditoresDAO ioEditoraDAO = new EditoresDAO();
        TipoLivroDAO ioTipoLivroDAO = new TipoLivroDAO(); 
        AutoresDAO ioAutoresDAO = new AutoresDAO();
        public BindingList<Livro> ListaLivros
        {
            get
            {
                if ((BindingList<Livro>)ViewState["ViewStateListaLivros"] == null)
                    this.CarregaDados();
                return (BindingList<Livro>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }
        public BindingList<Editores> ListaEditoras
        {
            get
            {
                if (ViewState["ViewStateListaEditoras"] == null)
                {
                    return new BindingList<Editores>();
                }
                return (BindingList<Editores>)ViewState["ViewStateListaEditoras"];
            }
            set
            {
                ViewState["ViewStateListaEditoras"] = value;
            }
        }
        public BindingList<Autores> ListaAutores
        {
            get
            {
                if (ViewState["ViewStateListaAutores"] == null)
                {
                    return new BindingList<Autores>();
                }
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }
        public BindingList<TipoLivro> ListaTipoLivros 
        {
            get
            {
                if (ViewState["ViewStateListaTipoLivro"] == null)
                {
                    return new BindingList<TipoLivro>();
                }
                return (BindingList<TipoLivro>)ViewState["ViewStateListaTipoLivro"];
            }
            set
            {
                ViewState["ViewStateListaTipoLivro"] = value;
            }
        }
        public TipoLivro TipoLivroSession
        {
            get { return (TipoLivro)Session["SessionTipoLivroSelecionado"]; }
            set { Session["SessionTipoLivroSelecionado"] = value; }
        }


        public Autores AutoresSession
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                CarregaDados();
        }
        private void CarregaDados()
        {
            try
            {
                this.ListaLivros = this.ioLivrosDAO.BuscaLivro();
                this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(loLivro => loLivro.liv_nm_titulo);
                this.gvGerenciamentoLivros.DataBind();

                this.ListaTipoLivros = this.ioTipoLivroDAO.BuscaTipoLivro();
                this.cbCadastroTipoLivro.DataSource = this.ListaTipoLivros.OrderBy(loTipoLivro => loTipoLivro.til_ds_descricao);
                this.cbCadastroTipoLivro.TextField = "til_ds_descricao";
                this.cbCadastroTipoLivro.DataBind();
                this.cbCadastroTipoLivro.Items.Insert(0, new ListEditItem("Selecione...", null));

                this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
                this.cbCadastroAutorLivro.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nm_nome);
                this.cbCadastroAutorLivro.TextField = "aut_nm_nome";
                this.cbCadastroAutorLivro.DataBind();
                this.cbCadastroAutorLivro.Items.Insert(0, new ListEditItem("Selecione...", null));

                this.ListaEditoras = this.ioEditoraDAO.BuscaEditores();
                this.cbCadastroEditoraLivro.DataSource = this.ListaEditoras.OrderBy(loEditor => loEditor.edi_nm_editor);
                this.cbCadastroEditoraLivro.TextField = "edi_nm_editor";
                this.cbCadastroEditoraLivro.DataBind();
                this.cbCadastroEditoraLivro.Items.Insert(0, new ListEditItem("Selecione...", null));


            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Livros');</script>");
            }
        }

        protected void btnNovoLivro_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            try
            {
                decimal IdcIdLivro = this.ListaLivros.OrderByDescending(a => a.liv_id_livro).First().liv_id_livro + 1;
                decimal isTipoLivro = Convert.ToDecimal(this.cbCadastroTipoLivro.Text);
                decimal isEditoraLivro = Convert.ToDecimal(this.cbCadastroEditoraLivro.Text);
                string isTNomeTituloLivro = this.txbCadastroTituloLivro.Text;
                string isAutorLivro = this.cbCadastroAutorLivro.Text;
                decimal isPrecoLivro = Convert.ToDecimal(this.seCasdastroPrecoLivro.Text);
                decimal isRoyaltyLivro = Convert.ToDecimal(this.seCadastroRoyaltyLivro.Text);
                string isResumoLivro = this.txbCadastroResumoLivro.Text;
                int isEdicaoLivro = Convert.ToInt32(this.seCadastroEdicaoLivro.Text);


                Livro loLivro = new Livro(IdcIdLivro, isTipoLivro, isEditoraLivro, isTNomeTituloLivro, isPrecoLivro, isRoyaltyLivro, isResumoLivro, isEdicaoLivro);
                this.ioLivrosDAO.InsereLivro(loLivro);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!')</script>");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Livro')</script>");
                throw new Exception(ex.Message);
            }

            this.cbCadastroTipoLivro.SelectedIndex = -1;
            this.cbCadastroEditoraLivro.SelectedIndex = -1;
            this.txbCadastroTituloLivro.Text = String.Empty;
            this.cbCadastroAutorLivro.SelectedIndex = -1;
            this.seCasdastroPrecoLivro.Value = null;
            this.seCadastroRoyaltyLivro.Value = null;
            this.txbCadastroResumoLivro.Text = String.Empty;
            this.seCadastroEdicaoLivro.Value = null;
        }
        protected void gvGerenciamentoLivros_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal LivroId = Convert.ToDecimal(e.Keys["liv_id_livro"]);
                decimal TipoLivro = Convert.ToDecimal(e.NewValues["liv_tipo_livro"]);
                decimal EditoraLivro = Convert.ToDecimal(e.NewValues["liv_editora_livro"]);
                string TituloLivro = Convert.ToString(e.NewValues["liv_nm_titulo"].ToString()); // possível erro
                decimal AutorLivro = Convert.ToDecimal(e.NewValues["liv_autor_livro"]);
                decimal PrecoLivro = Convert.ToDecimal(e.NewValues["liv_preco_livro"]);
                decimal RoyaltyLivro = Convert.ToDecimal(e.NewValues["liv_royalty_livro"]);
                string ResumoLivro = Convert.ToString(e.NewValues["liv_ds_resumo"].ToString());
                int EdicaoLivro = Convert.ToInt32(e.NewValues["liv_edicao_livro"]);


                // entender o comportamento dessas condicionais na tela.
                if (string.IsNullOrEmpty(TituloLivro))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o título do livro.')</script>");
                    e.Cancel = true;
                    return;
                }

                if (TipoLivro == 0)
                {
                    HttpContext.Current.Response.Write("<script>alert('Selecione o tipo do livro.')</script>");
                    e.Cancel = true;
                    return;
                }

                if (EditoraLivro == 0)
                {
                    HttpContext.Current.Response.Write("<script>alert('Selecione a editora do livro.')</script>");
                    e.Cancel = true;
                    return;
                }

                if (AutorLivro == 0)
                {
                    HttpContext.Current.Response.Write("<script>alert('Selecione o autor do livro.')</script>");
                    e.Cancel = true;
                    return;
                }

                if (PrecoLivro <= 0)
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe um preço válido para o livro.')</script>");
                    e.Cancel = true;
                    return;
                }

                if (RoyaltyLivro < 0)
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe um valor de royalty válido.')</script>");
                    e.Cancel = true;
                    return;
                }

                if (string.IsNullOrEmpty(ResumoLivro))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o resumo do livro.')</script>");
                    e.Cancel = true;
                    return;
                }

                if (EdicaoLivro <= 0)
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o número da edição.')</script>");
                    e.Cancel = true;
                    return;
                }

                Livro livro = new Livro(LivroId, TipoLivro, EditoraLivro, TituloLivro, PrecoLivro, RoyaltyLivro, ResumoLivro, EdicaoLivro) { };
                int qtdLinhasAfetadas = ioLivrosDAO.AtualizaLivro(livro);

                e.Cancel = true;
                this.gvGerenciamentoLivros.CancelEdit();

                CarregaDados();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na atualização do cadastro do livro." + ex);
            }
        }
        protected void gvGerenciamentoLivros_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            decimal LivroId = Convert.ToDecimal(e.Keys["liv_id_livro"]);
            Livro livro = this.ListaLivros.FirstOrDefault(a => a.liv_id_livro == LivroId);
            int qtdRegistrosExcluidos = ioLivrosDAO.RemoveLivro(livro);
            CarregaDados();

            e.Cancel = true;
            this.gvGerenciamentoLivros.CancelEdit();
        }
        protected void gvGerenciamentoLivros_CustomButtonCallBack(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal LivroId = Convert.ToDecimal(gvGerenciamentoLivros.GetRowValues(e.VisibleIndex, "liv_id_livro"));
            var livro = ioLivrosDAO.BuscaLivro(LivroId).FirstOrDefault();

            if (e.ButtonID == "btnAutorInfo")
            {

            }
            else if (e.ButtonID == "btnLivros")
            {
                Session["SessionLivroSelecionado"] = livro;

                gvGerenciamentoLivros.JSProperties["cpRedirectToLivros"] = true;
            }
        }
        // esse método é necessário? aqui ***
        protected void RedirectLivros(string idAutorString, string controlID)
        {
            switch (controlID)
            {
                case "btnLivros":
                    decimal id = Convert.ToDecimal(idAutorString);
                    AutoresSession = this.ioAutoresDAO.BuscaAutores(id).FirstOrDefault();
                    Response.Redirect("/Livraria/GerenciamentoLivros.aspx");
                    break;

                default: break;
            }
        }
    }
}