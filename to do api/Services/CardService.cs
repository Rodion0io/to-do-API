using System;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using to_do_api.Modules;
using to_do_api.DTO;

namespace to_do_api.Services
{
	public class CardService : ICardService
	{

		private readonly Context _context;

		public CardService(Context context)
		{
			_context = context;
		}

		public async Task<List<Card>> GetFullTable()
		{
			return await _context.Cards.ToListAsync();
		}

		public CardDTO[] GenerateCard()
		{
			return _context.Cards.Select(x => new CardDTO
			{
				Text = x.Text,
			}).ToArray();
		}

		public async Task Add(CardDTO model)
		{
			await _context.Cards.AddAsync(new Card
			{
				Text = model.Text,
			});
			await _context.SaveChangesAsync();
		}

		public async Task ChangeIndicator(string id)
		{
			var card = await _context.Cards.FindAsync(Guid.Parse(id));

			if (card != null)
			{
				if (card.IsSelected)
				{
					card.IsSelected = false;
					await _context.SaveChangesAsync();
				}
				else
				{
					card.IsSelected = true;
					await _context.SaveChangesAsync();
				}
			}
			else
			{
				throw new Exception("Card not found");
			}
		}

		public async Task ChangeText(string id, string newText)
		{
			var card = await _context.Cards.FindAsync(Guid.Parse(id));

			if (card != null)
			{
				card.Text = newText;
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception("Card not found");
			}
		}

		public async Task DeleteCard(string id)
		{
			var card = await _context.Cards.FindAsync(Guid.Parse(id));
		
			if (card != null)
			{
				_context.Cards.Remove(card);
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception("Card not found");
			}
		}

		public async Task DeleteTable()
		{
			var table = _context.Cards.ToList();
			_context.Cards.RemoveRange(table);
			await _context.SaveChangesAsync();
		}
		
		public async Task AddCards(List<Card> cards)
		{
			if (cards == null || cards.Count == 0)
			{
				throw new InvalidDataException("Error");
			}
			else if (_context.Cards.ToList().Count != 0)
			{
				await DeleteTable();
				await _context.Cards.AddRangeAsync(cards);
				await _context.SaveChangesAsync();
			}
			else
			{
				await _context.Cards.AddRangeAsync(cards);
				await _context.SaveChangesAsync();
			}
		}
	}
}