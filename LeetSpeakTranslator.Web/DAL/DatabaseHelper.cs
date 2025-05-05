using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LeetSpeakTranslator.Web.Models;

namespace LeetSpeakTranslator.Web.DAL
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LeetSpeakDB"].ConnectionString;
        }

        public void SaveTranslation(string original, string translated)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertTranslationRecord", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OriginalText", original);
                cmd.Parameters.AddWithValue("@TranslatedText", translated);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<TranslationRecord> GetAllTranslations()
        {
            var list = new List<TranslationRecord>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM TranslationRecords ORDER BY CreatedAt DESC", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TranslationRecord
                        {
                            Id = (int)reader["Id"],
                            OriginalText = reader["OriginalText"].ToString(),
                            TranslatedText = reader["TranslatedText"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                        });
                    }
                }
            }

            return list;
        }

    }
}
