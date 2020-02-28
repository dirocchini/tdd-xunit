using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Persistence.Contexts;

namespace TDDxUnitCore.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
    {
        private readonly TddXUnitContext _context;

        public BaseRepository(TddXUnitContext context)
        {
            _context = context;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(entity => entity.Id == id);
        }

        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);

        public TEntity GetById(int id)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(entity => entity.Id == id);
            return query.Any() ? query.First() : null;
        }

        public List<TEntity> Get()
        {
            var entities = _context.Set<TEntity>().ToList();
            return entities.Any() ? entities : new List<TEntity>();
        }
    }
}
