using System;
namespace API_Intro.Models
{
	public class Book : BaseEntity
	{
		public string Name { get; set; }
		public int PageCount { get; set; }
	}
}

