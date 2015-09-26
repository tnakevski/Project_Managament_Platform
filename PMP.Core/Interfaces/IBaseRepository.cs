using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Core.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Create(TEntity entity);
        void Delete(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(int id);
        void Update(TEntity entity);
        void Save();
    }
}
