using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagementApp.DTOs
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        public int ClientId { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
    }
}
