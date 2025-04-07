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
                        Addresses = new[] {
                    new AddressDto {
                        AddressType = addressTypes[i],
                        Street = streets[i],
                        City = cities[i],
                        Province = provinces[i],
                        PostalCode = postalCodes[i],
                        Country = countries[i]
                    }
                },
                        Contacts = new[] {
                    new ContactDto {
                        ContactType = contactTypes[i],
                        ContactValue = contactValues[i]
                    }
                }
                    };

                    proxy.AddClient(client);
                }

                lblStatus.Visible = true;
                lblStatus.Text = "All clients saved successfully!";
            }
            catch (Exception ex)
            {
                lblStatus.Visible = true;
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Error saving clients: " + ex.Message;
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
