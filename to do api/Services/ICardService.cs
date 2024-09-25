using System;
using to_do_api.Modules;

namespace to_do_api.Services
{
	public interface ICardService
	{
        public Task Add(Card model);
        // public Task UpIndicator(string id);
    }
}

