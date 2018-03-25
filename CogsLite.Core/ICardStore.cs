using System;
using System.Collections.Generic;

namespace CogsLite.Core
{
	public interface ICardStore
	{
		Card Get(Guid cardId);
		IEnumerable<Card> Get(Guid gameId, string nameFilter, int pageNumber, int itemsPerPage);
		void Add(Card card);
		void UpdateOne(Guid id, Action<Card> updateAction);
	}
}
