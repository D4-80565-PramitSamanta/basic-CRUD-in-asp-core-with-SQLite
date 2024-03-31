using Microsoft.AspNetCore.Mvc;
using ravi_co.Models;
using ravi_co.Models.DTOs;
using System.Linq;

namespace ravi_co.Controllers
{
    [Route("api/coord")]
    [ApiController]
    public class coordinateController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CoordinateDTO>> GetCos()
        {
            return Ok(new List<CoordinateDTO>()
            {
                new CoordinateDTO { X = 10, Y = 20 },
                new CoordinateDTO { X = 15, Y = 25 },
                new CoordinateDTO { X = 20, Y = 30 }
            });
        }

        [HttpGet("{_id:int}")]
        public ActionResult<Coordinate> GetCo(int _id)
        {
            if (_id == 0)
            {
                return BadRequest();
            }
            var list =  new List<Coordinate>()
            {
                new Coordinate {id = 1, X = 10, Y = 20 },
                new Coordinate {id = 2, X = 15, Y = 25 },
                new Coordinate {id = 3, X = 20, Y = 30 }
            };
            var x = list.FirstOrDefault(a => a.id == _id);
            if (x == null)
            {
                return NotFound();
            }
            return  Ok(x);
        }
    }
}
