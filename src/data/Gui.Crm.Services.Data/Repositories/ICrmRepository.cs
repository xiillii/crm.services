using Gui.Crm.Services.Data.Entities;
using System.Linq;

namespace Gui.Crm.Services.Data.Repositories
{
    public interface ICrmRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
    }
}
