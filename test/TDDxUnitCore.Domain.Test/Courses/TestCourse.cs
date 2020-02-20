using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TDDxUnitCore.Domain.Test.Courses
{
    public class TestCourse
    {
        [Fact(DisplayName = "MustCreateCourse")]
        public void MustCreateCourse()
        {
            var expectedCourse = new
            {
                Name = "Dev",
                Hours = (double)15.57,
                Audience = Audience.Studant,
                Cost = (double)1500
            };


            var course = new Course(expectedCourse.Name, expectedCourse.Hours, expectedCourse.Audience, expectedCourse.Cost);


            expectedCourse.ToExpectedObject().ShouldMatch(course);
        }
    }



    class Course
    {
        public Course(string name, double hours, Audience audience, double cost)
        {
            Name = name;
            Hours = hours;
            Audience = audience;
            Cost = cost;
        }

        public string Name { get; private set; }
        public double Hours { get; private set; }
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
