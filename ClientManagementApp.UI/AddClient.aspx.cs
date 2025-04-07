using ClientManagementApp.UI.ClientServiceReference;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ClientManagementApp.UI
{
    public partial class AddClient : System.Web.UI.Page
    {
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var firstNames = Request.Form.GetValues("FirstName");
            var lastNames = Request.Form.GetValues("LastName");
            var genders = Request.Form.GetValues("Gender");
            var dobs = Request.Form.GetValues("DateOfBirth");

            var addressTypes = Request.Form.GetValues("AddressType");
            var streets = Request.Form.GetValues("Street");
            var cities = Request.Form.GetValues("City");
            var provinces = Request.Form.GetValues("Province");
            var postalCodes = Request.Form.GetValues("PostalCode");
            var countries = Request.Form.GetValues("Country");

            var contactTypes = Request.Form.GetValues("ContactType");
            var contactValues = Request.Form.GetValues("ContactValue");

            var breaks = Request.Form.GetValues("ClientBreak");

            int addressIndex = 0;
            int contactIndex = 0;

            try
            {
                ClientServiceClient proxy = new ClientServiceClient();

                for (int i = 0; i < firstNames.Length; i++)
                {
                    var client = new ClientDto
                    {
                        FirstName = firstNames[i],
                        LastName = lastNames[i],
                        Gender = genders[i],
                        DateOfBirth = dobs[i],
                        Addresses = new AddressDto[0],
                        Contacts = new ContactDto[0]
                    };

                    // Manually build address list per client
                    var addrList = new List<AddressDto>();
                    while (addressIndex < addressTypes.Length && !string.IsNullOrWhiteSpace(addressTypes[addressIndex]))
                    {
                        addrList.Add(new AddressDto
                        {
                            AddressType = addressTypes[addressIndex],
                            Street = streets[addressIndex],
                            City = cities[addressIndex],
                            Province = provinces[addressIndex],
                            PostalCode = postalCodes[addressIndex],
                            Country = countries[addressIndex]
                        });
                        addressIndex++;
                    }

                    var contList = new List<ContactDto>();
                    while (contactIndex < contactTypes.Length && !string.IsNullOrWhiteSpace(contactTypes[contactIndex]))
                    {
                        contList.Add(new ContactDto
                        {
                            ContactType = contactTypes[contactIndex],
                            ContactValue = contactValues[contactIndex]
                        });
                        contactIndex++;
                    }

                    client.Addresses = addrList.ToArray();
                    client.Contacts = contList.ToArray();

                    proxy.AddClient(client);

                    // Skip over the ClientBreak (if exists)
                    if (breaks != null && i < breaks.Length)
                    {
                        addressIndex++;
                        contactIndex++;
                    }
                }

                lblStatus.Visible = true;
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "All clients and nested data saved successfully!";
            }
            catch (Exception ex)
            {
                lblStatus.Visible = true;
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Error: " + ex.Message;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string exportPath = Server.MapPath("~/App_Data/ClientsExport.csv");
                ClientServiceClient proxy = new ClientServiceClient();
                string result = proxy.ExportClientsToCsv(exportPath);

                lblStatus.Visible = true;
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "CSV export successful: " + result;
            }
            catch (Exception ex)
            {
                ShowError("Export failed: " + ex.Message);
            }
        }

        private void ShowError(string message)
        {
            lblStatus.Visible = true;
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = message;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
