using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using to_do_api.Modules;

namespace to_do_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class testController : ControllerBase
    {
        [HttpGet("{text}")]
        public ActionResult<Card> Get(string text)
        {
            Card firstCard = new Card($"{text}");

            return firstCard;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"This is get with id = {id}";
        }

        [HttpDelete]
        public string Delete(int id)
        {
            return $"This is delete with id = {id}";
        }
    }
}

