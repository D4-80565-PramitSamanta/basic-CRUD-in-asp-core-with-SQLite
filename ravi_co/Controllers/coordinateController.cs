using Microsoft.AspNetCore.Mvc;
using ravi_co.Data;
using ravi_co.Models;
using ravi_co.Models.DTOs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ravi_co.Controllers
{
    [Route("api/coord")]
    [ApiController]
    /// commenting out will cause validation failure 
    public class coordinateController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CoordinateDTO>> GetCos()
        {
            return Ok(Coord_Store.list);
        }

        [HttpGet("{_id:int}" , Name = "GetCoord")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<Coordinate> GetCo(int _id)
        {
            

            if (_id == 0)
            {
                return BadRequest();
            }
            
            var x = Coord_Store.list.FirstOrDefault(a => a.id == _id);
            if (x == null)
            {
                return NotFound();
            }
            return  Ok(x);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Coordinate> CreateCord([FromBody] Coordinate dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            if (dto == null)
            {
                return BadRequest();
            }

            int newId;
            if (Coord_Store.list.Count == 0)
            {
                newId = 1;
            }
            else
            {
                newId = Coord_Store.list.Max(a => a.id) + 1;
            }

            dto.id = newId;
            Coord_Store.list.Add(dto);
            return CreatedAtRoute("GetCoord", new { _id = dto.id }, dto);
        }
        [HttpGet("latest")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Coordinate> GetLatestCoordinate()
        {
            if (Coord_Store.list.Count == 0)
            {
                var defaultCoordinate = new Coordinate { id = 1, X = 0, Y = 0 };
                Coord_Store.list.Add(defaultCoordinate);
                return Ok(defaultCoordinate);
            }

            var latestCoordinate = Coord_Store.list.OrderByDescending(c => c.id).FirstOrDefault();
            return Ok(latestCoordinate);
        }
        [HttpPost("reset")]
        [ProducesResponseType(200)]
        public ActionResult ResetCoordinates()
        {
            Coord_Store.list.Clear();

            Coordinate defaultCoordinate = new Coordinate
            {
                id = 1,
                X = 0,
                Y = 0
            };
            Coord_Store.list.Add(defaultCoordinate);

            return Ok();
        }
    }
}
