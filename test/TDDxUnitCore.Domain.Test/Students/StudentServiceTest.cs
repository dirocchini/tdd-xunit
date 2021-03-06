﻿using System;
using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Domain.Test._Tooling;
using TDDxUnitCore.Domain.UnitTest._Builders;
using Xunit;

namespace TDDxUnitCore.Domain.UnitTest.Students
{
    public partial class StudentServiceTest
    {
        private readonly StudentDTO _studentDTO;
        private readonly StudentService _studentService;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly Mock<IAudienceConverter> _audienceConverterMock;
        private readonly Mock<IMapper> _mapperMock;


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
            _audienceConverterMock = new Mock<IAudienceConverter>();
            _mapperMock = new Mock<IMapper>();
            _studentService = new StudentService(_studentRepositoryMock.Object, _audienceConverterMock.Object, _mapperMock.Object);
            
            _mapperMock.Setup(r => r.Map<Student>(_studentDTO)).Returns(new Student(_studentDTO.Name, _studentDTO.Document, _studentDTO.Email, Audience.CTO));
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
        public void Save_MustHaveUniqueEmail_Exception()
        {
            var studentAlreadySaved = BuilderStudent.New().WithEmail(_studentDTO.Email).Build();
            _studentDTO.Id = 1;
            _studentRepositoryMock.Setup(r => r.GetByEmail(_studentDTO.Email)).Returns(studentAlreadySaved);

            Assert.Throws<DomainCustomException>(() => _studentService.Save(_studentDTO)).WithMessage(Resources.EmailAlreadyTaken);
        }


        [Fact]
        public void Save_UpdateMustNotCallAdd_CallNever()
        {
            _studentDTO.Id = 1;
            var student = BuilderStudent.New().Build();
            _studentRepositoryMock.Setup(r => r.GetById(_studentDTO.Id)).Returns(student);

            _studentService.Save(_studentDTO);
            

            _studentRepositoryMock.Verify(r => r.Add(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public void Save_UpdateMustCall_GetById()
        {
            _studentDTO.Id = 1;
            var studentAlreadySaved = BuilderStudent.New().Build();
            _studentRepositoryMock.Setup(r => r.GetById(_studentDTO.Id)).Returns(studentAlreadySaved);
            
            _studentService.Save(_studentDTO);
            _studentRepositoryMock.Verify(r=>r.GetById(_studentDTO.Id), Times.Once);

        }

        [Fact]
        public void Save_MustUpdateCourse_UpdatedCourse()
        {
            _studentDTO.Id = 1;
            var student = BuilderStudent.New().Build();
            _studentRepositoryMock.Setup(r => r.GetById(_studentDTO.Id)).Returns(student);

            _studentService.Save(_studentDTO);

            Assert.Equal(_studentDTO.Name, student.Name);
        }
    }
}
