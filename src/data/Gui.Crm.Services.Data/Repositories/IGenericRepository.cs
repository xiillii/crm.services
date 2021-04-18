using System.Collections.Generic;

namespace Gui.Crm.Services.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(long id);
        IEnumerable<T> GetAll();
        void Create(T newDataObject);
        void Update(T changedDataObject);
        void Delete(long id);
    }
}
