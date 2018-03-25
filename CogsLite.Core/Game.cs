using System;

namespace CogsLite.Core
{
	public class Game : BaseObject
    {
		public string Name { get; set; }
		public DateTime CreatedOn { get; set; }	
		public int CardCount { get; set; }
    }
}
