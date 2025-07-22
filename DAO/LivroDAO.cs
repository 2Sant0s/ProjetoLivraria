using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivroDAO
    {
        SqlConnection ioConexao;
        SqlCommand ioQuery;

        public BindingList<Livro> BuscaLivro(decimal? adcIdLivro = null)
        {
            BindingList<Livro> loListAutores = new BindingList<Livro>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    if (adcIdLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand(@"
               
       SELECT 
         LIV.LIV_ID_LIVRO,
         LIV.LIV_ID_TIPO_LIVRO,
         LIV.LIV_ID_EDITOR,
         LIV.LIV_NM_TITULO,
         LIV.LIV_VL_PRECO,
         LIV.LIV_PC_ROYALTY,
         LIV.LIV_DS_RESUMO,
		 EDI.EDI_NM_EDITOR,
         LIV.LIV_NU_EDICAO,
         AUT.AUT_NM_NOME
     FROM LIV_LIVROS LIV
	  JOIN LIA_LIVRO_AUTOR LIA ON LIV.LIV_ID_LIVRO = LIA.LIA_ID_LIVRO
	  JOIN AUT_AUTORES AUT ON LIA_ID_AUTOR = AUT.AUT_ID_AUTOR
	  JOIN EDI_EDITORES EDI on LIV.LIV_ID_EDITOR = EDI.EDI_ID_EDITOR
", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            decimal idLivro = loReader.IsDBNull(0) ? 0 : loReader.GetDecimal(0);
                            decimal tipoLivro = loReader.IsDBNull(1) ? 0 : loReader.GetDecimal(1);
                            decimal idEditor = loReader.IsDBNull(2) ? 0 : loReader.GetDecimal(2);
                            string titulo = loReader.IsDBNull(3) ? "" : loReader.GetString(3);
                            decimal preco = loReader.IsDBNull(4) ? 0 : loReader.GetDecimal(4);
                            decimal royalty = loReader.IsDBNull(5) ? 0 : loReader.GetDecimal(5);
                            string resumo = loReader.IsDBNull(6) ? "" : loReader.GetString(6);
                            string nomeEditor = loReader.IsDBNull(7) ? "" : loReader.GetString(7);   // se quiser salvar no modelo
                            int edicao = loReader.IsDBNull(8) ? 0 : loReader.GetInt32(8);
                            string nomeAutor = loReader.IsDBNull(9) ? "" : loReader.GetString(9);

                            Livro loNovoLivro = new Livro(idLivro, tipoLivro, idEditor, titulo, preco, royalty, resumo, edicao, nomeAutor, nomeEditor);

                            loListAutores.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar livros. " + ex.Message);
                }
            }
            return loListAutores;
        }
        // MÉTODO INSERÇÃO
        public int InsereLivro(Livro aoNovoLivro)
        {
            if (aoNovoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // ORGANIZAR ESSE BLOCO DE CÓDIGO.
                    ioQuery = new SqlCommand(@"INSERT INTO LIV_LIVROS (LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, LIV_ID_EDITOR, LIV_NM_TITULO, " +
                        "LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO ) " +
                        "VALUES (@idLivro, @idTipoLivro, @idEditor, @nomeTitulo, @precoLivro, @royaltyLivro, @resumoLivro, @numeroEdicaoLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeTitulo", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@precoLivro", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumoLivro", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@numeroEdicaoLivro", aoNovoLivro.liv_nu_edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar inserir livro");
                }
            }
            return liQtdRegistrosInseridos;
        }
        // MÉTODO REMOÇÃO
        public int RemoveLivro(Livro aoNovoLivro)
        {
            if (aoNovoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar remover livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        // MÉTODO ATUALIZAÇÃO
        public int AtualizaLivro(Livro aoNovoLivro)
        {
            if (aoNovoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIV_LIVROS SET LIV_ID_TIPO_LIVRO = @idTipoLivro, LIV_ID_EDITOR = @idEditor, LIV_NM_TITULO = @nomeTitulo, LIV_VL_PRECO = @valorPreco, LIV_PC_ROYALTY = @royalty, LIV_DS_RESUMO = @resumo, LIV_NU_EDICAO = @numeroEdicao WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    // PROV. TEM QUE CORRIGIR ALGO AQUI...
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeTitulo", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@valorPreco", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@numeroEdicao", aoNovoLivro.liv_nu_edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar atualizar livro." +ex.Message);
                }
            }
            return liQtdRegistrosInseridos;
        }
        //busca livros pelo autor
        public BindingList<Livro> FindLivrosByAutor(Autores aoNovoAutor)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            if (aoNovoAutor == null)
                throw new NullReferenceException();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS LIV JOIN LIA_LIVRO_AUTOR AUT ON LIV.LIV_ID_LIVRO = AUT.LIA_ID_LIVRO " +
                        "WHERE LIA_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.aut_id_autor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livro loLivro = new Livro(
                                loReader.GetDecimal(0),
                                loReader.GetDecimal(1),
                                loReader.GetDecimal(2),
                                loReader.GetString(3),
                                loReader.GetDecimal(4),
                                loReader.GetDecimal(5),
                                loReader.GetString(6),
                                loReader.GetInt32(7)
                            );
                            loListLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar livros pelo autor.");
                }
            }
            return loListLivros;
        }
        //teste com editores
        public BindingList<Livro> FindLivrosByEditor(Editores aoNovoEditor)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            if (aoNovoEditor == null)
                throw new NullReferenceException();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS LIV WHERE LIV.LIV_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.edi_id_editor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livro loLivro = new Livro(
                                loReader.GetDecimal(0),
                                loReader.GetDecimal(1),
                                loReader.GetDecimal(2),
                                loReader.GetString(3),
                                loReader.GetDecimal(4),
                                loReader.GetDecimal(5),
                                loReader.GetString(6),
                                loReader.GetInt32(7)
                            );
                            loListLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar livros pelo autor.");
                }
            }
            return loListLivros;
        }
        public BindingList<Livro> FindLivrosByCategoria(TipoLivro aoNovoTipoLivro)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            if (aoNovoTipoLivro == null)
                throw new NullReferenceException();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS LIV JOIN TIL_TIPO_LIVRO TIL on " +
                        "LIV.LIV_ID_TIPO_LIVRO = TIL.TIL_ID_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoTipoLivro.til_id_tipo_livro));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livro loLivro = new Livro(
                                loReader.GetDecimal(0),
                                loReader.GetDecimal(1),
                                loReader.GetDecimal(2),
                                loReader.GetString(3),
                                loReader.GetDecimal(4),
                                loReader.GetDecimal(5),
                                loReader.GetString(6),
                                loReader.GetInt32(7)
                            );
                            loListLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar livros pelo autor.");
                }
            }
            return loListLivros;
        }
    }
}