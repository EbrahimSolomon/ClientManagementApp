using ClientManagementApp.DTOs;
using System.ServiceModel;

namespace ClientManagementApp.WCF
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        int AddClient(ClientDto client);

        [OperationContract]
        string ExportClientsToCsv(string filePath);
    }
}
