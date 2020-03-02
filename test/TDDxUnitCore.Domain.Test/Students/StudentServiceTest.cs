using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Domain.Test._Tooling;
using TDDxUnitCore.Domain.UnitTest._Builders;
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
                , "CTO"
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

        [Fact]
        public void MustAddStudentEqualToDto()
        {
            _studentService.Save(_studentDTO);
            _studentRepositoryMock.Verify(r => r.Add(
                It.Is<Student>(s =>
                    s.Name.Equals(_studentDTO.Name, StringComparison.CurrentCultureIgnoreCase) &&
                    s.Email.Equals(_studentDTO.Email, StringComparison.CurrentCultureIgnoreCase)
                )
            ));
        }
        
        [Fact]
        public void MustAddInvalidAudience()
        {
            string invalidAudience = "medics";
            _studentDTO.Audience = invalidAudience;

            Assert.Throws<DomainCustomException>(() => _studentService.Save(_studentDTO)).WithMessage(Resources.InvalidAudience);
        }

        [Fact]
        public void Save_MustHaveUniqueEmail_Exception()
        {
            var studentAlreadySaved = BuilderStudant.New().WithEmail(_studentDTO.Email).Build();
            _studentDTO.Id = 1;
            _studentRepositoryMock.Setup(r => r.GetByEmail(_studentDTO.Email)).Returns(studentAlreadySaved);

            Assert.Throws<DomainCustomException>(() => _studentService.Save(_studentDTO)).WithMessage(Resources.EmailAlreadyTaken);
        }


        [Fact]
        public void Save_UpdateMustNotCallAdd_CallNever()
        {
            _studentDTO.Id = 1;
            var student = BuilderStudant.New().Build();
            _studentRepositoryMock.Setup(r => r.GetById(_studentDTO.Id)).Returns(student);

            _studentService.Save(_studentDTO);
            

            _studentRepositoryMock.Verify(r => r.Add(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public void Save_UpdateMustCall_GetById()
        {
            _studentDTO.Id = 1;
            var studentAlreadySaved = BuilderStudant.New().Build();
            _studentRepositoryMock.Setup(r => r.GetById(_studentDTO.Id)).Returns(studentAlreadySaved);
            
            _studentService.Save(_studentDTO);
            _studentRepositoryMock.Verify(r=>r.GetById(_studentDTO.Id), Times.Once);

        }

        [Fact]
        public void Save_MustUpdateCourse_UpdatedCourse()
        {
            _studentDTO.Id = 1;
            var student = BuilderStudant.New().Build();
            _studentRepositoryMock.Setup(r => r.GetById(_studentDTO.Id)).Returns(student);

            _studentService.Save(_studentDTO);

            Assert.Equal(_studentDTO.Name, student.Name);
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
                var studentAlreadySaved = _studentRepository.GetByEmail(studentDto.Email);

                RulerValidator.New()
                    .When(studentAlreadySaved != null && studentDto.Id != studentAlreadySaved.Id, Resources.EmailAlreadyTaken)
                    .When(!Enum.TryParse<Audience>(studentDto.Audience, out var audience), Resources.InvalidAudience)
                    .ThrowException();

                var student = new Student(studentDto.Name, studentDto.Document, studentDto.Email, audience);

                if (studentDto.Id > 0)
                {
                    var studentFromDb = _studentRepository.GetById(studentDto.Id);
                    studentFromDb.ChangeName(studentDto.Name);
                }

                if (studentDto.Id == 0)
                    _studentRepository.Add(student);
            }

        }
    }
}
