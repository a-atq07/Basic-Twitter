using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitter.Models;
using Twitter.DBHelper;
using System.Data;

namespace Twitter.DataAccess
{
	public class TweetDataAccess
	{
		public void AddTweet(Tweet tweet)
		{
			try
			{
				string query = "insert into Tweets (Content, TweetedBy, TweetedOn) values('"+
								tweet.Content + "'," +
								tweet.TweetedBy + ",'" +
								tweet.TweetedOn.ToString("MM-dd-yyyy HH:mm:ss") + "');";
				SQLiteHelper.ExecuteNonQuery(query);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<Tweet> GetTweets(int id)
		{
			try
			{
				string query = "select t.ID, t.Content, t.TweetedBy, t.TweetedOn, u.Name " +
								"from tweets t " +
								"inner join users u on u.ID=t.TweetedBy " +
								"where u.ID=" + id +
								" or u.ID in (select UserID from followers where followedBy=" + id + ")" +
								"order by t.TweetedOn desc";
				DataSet ds = SQLiteHelper.ExecuteQuery(query);
				if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
				{
					List<Tweet> tweets = new List<Tweet>();
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						Tweet tweet = new Tweet();
						tweet.ID = Convert.ToInt32(row["ID"].ToString());
						tweet.Content = row["Content"].ToString();
						tweet.TweetedOn = Convert.ToDateTime(row["TweetedOn"].ToString());
						tweet.TweetedBy = Convert.ToInt32(row["TweetedBy"].ToString());
						tweet.TweetedByName = row["Name"].ToString();

						tweets.Add(tweet);
					}
					return tweets;
				}
				return null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public List<Tweet> GetTweetsByUser(int userID)
		{
			try
			{
				string query = "select t.ID, t.Content, t.TweetedBy, t.TweetedOn, u.Name " +
								"from tweets t " +
								"inner join users u on u.ID=t.TweetedBy " +
								"where t.TweetedBy=" + userID +
								" order by t.TweetedOn desc";
				DataSet ds = SQLiteHelper.ExecuteQuery(query);
				if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
				{
					List<Tweet> tweets = new List<Tweet>();
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						Tweet tweet = new Tweet();
						tweet.ID = Convert.ToInt32(row["ID"].ToString());
						tweet.Content = row["Content"].ToString();
						tweet.TweetedOn = Convert.ToDateTime(row["TweetedOn"].ToString());
						tweet.TweetedBy = Convert.ToInt32(row["TweetedBy"].ToString());
						tweet.TweetedByName = row["Name"].ToString();

						tweets.Add(tweet);
					}
					return tweets;
				}
				return null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<Tweet> SearchTweets(string searchKey)
		{
			try
			{
				string query = "select t.ID, t.Content, t.TweetedBy, t.TweetedOn, u.Name " +
								"from tweets t " +
								"inner join users u on u.ID=t.TweetedBy " +
								"where u.Name like '%" + searchKey + "%' " +
								" or  t.Content like '%" + searchKey + "%' " +
								"order by t.TweetedOn desc";
				DataSet ds = SQLiteHelper.ExecuteQuery(query);
				if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
				{
					List<Tweet> tweets = new List<Tweet>();
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						Tweet tweet = new Tweet();
						tweet.ID = Convert.ToInt32(row["ID"].ToString());
						tweet.Content = row["Content"].ToString();
						tweet.TweetedOn = Convert.ToDateTime(row["TweetedOn"].ToString());
						tweet.TweetedBy = Convert.ToInt32(row["TweetedBy"].ToString());
						tweet.TweetedByName = row["Name"].ToString();

						tweets.Add(tweet);
					}
					return tweets;
				}
				return null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}