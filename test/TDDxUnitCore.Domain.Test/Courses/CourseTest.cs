using ExpectedObjects;
using System;
using System.Collections.Immutable;
using Bogus;
using TDDxUnitCore.Domain._Base;
using Xunit;
using TDDxUnitCore.Domain.Test._Tooling;
using Xunit.Abstractions;
using TDDxUnitCore.Domain.Test._Builders;
using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Domain.Test.Courses
{
    public class CourseTest
    {
        private readonly ITestOutputHelper _output;

        private readonly Faker _faker;


        public CourseTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Constructor called");
            _faker = new Faker();
        }

        [Fact(DisplayName = "MustCreateCourse")]
        public void MustCreateCourse()
        {
            var expectedCourse = BuilderCourse.New().Build();

            var course = new Course(
                expectedCourse.Name, expectedCourse.Description, expectedCourse.Workload, expectedCourse.Audience, expectedCourse.Cost);

            expectedCourse.ToExpectedObject().ShouldMatch(course);
        }

        [Theory(DisplayName = "MustHaveValidName")]
        [InlineData("")]
        [InlineData(null)]
        public void MustHaveValidName(string invalidName)
        {
            Assert.Throws<DomainCustomException>(() => BuilderCourse.New().WithName(invalidName).Build())
                .WithMessage("Enter a valid name (not empty or null)");
        }

        [Theory(DisplayName = "MustHavePositiveHours")]
        [InlineData(0)]
        [InlineData(-1.9)]
        [InlineData(-299.50)]
        public void MustHavePositiveHours(double invalidWorkload)
        {
            Assert.Throws<DomainCustomException>(() => BuilderCourse.New().WithWorkload(invalidWorkload).Build())
                .WithMessage("Enter a valid workload (greater than zero)");
        }


        [Theory(DisplayName = "MustHavePositiveCost")]
        [InlineData(0)]
        [InlineData(-1.9)]
        [InlineData(-299.50)]
        public void MustHavePositiveCost(double invalidCost)
        {
            Assert.Throws<DomainCustomException>(() => BuilderCourse.New().WithCost(invalidCost).Build())
                .WithMessage("Enter a valid cost (greater than zero)");
        }

        [Fact(DisplayName = "ChangeName_MustChange_CourseWithChangedName")]
        public void ChangeName_MustChange_ChangedName()
        {
            var newValidNameName = _faker.Person.FullName;
            var course = BuilderCourse.New().Build();

            course.ChangeName(newValidNameName);

            Assert.Equal(newValidNameName, course.Name);
        }

        [Theory(DisplayName = "ChangeName_MustBeValidName_Exception")]
        [InlineData("")]
        [InlineData(null)]
        public void ChangeName_MustBeValidName_Exception(string invalidName)
        {
            var course = BuilderCourse.New().Build();

            Assert.Throws<DomainCustomException>(()=> course.ChangeName(invalidName))
                .WithMessage("Enter a valid name (not empty or null)");
        }


        [Fact(DisplayName = "ChangeCost_MustChange_CourseWithChangedCost")]
        public void ChangeCost_MustChange_CourseWithChangedCost()
        {
            var newValidCost = _faker.Random.Double(10, 4000);
            var course = BuilderCourse.New().Build();
            
            course.ChangeCost(newValidCost);
            
            Assert.Equal(newValidCost, course.Cost);
        }

        [Theory(DisplayName = "ChangeCost_MustBeValidCost_Exception")]
        [InlineData(0)]
        [InlineData(-123)]
        public void ChangeCost_MustBeValidCost_Exception(double invalidCost)
        {
            var course = BuilderCourse.New().Build();
            Assert.Throws<DomainCustomException>(() => course.ChangeCost(invalidCost))
                .WithMessage("Enter a valid cost (greater than zero)");
        }

        ////OLD
        //[Theory(DisplayName = "MustHavePositiveCost")]
        //[InlineData(0)]
        //[InlineData(-1.9)]
        //[InlineData(-299.50)]
        //public void MustHavePositiveCost(double invalidCost)
        //{
        //    Assert.Throws<ArgumentException>(() => BuilderCourse.New().WithCost(invalidCost).Build())
        //        .WithMessage("Enter a valid cost (greater than zero)");
        //}
    }
}