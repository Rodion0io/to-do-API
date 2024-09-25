using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using to_do_api.DTO;
using to_do_api.Modules;
using to_do_api.Services;

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

        [HttpGet("getList")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cards = await _cardService.GetFullTable();
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> Post([FromBody] CardDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Card model is null.");
                }

                await _cardService.Add(model);
                var cards = _cardService.GenerateCard();
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("changeIndicator")]
        public async Task<IActionResult> Put(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Card ID is null or empty.");
                }

                await _cardService.ChangeIndicator(id);
                return Ok("Indicator changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("changeText")]
        public async Task<IActionResult> Put(string id, string newText)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Card ID is null or empty.");
                }

                if (string.IsNullOrEmpty(newText))
                {
                    return BadRequest("New text is null or empty.");
                }

                await _cardService.ChangeText(id, newText);
                return Ok("Text changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Card ID is null or empty.");
                }

                await _cardService.DeleteCard(id);
                return Ok("Card deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await _cardService.DeleteTable();
                return Ok("Table cleared successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("newList")]
        public async Task<IActionResult> UploadList([FromBody] List<Card> cards)
        {
            try
            {
                if (cards == null || cards.Count == 0)
                {
                    return BadRequest("Card list is null or empty.");
                }

                await _cardService.AddCards(cards);
                return Ok("List uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}