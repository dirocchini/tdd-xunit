using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Enrollments;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Domain.Test._Builders;
using TDDxUnitCore.Domain.Test._Tooling;
using TDDxUnitCore.Domain.UnitTest._Builders;
using Xunit;

namespace TDDxUnitCore.Domain.UnitTest.Enrollments
{
    public class EnrollmentServiceTest
    {
        private readonly Mock<ICourseRepository> _courseRepository;
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<IEnrollmentRepository> _enrollmentRepository;

        private readonly EnrollmentService _enrollmentService;
        private readonly Student _student;
        private readonly Course _course;
        private readonly EnrollmentDTO _enrollmentDTO;


        public EnrollmentServiceTest()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _studentRepository = new Mock<IStudentRepository>();
            _enrollmentRepository = new Mock<IEnrollmentRepository>();

            _enrollmentService = new EnrollmentService(_courseRepository.Object, _studentRepository.Object, _enrollmentRepository.Object);

            _course = BuilderCourse.New().WithId(1).WithAudience(Audience.CTO).Build();
            _student = BuilderStudent.New().WithId(1).WithAudience(Audience.CTO).Build();

            _courseRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(_course);
            _studentRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(_student);

            _enrollmentDTO = new EnrollmentDTO(_course.Id, _student.Id, _course.Cost);
        }



        [Fact]
        public void Save_CourseNotFound_Exception()
        {
            Course invalidCourse = null;
            _courseRepository.Setup(r => r.GetById(_enrollmentDTO.CourseId)).Returns(invalidCourse);

            Assert.Throws<DomainCustomException>(() => _enrollmentService.Save(_enrollmentDTO)).WithMessage(Resources.CourseNotFound);
        }

        [Fact]
        public void Save_StudentNotFound_Exception()
        {
            Student invalidStudent = null;
            _studentRepository.Setup(r => r.GetById(_enrollmentDTO.StudentId)).Returns(invalidStudent);

            Assert.Throws<DomainCustomException>(() => _enrollmentService.Save(_enrollmentDTO)).WithMessage(Resources.StudentNotFound);
        }


        [Fact]
        public void Save_Enrollment_Void()
        {
            _enrollmentService.Save(_enrollmentDTO);

            _enrollmentRepository.Verify(r => r.Add(It.Is<Enrollment>(m=> m.Course == _course && m.Student == _student)));
        }


        //TODO - SHOW THIS EXAMPLE
        [Fact]
        public void UpdateGrade_MustChangeStudentGrade_Void()
        {
            var expectedStudentGrade = 8M;
            var enrollment = BuilderEnrollment.New().Build();
            _enrollmentRepository.Setup(r => r.GetById(enrollment.Id)).Returns(enrollment);

            _enrollmentService.FinishEnrollment(enrollment.Id, expectedStudentGrade);
            Assert.Equal(expectedStudentGrade, enrollment.StudentGrade);
        }


        //TODO  -  VERIFICAR SE ENCONTROU A MATRICULA
    }
}
