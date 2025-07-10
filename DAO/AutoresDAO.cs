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
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString))
            {
                try
                {
                    //abertura conexao com server
                    ioConexao.Open();

                    // verifica se o id do Autor foi informado
                    if (adcIdAutor != null)
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
    }
}