using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guilded;
using Guilded.Client;
using Guilded.Content;

namespace MURDoX.Interfaces
{
    public interface IGuildedMessageService
    {
        Task HandleMessageAsync(AbstractGuildedClient client, Message message, Guid channel);
    }
}
