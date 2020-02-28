using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Persistence.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TddXUnitContext _context;

        public UnitOfWork(TddXUnitContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
