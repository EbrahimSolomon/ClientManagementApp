============================================================
               CLIENT MANAGEMENT APP (ASP.NET 4.8)
============================================================

Project Description:
--------------------
This is a multi-client capture web application built using ASP.NET Web Forms (.NET Framework 4.8). It allows the user to:
- Add multiple clients with associated addresses and contacts.
- Perform client-side and server-side validations (e.g. required fields, numeric mobile number).
- Export all clients with addresses (excluding contact numbers) to a CSV file.

Solution Structure:
-------------------
ClientManagementApp.sln
│
├── ClientManagementApp.UI        (Web Forms Frontend)
│     ├── Assessment.aspx         (Landing Page)
│     ├── AddClient.aspx          (Multi-client form)
│     ├── App_Data                (CSV exports saved here)
│     └── Web.config              (Configured to start on Assessment.aspx)
│
├── ClientManagementApp.WCF      (WCF Service Layer)
│     ├── ClientService.svc
│     └── Interfaces and Contracts
│
├── ClientManagementApp.BLL      (Business Logic Layer)
│
├── ClientManagementApp.DAL      (Data Access Layer)
│     └── ClientRepository.cs    (Handles DB operations and export)
│
├── ClientManagementApp.DTOs     (Shared DTOs)

How to Run:
-----------
1. Open `ClientManagementApp.sln` in Visual Studio.
2. Make sure SQL Server (e.g. SQLExpress) is running.
3. Ensure the database `ClientsDb` exists with the required tables (Client, Address, Contact).
4. Press **F5** or click **Start**.
5. The app will launch and load `Assessment.aspx` by default.
6. From there, you can click to open the client capture form.
7. Once clients are captured, click **Export to CSV** to generate a file in `App_Data/ClientsExport.csv`.

Note:
-----
- Ensure that `ClientsExport.csv` is closed before exporting again.
- You must have proper read/write access to the `App_Data` folder.

============================================================
