using Moq;
using System;
using TDDxUnitCore.Domain.Courses;
using Xunit;

namespace TDDxUnitCore.Domain.Test.Courses
{
    public class CourseServiceTest
    {
        [Fact(DisplayName = "ShouldAddCourse")]
        public void ShouldAddCourse()
        {
            var dtoCourse = new DTOCourse("A Course", "A Description", 123.2, 1, 213);

            var courseRepositoryMock = new Mock<ICourseRepository>();
            var courseService = new CourseService(courseRepositoryMock.Object);
            courseService.Save(dtoCourse);


            courseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()));
        }
    }

    public interface ICourseRepository
    {
        void Add(Course course);
    }

    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Save(DTOCourse dtoCourse)
        {
            var course = new Course(dtoCourse.Name, dtoCourse.Description, dtoCourse.Workload, Audience.CTO, dtoCourse.Cost);

            _courseRepository.Add(course);
        }
    }


    public class DTOCourse
    {
        public DTOCourse(string name, string description, double workload, int audienceId, double cost)
        {
            Name = name;
            Description = description;
            Workload = workload;
            AudienceId = audienceId;
            Cost = cost;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Workload { get; private set; }
        public int AudienceId { get; private set; }
        public double Cost { get; private set; }
    }
}
