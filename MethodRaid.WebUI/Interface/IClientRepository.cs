using MethodRaid.Domain.Models;
using System.Collections.Generic;

namespace MethodRaid.WebUI.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> Clients { get; }
    }
}
