using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Genbox.Wikipedia;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Commands;
using Guilded.Permissions;
using MURDoX.Services;
using MURDoX.Utils;

namespace MURDoX.Commands
{
    public class WikiCommands : CommandModule
    {
        private GuildedDataService gds = new();
        [Command(Aliases = new string[] { "wiki", "search" })]
        [Description("search the wiki web site")]
        public async Task SearchWiki(CommandEvent invokator, string[] query)
        {
            var newQuery = string.Join(" ", query);
            string apiUrl = $"https://en.wikipedia.org/w/api.php?action=query&format=json&list=search&srsearch={newQuery}&utf8=1&prop=extracts&exintro=2";
            using var httpClient = new HttpClient();
            var embed = new Embed();
            
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    embed.SetTitle($"Wiki Search: `{newQuery}`");
                    embed.SetColor(EmbedColors.GetColor("gray", Color.Gray));
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<WikipediaSearchResult>(jsonResult);
                    var rnd = new Random();

                    if (result.Query.Search.Length < 1)
                    {
                        await invokator.ReplyAsync($"no results found for query: `{newQuery}`");
                    }
                    else
                    {
                        var index = rnd.Next(1, result!.Query.Search.Length);

                        var title = result.Query.Search[index].Title;
                        var snippet = result.Query.Search[index].Snippet.SanitizeTags();
                        embed.SetDescription("Wiki Search is a work in progress.");
                        embed.AddField("Title", title, true);
                        embed.AddField("Summary", $"{snippet}...", true);

                        var test = "";

                        embed.SetFooter("MURDoX watching everything");
                        embed.SetTimestamp(DateTime.Now);
                        await invokator.ReplyAsync(null, false, false, embed);
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
