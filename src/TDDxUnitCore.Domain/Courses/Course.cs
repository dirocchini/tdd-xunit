using System;
using System.Collections.Generic;
using System.Text;

namespace TDDxUnitCore.Domain.Courses
{
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
            if (string.IsNullOrEmpty(Name))
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
}
