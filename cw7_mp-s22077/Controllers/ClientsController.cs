using cw7_mp_s22077.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw7_mp_s22077.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClientsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveClient(int id)
        {
            bool removed = await _dbService.RemoveClient(id);
            if (removed)
                return Ok("Removed Trip");
            else
                return BadRequest("The client has trips planned");
        }

        
    }
}
