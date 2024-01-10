using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MURDoX.Games.Blackjack;

namespace MURDoX.Interfaces
{
    public interface IXmlData
    {
        Task CreatePlayerAsync();
        Task<Player> GetPlayerInfoAsync();
        Task<List<Player>> GetAllPlayersAsync();
        Task<Player> UpdatePlayerInfoAsync();
        void DeletePlayer(int playerId);
        Task<bool> CreateSupportTicketAsync(Guid id);
    }
}
