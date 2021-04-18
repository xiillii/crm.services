using Gui.Crm.Services.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gui.Crm.Services.Data.Models
{
    public class CrmDbContext : DbContext
    {
        public CrmDbContext(DbContextOptions<CrmDbContext> opts) : base(opts)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
