using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MURDoX.Games.Trivia
{
    public class TriviaModel
    {
        public string? type { get; set; }
        public string? difficulty { get; set; }
        public string? category { get; set; }
        public string? question { get; set; }
        public string? correct_answer { get; set; }
        public JArray? incorrect_answers { get; set; }

    }
}
