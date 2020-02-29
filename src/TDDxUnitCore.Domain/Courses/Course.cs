﻿using System;
using System.Collections.Generic;
using System.Text;
using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Domain.Courses
{
    public class Course : Entity
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
            RulerValidator.New()
                .When(string.IsNullOrEmpty(Name), "Enter a valid name (not empty or null)")
                .When(Workload <= 0, "Enter a valid workload (greater than zero)")
                .When(Cost <= 0, "Enter a valid cost (greater than zero)")
                .ThrowException();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Workload { get; private set; }
        public Audience Audience { get; private set; }
        public double Cost { get; private set; }
    }
}
