using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MURDoX.Models
{
    public class GuildedMember
    {
        [JsonPropertyName("members")]
        public List<Member>? Members { get; set; }
    }
}
