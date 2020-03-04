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
        private readonly EnrollmentService _enrollmentService;
        private readonly Student _student;
        private readonly Course _course;
        private readonly EnrollmentDTO _enrollmentDTO;


        public EnrollmentServiceTest()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _studentRepository = new Mock<IStudentRepository>();
            _enrollmentService = new EnrollmentService(_courseRepository.Object, _studentRepository.Object);

            _course = BuilderCourse.New().WithId(1).WithAudience(Audience.CTO).Build();
            _student = BuilderStudent.New().WithId(1).WithAudience(Audience.CTO).Build();

            _courseRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(_course);
            _studentRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(_student);


            _enrollmentDTO = new EnrollmentDTO(_course.Id, _student.Id);
        }



        [Fact]
        public void CourseMustNotBeEmpty()
        {
            Course invalidCourse = null;
            _courseRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(invalidCourse);


            Assert.Throws<DomainCustomException>(() => _enrollmentService.Save(_enrollmentDTO)).WithMessage(Resources.InvalidCourse);
        }

    }

    public class EnrollmentService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public EnrollmentService(ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }


        public void Save(EnrollmentDTO enrollmentDto)
        {
            var course = _courseRepository.GetById(enrollmentDto.CourseId);
            var student = _studentRepository.GetById(enrollmentDto.StudentId);


        }
    }

    public interface IEnrollmentRepository : IRepositoryBase<Enrollment>
    {
        
    }

    public class EnrollmentDTO
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        

        public EnrollmentDTO(int courseId, int studentId)
        {
            CourseId = courseId;
            StudentId = studentId;
        }
    }
}
