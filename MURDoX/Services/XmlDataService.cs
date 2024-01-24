using System.Xml.Linq;
using Guilded.Base;
using MURDoX.Games.Blackjack;
using MURDoX.Interfaces;
using MURDoX.Models;
using Member = Guilded.Servers.Member;

namespace MURDoX.Services;

public class XmlDataService : IXmlData
{
    private string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Xml", "user_data.xml");
    public async Task<bool> CreateMemberAsync(Member member)
    {
        if (!File.Exists(xmlPath))
        {
            var stream = new FileStream(xmlPath, FileMode.OpenOrCreate);
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("user",
                    new XAttribute("id", member!.Id),
                    new XAttribute("name", member!.Name),
                    new XAttribute("created_at", member!.CreatedAt),
                    new XAttribute("warnings", "0"),
                    new XElement("rolesIds",
                        member.RoleIds.Select(x => new XElement("roleId", x)))));
            await doc.SaveAsync(stream, SaveOptions.None, CancellationToken.None);
            return true;
        }

        return false;
    }

    public async Task<Member> GetMemberInfoAsync(HashId userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Member>> GetAllMembersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Member> UpdateMemberInfoAsync(HashId userId)
    {
        throw new NotImplementedException();
    }

    public void DeleteMember(HashId userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateSupportTicketAsync(Guid id, HashId userId)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Xml", "support_tickets.xml");
        if (File.Exists(path))
        {
            //file already exists, load documnet
            //when loading content , we check to see if the member is in the list of members.
        }
        else
        {
            //file doesn't exist, create a new  document.
        }

        return false;
    }

    public Task<bool> AddMemberWarning(HashId userId, string reason)
    {
        throw new NotImplementedException();
    }
}