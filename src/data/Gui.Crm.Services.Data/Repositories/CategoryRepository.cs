using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gui.Crm.Services.Data.Entities;
using Gui.Crm.Services.Data.Models;

namespace Gui.Crm.Services.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private CrmDbContext context;

        public CategoryRepository(CrmDbContext ctx) => context = ctx;

        public Category Get(long id) => context.Categories.Find(id);

        public IEnumerable<Category> GetAll() => context.Categories;

        public void Create(Category newDataObject)
        {
            context.Add(newDataObject);
            context.SaveChanges();
        }

        public void Update(Category changeDataObject)
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
