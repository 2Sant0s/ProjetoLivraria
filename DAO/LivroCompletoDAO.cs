using DevExpress.Xpo.DB.Helpers;
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
    public class LivroCompletoDAO
    {
        SqlConnection ioConexao;
        SqlCommand ioQuery;
        public BindingList<LivroCompleto> BuscaLivrosCompletos(decimal? adcIdAutor = null)
        {
            BindingList<LivroCompleto> listaLivroCompleto = new BindingList<LivroCompleto>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    string sql = @"
                    SELECT 
                    LIV.LIV_ID_LIVRO,
                    LIV.LIV_NM_TITULO,
                    LIV.LIV_VL_PRECO,
                    LIV.LIV_NU_EDICAO,
                    EDI.EDI_NM_EDITOR,
                    CAT.TIL_DS_DESCRICAO,
                    LIV.LIV_PC_ROYALTY,
                    AUT.AUT_NM_NOME,
                    LIV.LIV_DS_RESUMO
                FROM LIV_LIVROS LIV
                INNER JOIN EDI_EDITORES EDI ON LIV.LIV_ID_EDITOR = EDI.EDI_ID_EDITOR
                INNER JOIN TIL_TIPO_LIVRO CAT ON LIV.LIV_ID_TIPO_LIVRO = CAT.TIL_ID_TIPO_LIVRO
                INNER JOIN LIA_LIVRO_AUTOR LIA ON LIV.LIV_ID_LIVRO = LIA.LIA_ID_LIVRO
                INNER JOIN AUT_AUTORES AUT ON LIA.LIA_ID_AUTOR = AUT.AUT_ID_AUTOR
            ";

                    ioQuery = new SqlCommand("SELECT \r\n                    LIV.LIV_ID_LIVRO,\r\n                    LIV.LIV_NM_TITULO,\r\n                    LIV.LIV_VL_PRECO,\r\n                    LIV.LIV_NU_EDICAO,\r\n                    EDI.EDI_NM_EDITOR,\r\n                    CAT.TIL_DS_DESCRICAO,\r\n                    LIV.LIV_PC_ROYALTY,\r\n                    AUT.AUT_NM_NOME,\r\n                    LIV.LIV_DS_RESUMO\r\n                FROM LIV_LIVROS LIV\r\n                INNER JOIN EDI_EDITORES EDI ON LIV.LIV_ID_EDITOR = EDI.EDI_ID_EDITOR\r\n                INNER JOIN TIL_TIPO_LIVRO CAT ON LIV.LIV_ID_TIPO_LIVRO = CAT.TIL_ID_TIPO_LIVRO\r\n                INNER JOIN LIA_LIVRO_AUTOR LIA ON LIV.LIV_ID_LIVRO = LIA.LIA_ID_LIVRO\r\n                INNER JOIN AUT_AUTORES AUT ON LIA.LIA_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcIdAutor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            LivroCompleto loNovoAutor = new LivroCompleto(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetDecimal(2), loReader.GetInt32(3), loReader.GetString(4), loReader.GetString(5), loReader.GetDecimal(6), loReader.GetString(7), loReader.GetString(8));
                            listaLivroCompleto.Add(loNovoAutor);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar livros completos: " + ex.Message);
                }
            }

            return listaLivroCompleto;
        }

    }
}