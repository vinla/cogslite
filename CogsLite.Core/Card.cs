﻿using System;

namespace CogsLite.Core
{
	public class Card : BaseObject
	{
		public Guid GameId { get; set; }
		public string Name { get; set; }
		public DateTime CreatedOn { get; set; }
	}	
}
