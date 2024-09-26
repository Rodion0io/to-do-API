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
                return StatusCode(500, "error 500");
            }
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> Post([FromBody] CardDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("error");
                }

                await _cardService.Add(model);
                var cards = _cardService.GenerateCard();
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error 500");
            }
        }

        [HttpPut("changeIndicator")]
        public async Task<IActionResult> Put(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("error");
                }

                await _cardService.ChangeIndicator(id);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error 500");
            }
        }

        [HttpPut("changeText")]
        public async Task<IActionResult> Put(string id, string newText)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("error");
                }

                if (string.IsNullOrEmpty(newText))
                {
                    return BadRequest("error");
                }

                await _cardService.ChangeText(id, newText);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error 500");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("error");
                }

                await _cardService.DeleteCard(id);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error 500");
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await _cardService.DeleteTable();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error 500");
            }
        }

        [HttpPost("newList")]
        public async Task<IActionResult> UploadList([FromBody] List<Card> cards)
        {
            try
            {
                if (cards == null || cards.Count == 0)
                {
                    return BadRequest("error");
                }

                await _cardService.AddCards(cards);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "error 500");
            }
        }
    }
}