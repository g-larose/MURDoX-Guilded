using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MURDoX.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("createdAt")] 
        public string? CreatedAt { get; set; }
        [JsonPropertyName("banner")]
        public string? Banner { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
       
    }
}
