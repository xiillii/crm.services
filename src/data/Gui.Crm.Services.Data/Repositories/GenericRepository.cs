using System.Collections.Generic;
using Gui.Crm.Services.Data.Models;

namespace Gui.Crm.Services.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected CrmDbContext context;

        public GenericRepository(CrmDbContext ctx) => context = ctx;

        public T Get(long id) => context.Set<T>().Find(id);

        public IEnumerable<T> GetAll() => context.Set<T>();

        public void Create(T newDataObject)
        {
            context.Add<T>(newDataObject);
            context.SaveChanges();
        }

        public void Update(T changedDataObject)
        {
            context.Update<T>(changedDataObject);
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            context.Remove<T>(Get(id));
            context.SaveChanges();
        }
    }
}
