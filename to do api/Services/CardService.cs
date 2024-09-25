using System;
using to_do_api.Modules;
namespace to_do_api.Services
{
	public class CardService : ICardService
	{

		private readonly Context _context;

		public CardService(Context context)
		{
			_context = context;
		}

        public async Task Add(Card model)
		{
			await _context.Cards.AddAsync(model);
			await _context.SaveChangesAsync();
		}

  //       public async Task UpIndicator(string id)
		// {
		// 	await _context.FindAsync(id);
		// }
    }
}

