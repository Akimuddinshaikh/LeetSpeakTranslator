using System;

namespace LeetSpeakTranslator.Web.Models
{
    public class TranslationRecord
    {
        public int Id { get; set; }
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
