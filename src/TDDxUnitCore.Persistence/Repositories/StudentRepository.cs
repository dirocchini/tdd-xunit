using System;
using System.Linq;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Persistence.Contexts;

namespace TDDxUnitCore.Persistence.Repositories
{
    public class StudentRepository: BaseRepository<Student>, IStudentRepository

    {
        private readonly TddXUnitContext _context;

        public StudentRepository(TddXUnitContext context) : base(context)
        {
            _context = context;
        }

        public Student GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            Student entity = _context.Set<Student>().SingleOrDefault(s => s.Email.ToLower().Trim() == email.ToLower().Trim());

            return entity;
        }
    }
}
