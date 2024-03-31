using Microsoft.EntityFrameworkCore;
using ravi_co.Models;

namespace ravi_co.Data
{
    public class CoordinateContext : DbContext
    {
        public CoordinateContext(DbContextOptions<CoordinateContext> options):base(options)
        {
            
        }

        public DbSet<Coordinate> points { get; set; }
    }
}
