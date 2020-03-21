using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitter.DataAccess;
using Twitter.Models;

namespace Twitter.Pages
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				Response.Redirect("Home.aspx");
			}
			string newSignUp = Request.QueryString["NewSignUp"];
			if (newSignUp=="1")
			{
				lblSuccessMessage.Text = "Signed up successfully! Please login using username or email.";
			}
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			UserDataAccess userDB = new UserDataAccess();
			try
			{
				User user = userDB.ValidateUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());
				if (user != null)
				{
					Session["User"] = user;
					FormsAuthentication.RedirectFromLoginPage(user.ID.ToString(), false);
				}
				else
				{
					lblErrorMessage.Text = "Invalid credentials!!";
				}
			}
			catch (Exception ex)
			{
				lblErrorMessage.Text = ex.Message;
			}
		}
	}
}