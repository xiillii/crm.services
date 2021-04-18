using System.Collections.Generic;
using Gui.Crm.Services.Data.Entities;

namespace Gui.Crm.Services.Data.Repositories
{
    public interface ICategoryRepository
    {
        Category Get(long id);
        IEnumerable<Category> GetAll();
        void Create(Category newDataObject);
        void Update(Category changeDataObject);
        void Delete(long id);
    }
}
