using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using System.Collections.Generic;

namespace MethodRaid.WebUI.Interface
{
    public class ClientRepository: IClientRepository
    {
        public IEnumerable<Client> Clients { get; set; }

        public ClientRepository()
        {
            Clients = DB_Clients.Get_ListClients();
         
        }
    }
}