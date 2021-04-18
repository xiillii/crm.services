using Gui.Crm.Services.Data.Entities;
using Gui.Crm.Services.Data.Models;
using System.Collections.Generic;

namespace Gui.Crm.Services.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private CrmDbContext context;

        public ProductRepository(CrmDbContext ctx) => context = ctx;


        public Product Get(long id) => context.Products.Find(id);

        public IEnumerable<Product> GetAll() => context.Products;

        public void Create(Product newDataObject)
        {
            context.Add(newDataObject);
            context.SaveChanges();
        }

        public void Update(Product changeDataObject)
        {
            context.Update(changeDataObject);
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            context.Remove(Get(id));
            context.SaveChanges();
        }
    }
}
