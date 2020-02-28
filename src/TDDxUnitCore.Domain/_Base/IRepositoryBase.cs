using System.Collections.Generic;

namespace TDDxUnitCore.Domain._Base
{
    public interface IRepositoryBase<TEntity>
    {
        TEntity GetById(int id);
        List<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
    }
}
