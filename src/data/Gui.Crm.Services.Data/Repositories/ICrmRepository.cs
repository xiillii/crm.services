using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gui.Crm.Services.Data.Entities;

namespace Gui.Crm.Services.Data.Repositories
{
    public interface ICrmRepository
    {
        IQueryable<Product> Products { get; }
    }
}
