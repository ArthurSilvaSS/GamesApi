using GamesAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PlatformsController(AppDbContext context)
        {
            _context = context;
        }
    }

}
