using System;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Persistence.Contexts;

namespace TDDxUnitCore.Persistence.Repositories
{
    public class StudentRepository: BaseRepository<Student>, IStudentRepository

    {
        public StudentRepository(TddXUnitContext context) : base(context)
        {
        }

        public Student GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
