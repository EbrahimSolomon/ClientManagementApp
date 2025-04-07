using ClientManagementApp.DAL;
using ClientManagementApp.DTOs;

namespace ClientManagementApp.BLL
{
    public class ClientManagement
    {
        private readonly ClientRepository _repository = new ClientRepository();

        public int AddClient(ClientDto client)
        {
            return _repository.AddClient(client);
        }

        public string ExportClientsToCsv(string filePath)
        {
            return _repository.ExportClientsToCsv(filePath);
        }
    }
}
