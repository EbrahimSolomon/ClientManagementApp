<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Assessment.aspx.cs" Inherits="ClientManagementApp.UI.Assessment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assessment Landing</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container text-center mt-5">
            <h1 class="display-5 mb-4">Welcome to My Assessment</h1>
            <p class="lead mb-5">This page is part of my ASP.NET Web Forms assessment project for capturing and managing clients.</p>
            <asp:Button ID="btnGoToAddClient" runat="server" Text="Proceed to Add Clients" CssClass="btn btn-primary btn-lg" OnClick="btnGoToAddClient_Click" />
        </div>
    </form>
</body>
</html>
