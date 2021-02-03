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
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteBusiness _rouletteBusiness;

        public RouletteController(IRouletteBusiness rouletteBusiness)
        {
            _rouletteBusiness = rouletteBusiness ?? throw new ArgumentException(nameof(rouletteBusiness));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateRoulette(RouletteDto roulette)
        {
            return Ok(_rouletteBusiness.CreateRulette(roulette));
        }
    }
}
