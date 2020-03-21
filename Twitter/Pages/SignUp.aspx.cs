using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twitter.DataAccess;
using Twitter.Models;

namespace Twitter.Pages
{
	public partial class SignUp : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnSignUp_Click(object sender, EventArgs e)
		{
			User newUser = new User();
			newUser.Name = txtName.Text.Trim();
			newUser.Handle = txtUserName.Text.Trim();
			newUser.Email = txtEmail.Text.Trim();
			newUser.Address = txtAddress.Text.Trim();
			string password = txtPassword.Text.Trim();

			try
			{
				UserDataAccess userDB = new UserDataAccess();
				userDB.AddUser(newUser, password);
				Response.Redirect("Login.aspx?NewSignUp=1");
			}
			catch (Exception ex)
			{
				lblErrorMessage.Text = ex.Message;
			}
		}

		[WebMethod()]
		public static bool ValidateUserName(string userName)
		{
			UserDataAccess userDB = new UserDataAccess();
			return userDB.ValidateUserName(userName);
		}

		[WebMethod()]
		public static bool ValidateUserEmail(string email)
		{
			UserDataAccess userDB = new UserDataAccess();
			return userDB.ValidateUserEmail(email);
		}
	}
}