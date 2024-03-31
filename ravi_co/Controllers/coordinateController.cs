using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ravi_co.Data;
using ravi_co.Models;
using ravi_co.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ravi_co.Controllers
{
    [Route("api/coord")]
    [ApiController]
    public class coordinateController : ControllerBase
    {
        private readonly CoordinateContext _context;

        public coordinateController(CoordinateContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coordinate>>> GetCos()
        {
            var coordinates = await _context.points.ToListAsync();
            return Ok(coordinates);
        }

        [HttpGet("{_id:int}", Name = "GetCoord")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Coordinate>> GetCo(int _id)
        {
            var coordinate = await _context.points.FindAsync(_id);
            if (coordinate == null)
            {
                return NotFound();
            }
            return Ok(coordinate);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Coordinate>> CreateCord([FromBody] Coordinate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.points.Add(dto);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetCoord", new { _id = dto.id }, dto);
        }

        [HttpGet("latest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Coordinate>> GetLatestCoordinate()
        {
            var latestCoordinate = await _context.points.OrderByDescending(c => c.id).FirstOrDefaultAsync();
            if (latestCoordinate == null)
            {
                var defaultCoordinate = new Coordinate { id = 1, X = 0, Y = 0 };
                _context.points.Add(defaultCoordinate);
                await _context.SaveChangesAsync();
                return Ok(defaultCoordinate);
            }
            return Ok(latestCoordinate);
        }

        [HttpPost("reset")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> ResetCoordinates()
        {
            _context.points.RemoveRange(_context.points);
            await _context.SaveChangesAsync();

            var defaultCoordinate = new Coordinate { id = 1, X = 0, Y = 0 };
            _context.points.Add(defaultCoordinate);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
