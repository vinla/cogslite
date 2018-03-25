using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CogsLite.Core
{
	public interface IGameStore
    {
		Game Get(Guid gameId);
		IEnumerable<Game> Get(string nameFilter, int pageNumber, int itemsPerPage);
		void Add(Game game);
		void UpdateOne(Guid id, Action<Game> updateAction);
	}
}
