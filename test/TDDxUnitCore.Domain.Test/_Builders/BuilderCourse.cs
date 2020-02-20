using System;
using System.Collections.Generic;
using System.Text;
using TDDxUnitCore.Domain.Test.Courses;

namespace TDDxUnitCore.Domain.Test._Builders
{
    public class BuilderCourse
    {
        private string _name = "Dev";
        private string _description = "Dev for everyone";
        private double _workload = 213.5;
        private Audience _audience = Audience.NonDev;
        private double _cost = 332;

        public static BuilderCourse New()
        {
            return new BuilderCourse();
        }


        public BuilderCourse WithName (string name)
        {
            _name = name;
            return this;
        }
        public BuilderCourse WithDescription(string description)
        {
            _description = description;
            return this;
        }
        
        public BuilderCourse WithWorkload(double workload)
        {
            _workload = workload;
            return this;
        }

        public BuilderCourse WithCost(double cost)
        {
            _cost = cost;
            return this;
        }

        public BuilderCourse WithAudience(Audience audience)
        {
            _audience = audience;
            return this;
        }

        public Course Build()
        {
            return new Course(_name, _description, _workload, _audience, _cost);
        }
    }
}
