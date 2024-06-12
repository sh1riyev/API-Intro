﻿using System;
namespace API_Intro.Models
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;

	}
}

