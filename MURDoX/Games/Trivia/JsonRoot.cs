using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MURDoX.Games.Trivia
{
    public class JsonRoot
    {
        public int response_code { get; set; }
        public JArray results { get; set; }
    }
}
