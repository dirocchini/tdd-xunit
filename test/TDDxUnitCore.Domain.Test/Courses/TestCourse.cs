using ExpectedObjects;
using System;
using Xunit;
using TDDxUnitCore.Domain.Test._Tooling;
using Xunit.Abstractions;
using TDDxUnitCore.Domain.Test._Builders;

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
            var expectedCourse = new
            {
                Name = "Dev",
                Description = "My Desc",
                Workload = (double)15.57,
                Audience = Audience.Studant,
                Cost = (double)1500
            };

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



    public class Course
    {
        public Course(string name, string description, double workload, Audience audience, double cost)
        {
            Name = name;
            Description = description;
            Workload = workload;
            Audience = audience;
            Cost = cost;

            Validate();
        }

        private void Validate()
        {
            if(string.IsNullOrEmpty(Name))
                throw new ArgumentException("Enter a valid name (not empty or null)");

            if (Workload <= 0)
                throw new ArgumentException("Enter a valid workload (greater than zero)");

            if (Cost <= 0)
                throw new ArgumentException("Enter a valid cost (greater than zero)");
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Workload { get; private set; }
        public Audience Audience { get; private set; }
        public double Cost { get; private set; }
    }

    public enum Audience
    {
        Studant,
        NonDev,
        Employee,
        CTO
    }
}