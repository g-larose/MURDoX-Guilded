using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MURDoX.Games._8Ball
{
    public class Sentiment
    {
        [JsonPropertyName("score")]
        public int? Score { get; set; }
        [JsonPropertyName("comparative")]
        public int? Comparative { get; set; }
        [JsonPropertyName("calculation")]
        public List<object>? Calculation { get; set; }
        [JsonPropertyName("tokens")]
        public List<string>? Tokens { get; set; }
        [JsonPropertyName("words")]
        public List<object>? Words { get; set; }
        [JsonPropertyName("positive")]
        public List<object>? Positive { get; set; }
        [JsonPropertyName("negative")]
        public List<object>? Negative { get; set; }
    }
}
