using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitter.Models;
using Twitter.DBHelper;
using System.Data;

namespace Twitter.DataAccess
{
	public class UserDataAccess
	{

		public void AddUser(User user, string password)
		{
			try
			{
				string query = "insert into users (handle, name, email, password, Address) values('" + user.Handle + "','" + user.Name + "','" + user.Email + "','" + password + "','" + user.Address + "');";
				SQLiteHelper.ExecuteNonQuery(query);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public User ValidateUser(string userName, string password)
		{
			string query = "select ID, handle, name,  email, address from users where (Email='" + userName + "' or handle='" + userName + "') and password='" + password + "';";
			DataSet ds = SQLiteHelper.ExecuteQuery(query);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				User user = new User();
				DataRow row = ds.Tables[0].Rows[0];
				user.ID = Convert.ToInt32(row["ID"].ToString());
				user.Name = row["name"].ToString();
				user.Handle = row["handle"].ToString();
				user.Email = row["email"].ToString();
				user.Address = row["address"].ToString();
				return user;
			}
			else
			{
				return null;
			}
		}

		public User GetUser(int id)
		{
			string query = "select ID, handle, name,  email, address from users where  ID=" + id + ";";
			DataSet ds = SQLiteHelper.ExecuteQuery(query);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				User user = new User();
				DataRow row = ds.Tables[0].Rows[0];
				user.ID = Convert.ToInt32(row["ID"].ToString());
				user.Name = row["name"].ToString();
				user.Handle = row["handle"].ToString();
				user.Email = row["email"].ToString();
				user.Address = row["address"].ToString();
				return user;
			}
			else
			{
				return null;
			}
		}

