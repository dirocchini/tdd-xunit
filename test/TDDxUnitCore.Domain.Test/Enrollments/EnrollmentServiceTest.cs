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
        [Fact]
        public void AudienceMustBeEqual()
        {
            var course = BuilderCourse.New().WithAudience(Audience.CTO).Build();
            var courseRepository = new Mock<ICourseRepository>();
            courseRepository.Setup(r => r.GetById(course.Id)).Returns(course);


            var student = BuilderStudent.New().WithAudience(Audience.CTO).Build();
            var studentRepository =new Mock<IStudentRepository>();
            studentRepository.Setup(r => r.GetById(student.Id)).Returns(student);

            var enrollmentDTO = new EnrollmentDTO(courseId: course.Id, studentId: student.Id);

            var enrollmentService = new EnrollmentService(courseRepository.Object, studentRepository.Object);
            

            Assert.Throws<DomainCustomException>(() => enrollmentService.Save(enrollmentDTO)).WithMessage(Resources.AudienceNotEquals);
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
