using System.Collections.Generic;

namespace TDDxUnitCore.Domain._Base
{
    public interface IRepositoryBase<TEntity>
    {
        TEntity GetById(int id);
        List<TEntity> Get();
        void Add(TEntity entity);
    }
}
