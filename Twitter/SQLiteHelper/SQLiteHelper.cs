using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Web.Hosting;

namespace Twitter.DBHelper
{
	public static class SQLiteHelper
	{

		public static void ExecuteNonQuery(string query)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString()))
			{
				using (SQLiteCommand command = new SQLiteCommand(connection))
				{
					command.CommandText = query;
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		public static string ExecuteScalar(string query)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString()))
			{
				using (SQLiteCommand command = new SQLiteCommand(connection))
				{
					command.CommandText = query;
					connection.Open();
					Object result = command.ExecuteScalar();
					if (result!=null)
					{
						return result.ToString();
					}
					else
					{
						return null;
					}
				}
			}
		}

		public static DataSet ExecuteQuery(string query)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString()))
			{
				using (SQLiteCommand command = new SQLiteCommand(connection))
				{
					command.CommandText = query;
					SQLiteDataAdapter da = new SQLiteDataAdapter(command);
					DataSet ds = new DataSet();
					da.Fill(ds);
					return ds;
				}
			}
		}

		private static string connectionString()
		{
			string absolutePath = HostingEnvironment.MapPath("~/App_Data/Twitter.db");
			string connString = "Data Source =" + absolutePath + "; Version=3;New=False;Compress=True;";

			return connString; //@"Data Source = D:\home\site\wwwroot\App_Data\Twitter.db;";
		}
	}
}