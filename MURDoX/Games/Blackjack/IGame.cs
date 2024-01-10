using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MURDoX.Games.Blackjack
{
    public interface IGame
    {
        public List<Card> Deck { get; set; }
        public List<Player> Players { get; set; }
    }
}
