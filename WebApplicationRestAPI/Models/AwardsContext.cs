using Microsoft.EntityFrameworkCore;
using WebApplicationRestAPI;

namespace HelloWebApi.Models
{
    public class AwardsContext : DbContext
    {
        public DbSet<Award> Awards { get; set; }
        public AwardsContext(DbContextOptions<AwardsContext> options)
            : base(options)
        { }
    }
}
