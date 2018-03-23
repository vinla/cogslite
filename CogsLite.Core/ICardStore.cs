using System;
using System.Collections.Generic;

namespace CogsLite.Core
{
	public interface ICardStore
	{
		IEnumerable<Card> Get(Guid gameId, string nameFilter, int pageNumber, int itemsPerPage);
		void Add(Card card);
	}
}
