using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MURDoX.Games.Trivia;
using Newtonsoft.Json;

namespace MURDoX.Helpers
{
    public class TriviaHelper
    {
        public static List<TriviaModel> HandleQuestionRequest(string? response)
        {
            dynamic? quests = JsonConvert.DeserializeObject(response);
            List<TriviaModel> Questions = new();

            if (quests == null) return Questions;
            foreach (var quest in quests["results"])
            {
                TriviaModel question = new();
                question.category = quest.category;
                question.question = quest.question;
                question.type = quest.type;
                question.difficulty = quest.difficulty;
                question.correct_answer = quest.correct_answer;
                question.incorrect_answers = quest.incorrect_answers;
                Questions.Add(question);
            }

            return Questions;
        }
        public static async Task<string> MakeQuestionRequest(string category = "general_knowledge", string difficulty = "easy")
        {
           // var cat = Converter.ConvertCategoryString(category);
            var EndPoint = $"https://opentdb.com/api.php?amount=10&category={category}&difficulty={difficulty}&type=multiple";
            HttpClient request = new HttpClient();

            HttpResponseMessage response = await request.GetAsync(EndPoint);
            string content = await response.Content.ReadAsStringAsync();
            content = Sanitize(content);
            return content;
        }

        private static string Sanitize(string content)
        {
            Regex matches = new Regex("(&#?[a-zA-Z0-9]+;)");
            string output = matches.Replace(content, "");

            return output;
        }
    }
}
