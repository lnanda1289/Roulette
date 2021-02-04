using Domain.Contracts;
using Domain.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteBusiness _rouletteBusiness;
        public RouletteController(IRouletteBusiness rouletteBusiness)
        {
            _rouletteBusiness = rouletteBusiness ?? throw new ArgumentException(nameof(rouletteBusiness));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("CreateRoulette")]
        public IActionResult CreateRoulette(RouletteDto roulette)
        {
            return Ok(_rouletteBusiness.CreateRulette(roulette));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("OpenRoulette")]
        public IActionResult OpenRoulette(string id)
        {
            return Ok(_rouletteBusiness.OpenRulette(id));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("CloseRoulette")]
        public IActionResult CloseRoulette(string id)
        {
            return Ok(_rouletteBusiness.CloseRulette(id));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Route("GetAllRoulettes")]
        public IActionResult GetAllRoulettes()
        {
            return Ok(_rouletteBusiness.GetAllRoulettes());
        }
    }
}
