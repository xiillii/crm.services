using System.Collections.Generic;
using Gui.Crm.Services.Data.Entities;
using System.Linq;

namespace Gui.Crm.Services.Data.Repositories
{
    public interface IProductRepository
    {
        Product Get(long id);
        IEnumerable<Product> GetAll();
        void Create(Product newDataObject);
        void Update(Product changeDataObject);
        void Delete(long id);

    }
}
