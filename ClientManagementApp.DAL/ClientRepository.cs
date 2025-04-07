using ClientManagementApp.DTOs;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace ClientManagementApp.DAL
{
    public class ClientRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ClientDb"].ConnectionString;

        public int AddClient(ClientDto client)
        {
            int clientId;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Insert into Client table
                string insertClient = @"INSERT INTO Client (FirstName, LastName, Gender, DateOfBirth) 
                                        OUTPUT INSERTED.ClientId 
                                        VALUES (@FirstName, @LastName, @Gender, @DOB)";

                using (SqlCommand cmd = new SqlCommand(insertClient, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", client.LastName);
                    cmd.Parameters.AddWithValue("@Gender", client.Gender);
                    cmd.Parameters.AddWithValue("@DOB", client.DateOfBirth);
                    clientId = (int)cmd.ExecuteScalar();
                }

                // Insert all addresses
                foreach (var address in client.Addresses)
                {
                    string insertAddress = @"INSERT INTO Address 
                        (ClientId, AddressType, Street, City, Province, PostalCode, Country) 
                        VALUES 
                        (@ClientId, @AddressType, @Street, @City, @Province, @PostalCode, @Country)";

                    using (SqlCommand cmd = new SqlCommand(insertAddress, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClientId", clientId);
                        cmd.Parameters.AddWithValue("@AddressType", address.AddressType);
                        cmd.Parameters.AddWithValue("@Street", address.Street);
                        cmd.Parameters.AddWithValue("@City", address.City);
                        cmd.Parameters.AddWithValue("@Province", address.Province);
                        cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                        cmd.Parameters.AddWithValue("@Country", address.Country);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Insert all contacts
                foreach (var contact in client.Contacts)
                {
                    string insertContact = @"INSERT INTO Contact 
                        (ClientId, ContactType, ContactValue) 
                        VALUES 
                        (@ClientId, @ContactType, @ContactValue)";

                    using (SqlCommand cmd = new SqlCommand(insertContact, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClientId", clientId);
                        cmd.Parameters.AddWithValue("@ContactType", contact.ContactType);
                        cmd.Parameters.AddWithValue("@ContactValue", contact.ContactValue);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return clientId;
        }

        public string ExportClientsToCsv(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (IOException)
                {
                    throw new IOException("The export file is currently in use. Please close it and try again.");
                }
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
            SELECT 
                c.FirstName, c.LastName, c.Gender, c.DateOfBirth,
                a.AddressType, a.Street, a.City, a.Province, a.PostalCode, a.Country
            FROM Client c
            LEFT JOIN Address a ON c.ClientId = a.ClientId
            ORDER BY c.ClientId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    //writer.WriteLine("FirstName,LastName,Gender,DateOfBirth,AddressType,Street,City,Province,PostalCode,Country");

                    while (reader.Read())
                    {
                        string Escape(string value)
                        {
                            if (string.IsNullOrEmpty(value)) return "";
                            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
                            {
                                return "\"" + value.Replace("\"", "\"\"") + "\"";
                            }
                            return value;
                        }

                        string firstName = reader["FirstName"]?.ToString() ?? "";
                        string lastName = reader["LastName"]?.ToString() ?? "";
                        string gender = reader["Gender"]?.ToString() ?? "";
                        string dob = DateTime.TryParse(reader["DateOfBirth"]?.ToString(), out DateTime date)
                                     ? date.ToString("yyyy-MM-dd") : "";

                        string addressType = reader["AddressType"]?.ToString() ?? "";
                        string street = reader["Street"]?.ToString() ?? "";
                        string city = reader["City"]?.ToString() ?? "";
                        string province = reader["Province"]?.ToString() ?? "";
                        string postalCode = reader["PostalCode"]?.ToString() ?? "";
                        string country = reader["Country"]?.ToString() ?? "";

                        string line = string.Join(",",
                            Escape(firstName),
                            Escape(lastName),
                            Escape(gender),
                            Escape(dob),
                            Escape(addressType),
                            Escape(street),
                            Escape(city),
                            Escape(province),
                            Escape(postalCode),
                            Escape(country));

                        writer.WriteLine(line);
                    }
                }
            }

            return filePath;
        }
    }
}
