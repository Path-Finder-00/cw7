using cw7_mp_s22077.Models.DTO;
using cw7_mp_s22077.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw7_mp_s22077.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }

        [HttpPost]
        [Route("{id}/clients")]
        public async Task<IActionResult> AssignClient(SomeSortOfClientTrip someSortOfClientTrip)
        {
            var added = await _dbService.AssignClient(someSortOfClientTrip);
            if (added.Equals("Ok"))
                return Ok();
            else
                return BadRequest(added);
        }
    }
}
