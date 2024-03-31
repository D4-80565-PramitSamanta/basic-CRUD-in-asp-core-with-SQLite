using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ravi_co.Models
{

    public class Coordinate
    {
        public int id { get; set; }
        [Required]
        public int X { get; set; }
        [Required]
        public int Y { get; set; }

    }
}
