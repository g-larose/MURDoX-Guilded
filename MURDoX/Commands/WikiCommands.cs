using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Genbox.Wikipedia;
using Guilded.Commands;
using MURDoX.Utils;

namespace MURDoX.Commands
{
    public class WikiCommands : CommandModule
    {
        [Command(Aliases = new string[] { "wiki", "search" })]
        [Description("search the wiki web site")]
        public async Task SearchWiki(CommandEvent invokator, string[] query)
        {
            var newQuery = string.Join(" ", query);
          string apiUrl = $"https://en.wikipedia.org/w/api.php?action=query&format=json&list=search&srsearch={newQuery}&utf8=1&prop=extracts&exintro=1";
            using var httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<WikipediaSearchResult>(jsonResult);
                    foreach (var item in result!.Query.Search)
                    {
                        var title = item.Title;
                        var snippet = item.Snippet.Sanitize();
                        var test = "";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }
    }

    public class WikipediaSearchResult
    {
        [JsonPropertyName("query")]
        public WikipediaQuery Query { get; set; }
    }

    public class WikipediaQuery
    {
        [JsonPropertyName("search")]
        public WikipediaSearchItem[] Search { get; set; }
    }

    public class WikipediaSearchItem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("snippet")]
        public string Snippet { get; set; }
    }
}
