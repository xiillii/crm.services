using System.Linq;
using Gui.Crm.Services.Data.Entities;
using Gui.Crm.Services.Data.Models;

namespace Gui.Crm.Services.Data.Repositories
{
    public class EFCrmRepository : ICrmRepository
    {
        private CrmDbContext context;

        public EFCrmRepository(CrmDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
        public IQueryable<Category> Categories => context.Categories;
    }
}
