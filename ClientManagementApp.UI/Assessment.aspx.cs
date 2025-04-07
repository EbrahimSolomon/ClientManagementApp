using System;

namespace ClientManagementApp.UI
{
    public partial class Assessment : System.Web.UI.Page
    {
        protected void btnGoToAddClient_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddClient.aspx");
        }
    }
}
