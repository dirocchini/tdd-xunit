using Bogus;
using Moq;
using System;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using Xunit;
using TDDxUnitCore.Domain.Test._Tooling;
using TDDxUnitCore.Domain.Test._Builders;

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
                , "Studant"
                , faker.Random.Double(1000, 1231)
            );

            _courseRepositoryMock = new Mock<ICourseRepository>();
            _courseService = new CourseService(_courseRepositoryMock.Object);

        }

        [Fact]
        public void MustCallAddCourse()
        {
            _courseService.Save(_dtoCourse);
            _courseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()));
        }

        [Fact]
        public void MustAddCourseEqualsToDto()
        {
            _courseService.Save(_dtoCourse);
            _courseRepositoryMock.Verify(r => r.Add(
                It.Is<Course>(c =>
                    c.Name.Equals(_dtoCourse.Name, StringComparison.InvariantCultureIgnoreCase) &&
                    c.Description.Equals(_dtoCourse.Description, StringComparison.InvariantCultureIgnoreCase)
                )
            ));
        }

        [Fact]
        public void ShouldAddValidAudience()
        {
            var invalidAudience = "medics";
            _dtoCourse.Audience = invalidAudience;

            Assert.Throws<DomainCustomException>(() =>
                _courseService.Save(_dtoCourse)).WithMessage(Resources.InvalidAudience);
        }

        [Fact]
        public void MustAddAUniqueCourseName()
        {
            var courseAlreadySaved = BuilderCourse.New().WithName(_dtoCourse.Name).Build();
            _courseRepositoryMock.Setup(r => r.GetByName(_dtoCourse.Name)).Returns(courseAlreadySaved);

            Assert.Throws<DomainCustomException>(() =>
                _courseService.Save(_dtoCourse)).WithMessage(Resources.NameAlreadyUsed);
        }

        [Fact]
        public void UpdateCourse_ChangeCourseProps_UpdatedCourse()
        {
            _dtoCourse.Id = 555;
            var course = BuilderCourse.New().Build();
            _courseRepositoryMock.Setup(r => r.GetById(555)).Returns(course);

            _courseService.Save(_dtoCourse);

            Assert.Equal(_dtoCourse.Name, course.Name);
            Assert.Equal(_dtoCourse.Cost, course.Cost);
            Assert.Equal(_dtoCourse.Workload, course.Workload);
        }

        [Fact]
        public void UpdateCourse_MustNotCallAdd_Exception()
        {
            _dtoCourse.Id = 555;
            var course = BuilderCourse.New().Build();
            _courseRepositoryMock.Setup(r => r.GetById(555)).Returns(course);

            _courseService.Save(_dtoCourse);

            _courseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never);
        }
    }
}
