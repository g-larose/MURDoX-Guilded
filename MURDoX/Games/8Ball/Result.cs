using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MURDoX.Games._8Ball
{
    public class Result
    {
        [JsonPropertyName("reading")]
        public string? Reading { get; set; }
        [JsonPropertyName("question")]
        public string? Question { get; set; }
        [JsonPropertyName("sentiment")]
        public Sentiment? Sentiment { get; set; }
    }
}
