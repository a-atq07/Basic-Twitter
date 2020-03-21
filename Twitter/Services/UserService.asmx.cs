using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Twitter.DataAccess;
using Twitter.Models;

namespace Twitter.Services
{
	/// <summary>
	/// Summary description for UserService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class UserService : System.Web.Services.WebService
	{

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
		public List<DetailedUser> SearchUsers(string searchKey)
		{
			try
			{
				int id = Convert.ToInt32(User.Identity.Name);
				UserDataAccess userDB = new UserDataAccess();
				return userDB.SearchDetailedUsers(id, searchKey);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
		public List<DetailedUser> GetFollowers()
		{
			try
			{
				int id = Convert.ToInt32(User.Identity.Name);
				UserDataAccess userDB = new UserDataAccess();
				return userDB.GetFollowers(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
		public List<DetailedUser> GetUsersFollowedBy()
		{
			try
			{
				int id = Convert.ToInt32(User.Identity.Name);
				UserDataAccess userDB = new UserDataAccess();
				return userDB.GetUsersFollowedBy(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
		public bool ToggleFollowing(int userID)
		{
			int id = Convert.ToInt32(User.Identity.Name);
			UserDataAccess userDB = new UserDataAccess();
			return userDB.ToggleFollowing(userID, id);
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
		public object UserProfile(int userID)
		{
			try
			{
				int id = Convert.ToInt32(User.Identity.Name);

				UserDataAccess userDB = new UserDataAccess();
				DetailedUser user = userDB.GetDetailedUser(userID, id);

				TweetDataAccess tweetDB = new TweetDataAccess();
				List<Tweet> tweets = tweetDB.GetTweetsByUser(userID);

				return new { User = user, Tweets = tweets };
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
