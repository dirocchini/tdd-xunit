﻿using Bogus;
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
                    c.Name.Equals(_dtoCourse.Name, StringComparison.InvariantCultureIgnoreCase) &&
                    c.Description.Equals(_dtoCourse.Description, StringComparison.InvariantCultureIgnoreCase)
                )
            ));
        }

        [Fact(DisplayName = "ShouldAddValidAudience")]
        public void ShouldAddValidAudience()
        {
            var invalidAudience = "medics";
            _dtoCourse.Audience = invalidAudience;

            Assert.Throws<DomainCustomException>(() =>
                _courseService.Save(_dtoCourse)).WithMessage("Must Enter a Valid Audience");
        }

        [Fact(DisplayName = "MustAddAUniqueCourseName")]
        public void MustAddAUniqueCourseName()
        {
            var courseAlreadySaved = BuilderCourse.New().WithName(_dtoCourse.Name).Build();
            _courseRepositoryMock.Setup(r => r.GetByName(_dtoCourse.Name)).Returns(courseAlreadySaved);

            Assert.Throws<DomainCustomException>(() =>
                _courseService.Save(_dtoCourse)).WithMessage("Course Name Already Used by Another Course");
        }
    }
}
