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
        LivrosEAutoresDAO ioLivrosEAutoresDAO = new LivrosEAutoresDAO();
        LivroCompletoDAO ioLivroCompletoDao = new LivroCompletoDAO();

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
                this.cbCadastroCategoria.DataSource = this.ListaTipoLivros.OrderBy(loTipoLivro => loTipoLivro.til_ds_descricao);
                this.cbCadastroCategoria.TextField = "til_ds_descricao";
                this.cbCadastroCategoria.ValueField = "til_id_tipo_livro";
                this.cbCadastroCategoria.DataBind();

                this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
                this.cbCadastroAutorLivro.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nm_nome);
                this.cbCadastroAutorLivro.TextField = "aut_nm_nome";
                this.cbCadastroAutorLivro.ValueField = "aut_id_autor";
                this.cbCadastroAutorLivro.DataBind();

                this.ListaEditoras = this.ioEditoraDAO.BuscaEditores();
                this.cbCadastroEditorLivro.DataSource = this.ListaEditoras.OrderBy(loEditor => loEditor.edi_nm_editor);
                this.cbCadastroEditorLivro.TextField = "edi_nm_editor";
                this.cbCadastroEditorLivro.ValueField = "edi_id_editor";
                this.cbCadastroEditorLivro.DataBind();

                //this.cbCadastroEditoraLivro.Items.Insert(0, new ListEditItem("Selecione...", null));
            }
            catch (Exception ex)
            {

                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Livros');</script>");
                throw new Exception(ex.Message);
            }
        }

        protected void BtnNovoLivro_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid) return;
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(l => l.liv_id_livro).First().liv_id_livro + 1;
                decimal lsIdCategoria = Convert.ToDecimal(this.cbCadastroCategoria.Value);
                decimal lsIdEditor = Convert.ToDecimal(this.cbCadastroEditorLivro.Value);
                string lsTitulo = this.tbxCadastroTituloLivro.Text;
                decimal lsPreco = Convert.ToDecimal(this.seCasdastroPrecoLivro.Text);
                decimal lsRoyalty = Convert.ToDecimal(this.seCadastroRoyaltyLivro.Text);
                string lsResumo = this.tbxCadastroResumoLivro.Text;
                int lsEdicao = Convert.ToInt32(this.seCadastroEdicaoLivro.Text);
                decimal ldcIdAutor = Convert.ToDecimal(this.cbCadastroAutorLivro.Value);

                Livro loLivro = new Livro(ldcIdLivro, lsIdCategoria, lsIdEditor, lsTitulo, lsPreco, lsRoyalty, lsResumo, lsEdicao);
                this.ioLivrosDAO.InsereLivro(loLivro);
                this.CarregaDados();

                LivrosEAutores loLeA = new LivrosEAutores(ldcIdAutor, ldcIdLivro, lsRoyalty);
                this.ioLivrosEAutoresDAO.InsereLivroEAutores(loLeA);
                // fazer update do LivroEAutores?
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!')</script>");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Livro')</script>");
                throw new Exception(ex.Message);
            }

            this.cbCadastroCategoria.SelectedIndex = -1;
            this.cbCadastroEditorLivro.SelectedIndex = -1;
            this.tbxCadastroTituloLivro.Text = String.Empty;
            this.cbCadastroAutorLivro.SelectedIndex = -1;
            this.seCasdastroPrecoLivro.Value = null;
            this.seCadastroRoyaltyLivro.Value = null;
            this.tbxCadastroResumoLivro.Text = String.Empty;
            this.seCadastroEdicaoLivro.Value = null;
        }
        protected void gvGerenciamentoLivros_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Livro')</script>");
            try
            {
                decimal LivroId = Convert.ToDecimal(e.Keys["liv_id_livro"]);
                decimal AutorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
                string TituloLivro = Convert.ToString(e.NewValues["liv_nm_titulo"]);
                decimal PrecoLivro = Convert.ToDecimal(e.NewValues["liv_vl_preco"]);
                int EdicaoLivro = Convert.ToInt32(e.NewValues["liv_nu_edicao"]);
                decimal EditoraLivro = Convert.ToDecimal(e.NewValues["liv_id_editor"]);
                decimal TipoLivro = Convert.ToDecimal(e.NewValues["liv_id_tipo_livro"]);
                decimal RoyaltyLivro = Convert.ToDecimal(e.NewValues["liv_pc_royalty"]);
                string ResumoLivro = Convert.ToString(e.NewValues["liv_ds_resumo"]);


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

                //if (AutorLivro == 0)
                //{
                //    HttpContext.Current.Response.Write("<script>alert('Selecione o autor do loLivro.')</script>");
                //    e.Cancel = true;
                //    return;
                //}

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

                Livro livro = new Livro(LivroId, TipoLivro, EditoraLivro, TituloLivro, PrecoLivro, RoyaltyLivro, ResumoLivro, EdicaoLivro)
                {
                    aut_id_autor = AutorId
                };
                int qtdLinhasAfetadas = ioLivrosDAO.AtualizaLivro(livro);

                LivrosEAutores loLeA = new LivrosEAutores(AutorId, LivroId, RoyaltyLivro);
                this.ioLivrosEAutoresDAO.AtualizaLivroEAutor(loLeA);

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
    }
}