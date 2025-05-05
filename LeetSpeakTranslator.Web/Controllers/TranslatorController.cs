using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using LeetSpeakTranslator.Web.DAL;
using System.Data;
using System.Linq;
using System.Text;
using System.Text;
using System.IO;


namespace LeetSpeakTranslator.Web.Controllers
{
    public class TranslatorController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        // GET: Translator
        public ActionResult Index()
        {
            return View();
        }


        // POST: Translator
        [HttpPost]
        public async Task<JsonResult> Translate(string inputText)
        {
            string apiUrl = "https://api.funtranslations.com/translate/yoda.json";
            var data = new FormUrlEncodedContent(new[]
            {
        new KeyValuePair<string, string>("text", inputText)
    });

            string translated = "";
            string errorMessage = "";

            try
            {
                var response = await client.PostAsync(apiUrl, data);
                var result = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(result);
                if (json["contents"] != null)
                {
                    translated = json["contents"]["translated"].ToString();

                    // Save to DB
                    var db = new DAL.DatabaseHelper();
                    db.SaveTranslation(inputText, translated);
                }
                else
                {
                    errorMessage = "Unexpected response: " + result;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "API Error: " + ex.Message;
            }

            return Json(new { success = errorMessage == "", translated, error = errorMessage });
        }

        // GET /Translator/History
        public ActionResult History()
        {
            var db = new DatabaseHelper();
            var records = db.GetAllTranslations();
            return View(records);
        }

        public ActionResult ExportToCsv()
        {
            var db = new DAL.DatabaseHelper();
            var records = db.GetAllTranslations();

            var sb = new StringBuilder();

            // Add CSV header
            sb.AppendLine("OriginalText,TranslatedText,CreatedAt");

            foreach (var record in records)
            {
                sb.AppendLine($"\"{record.OriginalText}\",\"{record.TranslatedText}\",\"{record.CreatedAt}\"");
            }

            var csvBytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(csvBytes, "text/csv", "TranslationHistory.csv");
        }
    }
}



