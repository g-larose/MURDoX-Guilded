using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guilded.Base;
using MURDoX.Models;
using User = Guilded.Users.User;

namespace MURDoX.Interfaces
{
    public interface IGuildedDataProvider
    {
        Task<Member> GetMemberAsync(HashId serverId, HashId memId);
        Task<GuildedMember> GetServerMembersAsync(HashId serverId);
        Task<string> GetMemberXp(string serverId, string memberId);
        Task<string> GetMemberRolesAsync(HashId serverId, HashId userId);
        Task<string> GetUserServersAsync(string userId);
        public string GetBotToken();
    }
}
