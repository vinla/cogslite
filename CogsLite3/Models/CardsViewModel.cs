using System.Collections.Generic;

namespace CogsLite3.Models
{
    public class CardsViewModel
    {
		private readonly IEnumerable<Card> _cards;

		public CardsViewModel(IEnumerable<Card> cards)
		{
			_cards = cards;
		}

		public IEnumerable<Card> Cards => _cards;
    }
}
