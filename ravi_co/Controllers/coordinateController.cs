using Microsoft.AspNetCore.Mvc;
using ravi_co.Models;

namespace ravi_co.Controllers
{
    [Route("api/coord")]
    [ApiController]
    public class coordinateController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Coordinate> GetCos()
        {
            return new List<Coordinate>()
            {
                new Coordinate { id = 1, X = 10, Y = 20 },
                new Coordinate { id = 2, X = 15, Y = 25 },
                new Coordinate { id = 3, X = 20, Y = 30 }
            };
        }
    }
}
