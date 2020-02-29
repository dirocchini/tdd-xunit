using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Domain.Courses
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        Course GetByName(string name);
    }
}
