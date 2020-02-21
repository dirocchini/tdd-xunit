using Bogus;
using Moq;
using System;
using TDDxUnitCore.Domain.Courses;
using Xunit;

namespace TDDxUnitCore.Domain.Test.Courses
{
    public class CourseServiceTest
    {
        private readonly DTOCourse _dtoCourse;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly CourseService _courseService;

        public CourseServiceTest()
        {
            var faker = new Faker();
            _dtoCourse = new DTOCourse(
                faker.Random.Words()
                , faker.Lorem.Paragraph()
                , faker.Random.Double(5, 234)
                , 1
                , faker.Random.Double(1000, 1231)
            );

            _courseRepositoryMock = new Mock<ICourseRepository>();
            _courseService = new CourseService(_courseRepositoryMock.Object);

        }

        [Fact(DisplayName = "MustCallAddCourse")]
        public void MustCallAddCourse()
        {
            _courseService.Save(_dtoCourse);
            _courseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()));
        }

        [Fact(DisplayName = "MustAddCourseEqualsToDto")]
        public void MustAddCourseEqualsToDto()
        {
            _courseService.Save(_dtoCourse);
            _courseRepositoryMock.Verify(r => r.Add(
                It.Is<Course>(c =>
                    c.Name.Equals(_dtoCourse.Name) &&
                    c.Description.Equals(_dtoCourse.Description)
                )
            ));
        }
    }

    public interface ICourseRepository
    {
        void Add(Course course);
        void Update(Course course);
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
