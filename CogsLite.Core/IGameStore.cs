using System.Collections.Generic;

namespace CogsLite.Core
{
	public interface IGameStore
    {
		IEnumerable<Game> Get(string nameFilter, int pageNumber, int itemsPerPage);
		void Add(Game game);
	}
}
