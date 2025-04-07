# Client Management App

This is a .NET Framework 4.8 Web Forms application that allows users to **capture multiple clients**, each with multiple **addresses** and **contacts**, and **export client data to a CSV file** (excluding contact information).

## 💼 Assessment Purpose

This application was developed as part of a technical assessment and **strictly adheres to the .NET Framework 4.8** architecture and **WCF service** usage. The goal was to follow best practices in Web Forms development, separation of concerns, and service-oriented design.

---

## ⚙️ Technologies Used

- ASP.NET Web Forms (.NET Framework 4.8)
- WCF (Windows Communication Foundation)
- ADO.NET (No Entity Framework or LINQ)
- Bootstrap 5 for styling
- SQL Server for data storage

---

## 🛠️ Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/EbrahimSolomon/ClientManagementApp.git
   cd ClientManagementApp
SQL Server Setup

Run the script inside SQL Scripts/Initial.sql to create and seed the database (ClientsDb).

Ensure your connection string in Web.config matches your SQL Server setup:

xml
Copy
Edit
<connectionStrings>
  <add name="ClientDb" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=ClientsDb;Integrated Security=True;" />
</connectionStrings>
Run the App

Open the solution in Visual Studio.

Right-click on Assessment.aspx and choose "Set As Start Page".

Run the application.

✨ Features
Capture multiple clients with:

Multiple addresses

Multiple contacts

Client-side and server-side form validation

Export clients and addresses to CSV (contacts excluded)

Built-in styling using Bootstrap 5

WCF service used for all business logic

🧪 Validations
Mobile numbers must be numeric

Required fields are enforced

Date of Birth cannot be in the future

📦 Notes
WCF service follows a layered architecture

No use of Entity Framework, LINQ, or stored procedures

Adheres strictly to assessment requirements

📧 Author
Ebrahim Solomon
GitHub Profile

📝 License
This project was created for demonstration and educational purposes only.

---
