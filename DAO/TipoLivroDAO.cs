using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;

namespace ProjetoLivraria.DAO
{
    public class TipoLivroDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<TipoLivro> BuscaTipoLivro(decimal? adcIdTipoLivro = null)
        {
            BindingList<TipoLivro> loTipoLivro = new BindingList<TipoLivro>();

            // MÉTODO LISTAGEM
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    if (adcIdTipoLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdTipoLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            TipoLivro loNovoTipoLivro = new TipoLivro(loReader.GetDecimal(0), loReader.GetString(1));
                            loTipoLivro.Add(loNovoTipoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o tipo de livro.");
                }
            }
            return loTipoLivro;
        }
        // MÉTODO INSERÇÃO
        public int InsereTipoLivro(TipoLivro aoNovoTipoLivro)
        {
            if (aoNovoTipoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO TIL_TIPO_LIVRO (TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO)" +
                        " VALUES (@idTipoLivro, @tipoLivroDescricao)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@tipoLivroDescricao", aoNovoTipoLivro.til_ds_descricao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    {
                        throw new Exception("Erro ao tentar cadastrar novo tipo de livro");
                    }
                }
                return liQtdRegistrosInseridos;
            }
        }
        // MÉTODO REMOÇÃO
        public int RemoveTipoLivro(TipoLivro aoNovoTipoLivro)
        {
            if (aoNovoTipoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM TIL_TIPO_LIVRO WHERE ID = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoTipoLivro.til_id_tipo_livro));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar remover tipo do livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        // MÉTODO ATUALIZAÇÃO
        public int atualizaTipoLivro(TipoLivro aoNovoTipoLivro)
        {
            if (aoNovoTipoLivro == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE FROM TIL_TIPO_LIVRO SET TIL_DS_DESCRICAO = @descricaoTipoLivro WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoNovoTipoLivro.til_ds_descricao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar atualizar tipo de livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }
    }
}
