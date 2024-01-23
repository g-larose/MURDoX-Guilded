using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Users;
using MURDoX.Interfaces;
using MURDoX.Models;
using User = Guilded.Users.User;

namespace MURDoX.Services
{
    public class GuildedDataService : IGuildedDataProvider
    {
        public Task<Member> GetMemberAsync(HashId serverId, HashId memId)
        {
            throw new NotImplementedException();
        }

        public async Task<GuildedMember> GetServerMembersAsync(HashId serverId)
        {
            using var httpClient = new HttpClient();
            var token = GetBotToken();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var membersJson = await httpClient.GetStringAsync(new Uri($"https://www.guilded.gg/api/v1/servers/{serverId}/members"));
            var members = JsonSerializer.Deserialize<GuildedMember>(membersJson);
            return members;
        }

        public async Task<string> GetMemberXp(string serverId, string memberId)
        {
            using var httpClient = new HttpClient();
            var token = GetBotToken();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                var memXp = await httpClient.GetStringAsync(
                    new Uri($"https://www.guilded.gg/api/v1/servers/{serverId}/members/{memberId}/xp"));
                var hello = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            return "";
        }

        public async Task<string> GetMemberRolesAsync(HashId serverId, HashId userId)
        {
            using var httpClient = new HttpClient();
            var token = GetBotToken();
            string roles = "";
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                roles = await httpClient.GetStringAsync(
                    new Uri($"https://www.guilded.gg/api/v1/servers/{serverId}/members/{userId}/roles"));
                return roles;
            }
            catch (Exception e)
            {
                return roles;
            }

        }

        public async Task<string> GetUserServersAsync(string userId)
        {
            var servers = "";
            using var httpClient = new HttpClient();
            var token = GetBotToken();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            try
            {
                servers = await httpClient.GetStringAsync(
                    new Uri($"https://www.guilded.gg/api/v1/users/{userId}/servers"));
                var test = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return servers;
        }

        public string GetBotToken()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            var json = File.OpenRead(path);
            var token = JsonSerializer.Deserialize<ConfigJson>(json!).Token!;
            return token!;
        }

    }
}
