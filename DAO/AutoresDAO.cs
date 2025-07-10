using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;

namespace ProjetoLivraria.DAO
{
    public class AutoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Autores> BuscaAutores(decimal? adcIdAutor = null) //tipos de valor nao aceitam null (?) para 
        {
            // lista de autores
            BindingList<Autores> loListAutores = new BindingList<Autores>();

            // conexao
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    //abertura conexao com server
                    ioConexao.Open();

                    // MÉTODO LISTAGEM
                    if (adcIdAutor != null) //verifica se o id do Autor foi informado
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES where AUT_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcIdAutor));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", ioConexao);
                    }

                    //leitor de dados do SQL Server
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loNovoAutor = new Autores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            loListAutores.Add(loNovoAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) autor(s).");
                }
            }
            return loListAutores;
        }
        public int InsereAutor(Autores aoNovoAutor)
        {
            if (aoNovoAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                //MÉTODO INSERÇÃO
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO AUT_AUTORES (AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) " +
                        "VALUES (@idAutor, @nomeAutor, @sobrenomeAutor, @emailAutor)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.aut_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", aoNovoAutor.aut_nm_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoNovoAutor.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoNovoAutor.aut_ds_email));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar cadastrar novo autor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        //MÉTODO EXCLUSÃO
        public int RemoveAutor(Autores aoNovoAutor)
        {
            if (aoNovoAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.aut_id_autor));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar excluir autor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        //MÉTODO ATUALIZACAO
        public int AtualizaAutor(Autores aoNovoAutor)
        {
            if (aoNovoAutor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.aut_id_autor));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar atualizar autor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
    }

}
