using System.Collections.Generic;
using CogsLite.Core;

namespace CogsLite3.Models
{
    public class GamesViewModel
    {
		private readonly IEnumerable<Game> _games;

		public GamesViewModel(IEnumerable<Game> games)
		{
			_games = games;
		}

		public IEnumerable<Game> Games => _games;
    }
}
