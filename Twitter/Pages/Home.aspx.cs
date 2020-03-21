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
	public partial class Home : System.Web.UI.Page
	{
		private User loggedInUser = null;
		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInUser = getLoggedInUser();
			lblUserName.Text = loggedInUser.Name;
			hdnUserID.Value = loggedInUser.ID.ToString();
		}

		protected void btnLogout_Click(object sender, EventArgs e)
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			Response.Redirect("Login.aspx");
		}

		protected void lnkSearch_Click(object sender, EventArgs e)
		{

		}

		private User getLoggedInUser()
		{
			User loggedInUser = null;
			if (HttpContext.Current.Session["User"] != null)
			{
				loggedInUser = (User)(HttpContext.Current.Session["User"]);
			}
			else
			{
				int id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
				UserDataAccess userDB = new UserDataAccess();
				loggedInUser = userDB.GetUser(id);
				HttpContext.Current.Session["User"] = loggedInUser;
			}
			return loggedInUser;
		}
	}
}