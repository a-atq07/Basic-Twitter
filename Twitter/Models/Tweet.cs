using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
	public class Tweet
	{
		public int ID { get; set; }
		public string Content { get; set; }
		public int TweetedBy { get; set; }
		public DateTime TweetedOn { get; set; }
		public string TweetedByName { get; set; }
	}
}