using ProjetoLivraria.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;

namespace ProjetoLivraria.DAO
{
    public class EditoresDAO
    {
        SqlConnection ioConexao;
        SqlCommand ioQuery;

        // MÉTODO DE LISTAGEM
        public BindingList<Editores> BuscaEditores(decimal? adcIdEditor = null)
        {
            BindingList<Editores> loListEditores = new BindingList<Editores>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdEditor != null)
                    {
                        ioQuery = new SqlCommand("SELECT FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", adcIdEditor));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Editores loNovoEditor = new Editores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            loListEditores.Add(loNovoEditor);
                        }
                        loReader.Close();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar buscar editor.");
                }
            }
            return loListEditores;
        }
        // MÉTODO INSERÇÃO
        public int InsereEditor(Editores aoNovoEditor)
        {
            if (aoNovoEditor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO EDI_EDITORES (EDI_ID_EDITOR, EDI_NM_EDITOR, EDI_DS_EMAIL, EDI_DS_URL) " +
                        "VALUES (@idEditor, @nomeEditor, @emailEditor, urlEditor)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoNovoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoNovoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoNovoEditor.edi_ds_url));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar inserir editor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        // MÉTODO REMOÇÃO
        public int RemoveEditor(Editores aoNovoEditor)
        {
            if (aoNovoEditor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.edi_id_editor));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar excluir editor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        // MÉTODO ATUALIZAÇÃO
        public int AtualizaEditor(Editores aoNovoEditor)
        {
            if (aoNovoEditor == null)
                throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE EDI_EDITORES SET " +
                        "EDI_NM_EDITOR = @nomeEditor, " +
                        "EDI_DS_EMAIL = @emailEditor, " +
                        "EDI_DS_URL = @urlEditor " +
                        "WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoNovoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoNovoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoNovoEditor.edi_ds_url));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar atualizar informações do editor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
    }
}