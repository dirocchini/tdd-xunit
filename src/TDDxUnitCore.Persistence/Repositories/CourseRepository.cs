using System.Linq;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Persistence.Contexts;

namespace TDDxUnitCore.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly TddXUnitContext _context;

        public CourseRepository(TddXUnitContext context) : base(context)
        {
            _context = context;
        }

        public Course GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
                

            var entity = _context.Set<Course>().Where(c => c.Name.ToLower().Trim().Equals(name.Trim().ToLower()));

            if (entity.Any())
                return entity.First();

            return null;
        }
    }
}
