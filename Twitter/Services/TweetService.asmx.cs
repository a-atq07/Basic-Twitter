using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Twitter.DataAccess;
using Twitter.Models;
using Newtonsoft.Json;
using System.Web.Script.Services;

namespace Twitter.Services
{
	/// <summary>
	/// Summary description for TweetService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class TweetService : System.Web.Services.WebService
	{
		private User loggedInUser = null;
		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public Tweet AddTweet(string tweetContent)
		{
			try
			{
				int id = Convert.ToInt32(User.Identity.Name);
				loggedInUser = new UserDataAccess().GetUser(id);

				Tweet tweet = new Tweet();
				tweet.TweetedBy = loggedInUser.ID;
				tweet.Content = tweetContent;
				tweet.TweetedOn = DateTime.Now;
				tweet.TweetedByName = loggedInUser.Name;

				TweetDataAccess tweetDB = new TweetDataAccess();
				tweetDB.AddTweet(tweet);

				return tweet;//JsonConvert.SerializeObject(tweet);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet =true)]
		public List<Tweet> GetTweets()
		{
			try
			{
				int id = Convert.ToInt32(User.Identity.Name);
				TweetDataAccess tweetDB = new TweetDataAccess();
				return tweetDB.GetTweets(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
		public List<Tweet> SearchTweets(string searchKey)
		{
			try
			{
				TweetDataAccess tweetDB = new TweetDataAccess();
				return tweetDB.SearchTweets(searchKey);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
