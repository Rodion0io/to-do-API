using System;
using to_do_api.Modules;
using to_do_api.DTO;

namespace to_do_api.Services
{
	public interface ICardService
	{
		public Task<List<Card>> GetFullTable();
        public Task Add(CardDTO model);
        public Task ChangeIndicator(string id);
        public CardDTO[] GenerateCard();
	}
}

