using Domain.Contracts;
using Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult CreateRoulette()
        {
            return Ok(_rouletteBusiness.CreateRulette());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("OpenRoulette")]
        public IActionResult OpenRoulette(int id)
        {
            return Ok(_rouletteBusiness.OpenRulette(id));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("CloseRoulette")]
        public IActionResult CloseRoulette(int id)
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

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("CreateBet")]
        public IActionResult CreateBet(BetDto betDto)
        {
            if (betDto.Number >= 0 && betDto.Number <= 36 && betDto.Stake <= 10000)
            {
                return Ok(_rouletteBusiness.CreateBet(betDto));
            }
            return BadRequest();
        }
    }
}
