using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using to_do_api.DTO;
using to_do_api.Modules;
using to_do_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace to_do_api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly ICardService _cardService;

        public ValuesController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<List<Card>> Get()
        {
            return await _cardService.GetFullTable();
        }

        [HttpPost]
        public async Task<IEnumerable<CardDTO>> Post([FromBody] CardDTO model)
        {
            await _cardService.Add(model);
            return _cardService.GenerateCard();
        }

        [HttpPut]
        public async Task Put(string id)
        {
            await _cardService.ChangeIndicator(id);
        }
    }
}

