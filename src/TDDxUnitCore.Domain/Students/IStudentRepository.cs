using System;
using System.Collections.Generic;
using System.Text;
using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Domain.Students
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        Student GetByEmail(string email);
    }

}
