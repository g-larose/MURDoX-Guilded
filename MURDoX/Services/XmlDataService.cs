using MURDoX.Games.Blackjack;
using MURDoX.Interfaces;

namespace MURDoX.Services;

public class XmlDataService : IXmlData
{
    public async Task CreatePlayerAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Player> GetPlayerInfoAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Player>> GetAllPlayersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Player> UpdatePlayerInfoAsync()
    {
        throw new NotImplementedException();
    }

    public void DeletePlayer(int playerId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateSupportTicketAsync(Guid id)
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
}