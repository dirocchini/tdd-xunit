using System;
using System.Linq;
using TDDxUnitCore.CrossCutting.Extensions;
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
            if (string.IsNullOrEmpty(email.Trim()))
                return null;

            Student entity = _context.Set<Student>().SingleOrDefault(s => s.Email == email);

            return entity;
        }

        public Student GetByCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf.Trim()))
                return null;

            var student = _context.Set<Student>().SingleOrDefault(s => s.Document.ComparableString() == cpf.ComparableString());

            return student;
        }
    }
}
