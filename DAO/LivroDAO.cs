using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace ProjetoLivraria.DAO
{
    public class LivroDAO
    {
        SqlConnection ioConexao;
        SqlCommand ioQuery;

        public BindingList<Livro> buscaLivro(decimal? adcIdLivro = null)
        {
            BindingList<Livro> loListAutores = new BindingList<Livro>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdLivro != null)
                    {
                        // MÉTODO DE LISTAGEM
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livro loNovoLivro = new Livro(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3),
                                loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            loListAutores.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar livros.");
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
        public int removeLivro(Livro aoNovoLivro)
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
        public int atualizaLivro(Livro aoNovoLivro)
        {
            if (aoNovoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIV_LIVROS SET " +
                        "LIV_NM_TITULO = @nomeTitulo, " +
                        "LIV_VL_PRECO = @precoLivro, " +
                        "LIV_PC_ROYALTY = @royaltyLivro, " +
                        "LIV_DS_RESUMO = @resumoLivro, " +
                        "LIV_NU_EDICAO = @numeroEdicaoLivro " +
                        "WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    // PROV. TEM QUE CORRIGIR ALGO AQUI...
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeTitulo", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@precoLivro", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumoLivro", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@numeroEdicaoLivro", aoNovoLivro.liv_nu_edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar atualizar livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        //busca livros pelo autor
        public BindingList<Livro> FindLivrosByAutor(Autores aoNovoAutor)
        {
            BindingList<Livro> loListLivros = new BindingList<Livro>();

            if (aoNovoAutor == null)
                throw new ArgumentNullException();

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
                throw new ArgumentNullException();

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
    }
}