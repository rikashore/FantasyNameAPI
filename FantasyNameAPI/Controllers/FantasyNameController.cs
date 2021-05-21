using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyNameAPI.Database;
using FantasyNameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FantasyNameAPI.Controllers
{
    [ApiController]
    [Route("api/fantasyname")]
    public class FantasyNameController : ControllerBase
    {
        private static readonly string[] Classes = 
        {
            "skeleton", "zombie", "knight", "assassin", "warrior"
        };

        private readonly ILogger<FantasyNameController> _logger;
        private readonly FantasyNameApiContext _fantasyNameContext;
        private readonly Random _random;

        public FantasyNameController(ILogger<FantasyNameController> logger, FantasyNameApiContext fantasyNameApiContext, Random random)
        {
            _logger = logger;
            _fantasyNameContext = fantasyNameApiContext;
            _random = random;
        }

        // GET api/fantasyname/race
        [HttpGet("{race}")]
        public async Task<ActionResult<FantasyItem>> GetFantasyItem(string race)
        {
            if (!ClassExists(race))
                return NotFound();

            var fantasyRace = await _fantasyNameContext.FantasyItems
                .FindAsync(race);
            
            var name = fantasyRace.Names[_random.Next(fantasyRace.Names.Count)];
            var description = fantasyRace.Descriptions[_random.Next(fantasyRace.Descriptions.Count)];

            return new FantasyItem
            {
                Name = name,
                Description = description,
                Race = fantasyRace.Race
            };
        }

        // GET api/fantasyname/race/names
        [HttpGet("{race}/names")]
        public async Task<ActionResult<List<string>>> GetFantasyRaceNames(string race)
        {
            if (!ClassExists(race))
                return NotFound();

            var fantasyRace = await _fantasyNameContext.FantasyItems
                .FindAsync(race);

            return fantasyRace.Names;
        }

        [HttpGet("{race}/descriptions")]
        public async Task<ActionResult<List<string>>> GetFantasyRaceDescriptions(string race)
        {
            if (!ClassExists(race))
                return NotFound();

            var fantasyRace = await _fantasyNameContext.FantasyItems
                .FindAsync(race);

            return fantasyRace.Descriptions;
        }

        private bool ClassExists(string race)
            => Array.Exists(Classes,x => x == race);
    }
}