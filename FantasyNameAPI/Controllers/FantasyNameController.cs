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
        [HttpGet("{fantasyClass}")]
        public async Task<ActionResult<FantasyItem>> GetFantasyItem(string fantasyClass)
        {
            if (!ClassExists(fantasyClass))
                return NotFound();

            var fantasyRace = await _fantasyNameContext.FantasyItems
                .FindAsync(fantasyClass);
            
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
        [HttpGet("{fantasyClass}/names")]
        public async Task<ActionResult<List<string>>> GetFantasyRaceNames(string fantasyClass)
        {
            if (!ClassExists(fantasyClass))
                return NotFound();

            var fantasyRace = await _fantasyNameContext.FantasyItems
                .FindAsync(fantasyClass);

            return fantasyRace.Names;
        }

        [HttpGet("{fantasyClass}/descriptions")]
        public async Task<ActionResult<List<string>>> GetFantasyRaceDescriptions(string fantasyClass)
        {
            if (!ClassExists(fantasyClass))
                return NotFound();

            var fantasyRace = await _fantasyNameContext.FantasyItems
                .FindAsync(fantasyClass);

            return fantasyRace.Descriptions;
        }

        private bool ClassExists(string fantasyClass)
            => Array.Exists(Classes,x => x == fantasyClass);
    }
}