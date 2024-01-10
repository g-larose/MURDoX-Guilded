using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Guilded.Base.Embeds;
using Guilded.Commands;
using MURDoX.Converters;
using MURDoX.Helpers;
using MURDoX.Extensions;
namespace MURDoX.Games.Trivia
{
    public class TriviaGame : CommandModule
    {
        [Command("trivia")]
        [Description("basic trivia game")]
        public async Task Trivia(CommandEvent invokation)
        {
            var questions = new List<TriviaModel>();
            try
            {
                var cat = CategoryConverter.ConvertCategoryString("general knowledge");
                var questionRequest = await TriviaHelper.MakeQuestionRequest(cat, "easy");
                questions = TriviaHelper.HandleQuestionRequest(questionRequest);
                string[] shuffledAnswers = new String[4];
                shuffledAnswers[0] = questions[0].incorrect_answers[0].ToString();
                shuffledAnswers[1] = questions[0].incorrect_answers[1].ToString();
                shuffledAnswers[2] = questions[0].incorrect_answers[2].ToString();
                shuffledAnswers[3] = questions[0].correct_answer;
                shuffledAnswers.Shuffle();
                EmbedField embedField1 = new EmbedField("Answer", shuffledAnswers[0], true);
                EmbedField embedField2 = new EmbedField("Answer", shuffledAnswers[1], true);
                EmbedField embedField3 = new EmbedField("Answer", shuffledAnswers[2], true);
                EmbedField embedField4 = new EmbedField("Answer", shuffledAnswers[3], true);

                var fields = new List<EmbedField>();
                fields.Add(embedField1);
                fields.Add(embedField2);
                fields.Add(embedField3);
                fields.Add(embedField4);

                var embed = new Embed()
                {
                    Description = questions[0].question,
                    Fields = fields,
                    Color = await ShuffleHelper.GetRandomEmbedColorAsync()
                };
                await invokation.CreateMessageAsync(embed);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
