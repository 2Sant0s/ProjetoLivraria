using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivroAutorDAO
    {
        SqlConnection ioConexao;
        SqlCommand ioQuery;

        //MÉTODO LISTAGEM
        public BindingList<LivroAutor> BuscaLivroAutor(decimal? adcIdLivroAutor = null)
        {
            BindingList<LivroAutor> loListAutores = new BindingList<LivroAutor>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdLivroAutor != null)
                    {
                        ioQuery = new SqlCommand("SELECT FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idLivroAutor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivroAutor", adcIdLivroAutor));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            LivroAutor loNovoLivroAutor = new LivroAutor(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2));
                            loListAutores.Add(loNovoLivroAutor);
                        }
                        loReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar ...");
                }
            }
            return loListAutores;
        }
        //MÉTODO INSERÇÃO
        public int InsereLivroAutor(LivroAutor aoNovoLivroAutor)
        {
            if (aoNovoLivroAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIA_LIVRO_AUTOR (LIA_ID_AUTOR, LIA_ID_LIVRO, LIA_PC_ROYALTY) " +
                        "VALUES (@idAutor, @idLivro, @royaltyLivroAutor)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivroAutor", aoNovoLivroAutor.lia_pc_royalty));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar inserir ...");
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int RemoveLivroAutor(LivroAutor aoNovoLivroAutor)
        {
            if (aoNovoLivroAutor == null)
                throw new ArgumentNullException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoLivroAutor.lia_id_autor));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar excluir ...");
                }
            }
            return liQtdRegistrosInseridos;
        }
        public int AtualizaLivroAutor (LivroAutor aoNovoLivroAutor)
        {
            if (aoNovoLivroAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIA_LIVRO_AUTOR SET LIA_ID_LIVRO = @idLivro, LIA_PC_ROYALTY = @royaltyLivroAutor   " +
                        "WHERE LIA_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivroAutor ", aoNovoLivroAutor.lia_pc_royalty));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar atualizar ...");
                }
            }
            return liQtdRegistrosInseridos;
        }
    }
}