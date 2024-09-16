using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace moviesCore5CRUD.Models
{
    public class ApplicationDBcontecst:DbContext
    {
        public ApplicationDBcontecst(DbContextOptions<ApplicationDBcontecst> options):base(options) 
        {
          
        }
        public DbSet<Genera>generas { get; set; }
        public DbSet<movie> movies { get; set; }

    }
}
