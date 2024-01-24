using Guilded.Base;
using Guilded.Servers;

namespace MURDoX.Interfaces
{
    public interface IXmlData
    {
        Task<bool> CreateMemberAsync(Member member);
        Task<Member> GetMemberInfoAsync(HashId userId);
        Task<List<Member>> GetAllMembersAsync();
        Task<Member> UpdateMemberInfoAsync(HashId userId);
        void DeleteMember(HashId userId);
        Task<bool> CreateSupportTicketAsync(Guid id, HashId authorId);
        Task<bool> AddMemberWarning(HashId userId, string reason);
    }
}
