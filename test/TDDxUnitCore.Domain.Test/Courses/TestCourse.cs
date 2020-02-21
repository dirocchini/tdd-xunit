using ExpectedObjects;
using System;
using Xunit;
using TDDxUnitCore.Domain.Test._Tooling;
using Xunit.Abstractions;
using TDDxUnitCore.Domain.Test._Builders;
using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Domain.Test.Courses
{
    public class TestCourse
    {
        private readonly ITestOutputHelper _output;

        public TestCourse(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Constructor called");
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
            Assert.Throws<ArgumentException>(() => BuilderCourse.New().WithName(invalidName).Build())
                .WithMessage("Enter a valid name (not empty or null)");
        }

        [Theory(DisplayName = "MustHavePositiveHours")]
        [InlineData(0)]
        [InlineData(-1.9)]
        [InlineData(-299.50)]
        public void MustHavePositiveHours(double invalidWorkload)
        {
            Assert.Throws<ArgumentException>(() => BuilderCourse.New().WithWorkload(invalidWorkload).Build())
                .WithMessage("Enter a valid workload (greater than zero)");
        }


        [Theory(DisplayName = "MustHavePositiveCost")]
        [InlineData(0)]
        [InlineData(-1.9)]
        [InlineData(-299.50)]
        public void MustHavePositiveCost(double invalidCost)
        {
            Assert.Throws<ArgumentException>(() => BuilderCourse.New().WithCost(invalidCost).Build())
                .WithMessage("Enter a valid cost (greater than zero)");
        }
    }
}