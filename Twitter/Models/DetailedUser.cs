using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
	public class DetailedUser: User
	{
		public bool IsFollowed { get; set; }
		public int FollowerCount { get; set; }
		public int FollowingCount { get; set; }
	}
}