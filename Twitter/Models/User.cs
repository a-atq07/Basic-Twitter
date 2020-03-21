using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
	public class User
	{
		public int ID { get; set; }
		public string Handle { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
	}
}