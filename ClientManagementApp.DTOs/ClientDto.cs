using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementApp.DTOs
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public List<ContactDto> Contacts { get; set; }
    }
}
