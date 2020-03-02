using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;
using Xunit;

namespace TDDxUnitCore.Domain.UnitTest.Students
{
    public class StudentServiceTest
    {
        private readonly StudentDTO _studentDTO;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly StudentService _studentService;

        public StudentServiceTest()
        {
            var faker = new Faker();

            _studentDTO = new StudentDTO(
                  faker.Person.FullName
                , faker.Person.Cpf(true)
                , faker.Person.Email
                , Audience.CTO
            );

            _studentRepositoryMock = new Mock<IStudentRepository>();
            _studentService = new StudentService(_studentRepositoryMock.Object);
        }

        [Fact]
        public void MustCallAddStudent()
        {
            _studentService.Save(_studentDTO);
            _studentRepositoryMock.Verify(r => r.Add(It.IsAny<Student>()));
        }
    }





    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public void Save(StudentDTO studentDto)
        {
            var studentFromDb = _studentRepository.GetByEmail(studentDto.Email);

            RulerValidator.New()
                .When(studentFromDb != null, Resources.EmailAlreadyTaken)
                .ThrowException();

            var student = new Student(studentDto.Name, studentDto.Document, studentDto.Email, studentDto.Audience);

            _studentRepository.Add(student);
        }

    }
}