		public List<DetailedUser> SearchDetailedUsers(int userID, string searchInput)
		{
			string query = "select u.ID, u.Name, u.Handle, u.Address, " +
						   " (select COUNT(ID) from Followers where UserID = u.ID and FollowedBy=" + userID + ") IsFollowed," +
						   " (select ifnull(count(ID), 0) from Followers where UserID = u.ID) Followers, " +
						   " (select ifnull(count(ID), 0) from Followers where FollowedBy = u.ID) Following " +
						   " from Users u " +
						   " where u.ID<>" + userID +
						   " and (u.Name like '%" + searchInput + "%' " +
						   " or u.Handle like '%" + searchInput + "%'); ";
			DataSet ds = SQLiteHelper.ExecuteQuery(query);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				List<DetailedUser> detailedUsers = new List<DetailedUser>();
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					DetailedUser user = new DetailedUser();
					user.ID = Convert.ToInt32(row["ID"].ToString());
					user.Name = row["Name"].ToString();
					user.Handle = row["Handle"].ToString();
					user.Address = row["Address"].ToString();
					user.IsFollowed = Convert.ToInt32(row["IsFollowed"].ToString()) == 1;
					user.FollowerCount = Convert.ToInt32(row["Followers"]);
					user.FollowingCount = Convert.ToInt32(row["Following"]);

					detailedUsers.Add(user);
				}
				return detailedUsers;
			}
			else
			{
				return null;
			}
		}

		public DetailedUser GetDetailedUser(int userID, int id)
		{
			string query = "select u.ID, u.Name, u.Handle, u.Address, " +
						   " (select COUNT(ID) from Followers where UserID = u.ID and FollowedBy=" + id + ") IsFollowed," +
						   " (select ifnull(count(ID), 0) from Followers where UserID = u.ID) Followers, " +
						   " (select ifnull(count(ID), 0) from Followers where FollowedBy = u.ID) Following " +
						   " from Users u " +
						   " where u.ID=" + userID + ";";
			DataSet ds = SQLiteHelper.ExecuteQuery(query);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				DataRow row = ds.Tables[0].Rows[0];
				DetailedUser user = new DetailedUser();
				user.ID = Convert.ToInt32(row["ID"].ToString());
				user.Name = row["Name"].ToString();
				user.Handle = row["Handle"].ToString();
				user.Address = row["Address"].ToString();
				user.IsFollowed = Convert.ToInt32(row["IsFollowed"].ToString()) == 1;
				user.FollowerCount = Convert.ToInt32(row["Followers"]);
				user.FollowingCount = Convert.ToInt32(row["Following"]);

				return user;
			}
			else
			{
				return null;
			}
		}

		public List<DetailedUser> GetFollowers(int userID)
		{
			string query = "select u.ID, u.Name, u.Handle, u.Address, " +
						   " (select COUNT(ID) from Followers where UserID = u.ID and FollowedBy=" + userID + ") IsFollowed," +
						   " (select ifnull(count(ID), 0) from Followers where UserID = u.ID) Followers, " +
						   " (select ifnull(count(ID), 0) from Followers where FollowedBy = u.ID) Following " +
						   " from Users u " +
						   " join Followers f on u.ID=f.FollowedBy "+ 
						   " where f.UserID=" + userID + ";";
			DataSet ds = SQLiteHelper.ExecuteQuery(query);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				List<DetailedUser> detailedUsers = new List<DetailedUser>();
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					DetailedUser user = new DetailedUser();
					user.ID = Convert.ToInt32(row["ID"].ToString());
					user.Name = row["Name"].ToString();
					user.Handle = row["Handle"].ToString();
					user.Address = row["Address"].ToString();
					user.IsFollowed = Convert.ToInt32(row["IsFollowed"].ToString()) == 1;
					user.FollowerCount = Convert.ToInt32(row["Followers"]);
					user.FollowingCount = Convert.ToInt32(row["Following"]);

					detailedUsers.Add(user);
				}
				return detailedUsers;
			}
			else
			{
				return null;
			}
		}

		public List<DetailedUser> GetUsersFollowedBy(int userID)
		{
			string query = "select u.ID, u.Name, u.Handle, u.Address, " +
						   " (select COUNT(ID) from Followers where UserID = u.ID and FollowedBy=" + userID + ") IsFollowed," +
						   " (select ifnull(count(ID), 0) from Followers where UserID = u.ID) Followers, " +
						   " (select ifnull(count(ID), 0) from Followers where FollowedBy = u.ID) Following " +
						   " from Users u " +
						   " join Followers f on u.ID=f.UserID " +
						   " where f.FollowedBy=" + userID + ";";
			DataSet ds = SQLiteHelper.ExecuteQuery(query);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				List<DetailedUser> detailedUsers = new List<DetailedUser>();
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					DetailedUser user = new DetailedUser();
					user.ID = Convert.ToInt32(row["ID"].ToString());
					user.Name = row["Name"].ToString();
					user.Handle = row["Handle"].ToString();
					user.Address = row["Address"].ToString();
					user.IsFollowed = Convert.ToInt32(row["IsFollowed"].ToString()) == 1;
					user.FollowerCount = Convert.ToInt32(row["Followers"]);
					user.FollowingCount = Convert.ToInt32(row["Following"]);

					detailedUsers.Add(user);
				}
				return detailedUsers;
			}
			else
			{
				return null;
			}
		}


		public bool ToggleFollowing(int userID, int followedBy)
		{
			string checkQuery = "select ID from followers where userID=" + userID + " and FollowedBy=" + followedBy + " ;";

			if (SQLiteHelper.ExecuteScalar(checkQuery) != null)
			{
				string query = "delete from followers where userID=" + userID + " and FollowedBy=" + followedBy + " ;";
				SQLiteHelper.ExecuteNonQuery(query);
				return false;
			}
			else
			{
				string query = "insert into followers (userID,FollowedBy) values(" + userID + "," + followedBy + ") ;";
				SQLiteHelper.ExecuteNonQuery(query);
				return true;
			}
		}


		public bool ValidateUserName(string userName)
		{
			string query = "select ID from users where Handle='" + userName + "';";
			string result = SQLiteHelper.ExecuteScalar(query);

			return result != null;
		}

		public bool ValidateUserEmail(string email)
		{
			string query = "select ID from users where Email='" + email + "';";
			string result = SQLiteHelper.ExecuteScalar(query);

			return result != null;
		}
	}
}