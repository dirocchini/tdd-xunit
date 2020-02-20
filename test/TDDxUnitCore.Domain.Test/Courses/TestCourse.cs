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
            string name = "Dev";
            double hours = 5;
            string focus = "Non Dev";
            double cost = 1500.49;

            var course = new Course(name, hours, focus, cost);

            Assert.Equal(name, course.Name);
        }
    }



    class Course
    {
        public Course(string name, double hours, string focus, double cost)
        {
            Name = name;
            Hours = hours;
            Focus = focus;
            Cost = cost;
        }

        public string Name { get; private set; }
        public double Hours { get; private set; }
        public string Focus { get; private set; }
        public double Cost { get; private set; }
    }
}
