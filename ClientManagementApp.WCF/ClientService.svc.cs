using ClientManagementApp.BLL;
using ClientManagementApp.DTOs;
using System.Web.Services.Description;

namespace ClientManagementApp.WCF
{
    public class ClientService : IClientService
    {
        private readonly ClientManagement _clientService = new ClientManagement();

        public int AddClient(ClientDto client)
        {
            return _clientService.AddClient(client);
        }

        public string ExportClientsToCsv(string filePath)
        {
            return _clientService.ExportClientsToCsv(filePath);
        }
    }
}
