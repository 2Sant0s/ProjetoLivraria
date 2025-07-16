using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections;
using System.Collections.Generic;
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
            CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
                this.gvGerenciamentoAutores.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nm_nome);
                this.gvGerenciamentoAutores.DataBind();

            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Autores');</script>");
            }
        }
        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            if(!Page.IsValid)
            {
                return;
            }
            try
            {
                decimal IdcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;
                string isNomeAutor = this.txbCadastroNomeAutor.Text;
                string isSobrenomeAutor = this.txbCadastroSobrenomeAutor.Text;
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
        //nao funcional
        protected void gvGerenciamentoAutores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
                string nome = e.NewValues["aut_nm_nome"].ToString();
                string sobrenome = e.NewValues["aut_nm_sobrenome"].ToString();
                string email = e.NewValues["aut_ds_email"].ToString();

                if (string.IsNullOrEmpty(nome))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o nome do autor')</script>");
                    return;

                }
                if (string.IsNullOrEmpty(sobrenome))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o sobrenome do autor')</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(email))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o email do autor')</script>");
                    return;
                }

                Autores autor = new Autores(autorId, nome, sobrenome, email) { };
                int qtdLinhasAfetadas = ioAutoresDAO.AtualizaAutor(autor);

                e.Cancel = true;
                this.gvGerenciamentoAutores.CancelEdit();

                CarregaDados();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na atualização do cadastro do autor."+ex);
            }
        }

        //funcional
        protected void gvGerenciamentoAutores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
            Autores autor = this.ListaAutores.FirstOrDefault(a => a.aut_id_autor == autorId);
            int qtdRegistrosExcluidos = ioAutoresDAO.RemoveAutor(autor);
            CarregaDados();

            e.Cancel = true;
            this.gvGerenciamentoAutores.CancelEdit();
        }
        protected void gvGerenciamento_CustomButtonCallBack(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            // ...
        }
    }
}