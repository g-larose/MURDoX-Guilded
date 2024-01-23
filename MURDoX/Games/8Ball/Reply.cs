using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MURDoX.Games._8Ball
{
    public class Reply
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("reply")]
        public string? Response { get; set; }
    }
}
