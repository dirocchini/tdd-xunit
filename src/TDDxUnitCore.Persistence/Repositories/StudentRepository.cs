using System;
using System.Collections.Generic;
using System.Text;
using TDDxUnitCore.Domain.Courses;
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
