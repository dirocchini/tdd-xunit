﻿using Bogus;
using ExpectedObjects;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Test._Builders;
using TDDxUnitCore.Domain.Test._Tooling;
using Xunit;

namespace TDDxUnitCore.Domain.UnitTest.Courses
{
    public class CourseTest
    {
        private readonly Faker _faker;

        public CourseTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void MustCreateCourse()
        {
            var expectedCourse = BuilderCourse.New().Build();

            var course = new Course(
                expectedCourse.Name, expectedCourse.Description, expectedCourse.Workload, expectedCourse.Audience, expectedCourse.Cost);

            expectedCourse.ToExpectedObject().ShouldMatch(course);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MustHaveValidName(string invalidName)
        {
            Assert.Throws<DomainCustomException>(() => BuilderCourse.New().WithName(invalidName).Build())
                .WithMessage(Resources.InvalidName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.9)]
        [InlineData(-299.50)]
        public void MustHavePositiveWorkload(double invalidWorkload)
        {
            Assert.Throws<DomainCustomException>(() => BuilderCourse.New().WithWorkload(invalidWorkload).Build())
                .WithMessage(Resources.InvalidWorkload);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1.9)]
        [InlineData(-299.50)]
        public void MustHavePositiveCost(double invalidCost)
        {
            Assert.Throws<DomainCustomException>(() => BuilderCourse.New().WithCost(invalidCost).Build())
                .WithMessage(Resources.InvalidCost);
        }

        [Fact]
        public void ChangeName_MustChange_ChangedName()
        {
            var newValidNameName = _faker.Person.FullName;
            var course = BuilderCourse.New().Build();

            course.ChangeName(newValidNameName);

            Assert.Equal(newValidNameName, course.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ChangeName_MustBeValidName_Exception(string invalidName)
        {
            var course = BuilderCourse.New().Build();

            Assert.Throws<DomainCustomException>(()=> course.ChangeName(invalidName))
                .WithMessage(Resources.InvalidName);
        }


        [Fact]
        public void ChangeCost_MustChange_CourseWithChangedCost()
        {
            var newValidCost = _faker.Random.Double(10, 4000);
            var course = BuilderCourse.New().Build();
            
            course.ChangeCost(newValidCost);
            
            Assert.Equal(newValidCost, course.Cost);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123)]
        public void ChangeCost_MustBeValidCost_Exception(double invalidCost)
        {
            var course = BuilderCourse.New().Build();
            Assert.Throws<DomainCustomException>(() => course.ChangeCost(invalidCost))
                .WithMessage(Resources.InvalidCost);
        }

        [Fact]
        public void ChangeWorkload_MustChange_CourseWithChangedWorkload()
        {
            var newValidWorkload = _faker.Random.Double(100, 2341);
            var course = BuilderCourse.New().Build();

            course.ChangeWorkload(newValidWorkload);

            Assert.Equal(newValidWorkload, course.Workload);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1233)]
        public void ChangeWorkload_MustBeValidWorkload_Exception(double invalidWorkload)
        {
            var course = BuilderCourse.New().Build();

            Assert.Throws<DomainCustomException>(()=> course.ChangeWorkload(invalidWorkload))
                .WithMessage(Resources.InvalidWorkload);
        }
    }
}