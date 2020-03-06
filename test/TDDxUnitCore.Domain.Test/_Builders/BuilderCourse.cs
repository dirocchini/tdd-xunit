using System;
using Bogus;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Domain.Test._Builders
{
    public class BuilderCourse
    {
        private int _id { get; set; }
        private string _name { get; set; }
        private string _description { get; set; }
        private double _workload { get; set; }
        private Audience _audience { get; set; }
        private double _cost { get; set; }

        private Faker _faker;

        private BuilderCourse()
        {
            _faker = new Faker();
            _id = 0;
            _name = _faker.Person.FullName;
            _description = string.Join(' ', _faker.Lorem.Words(7));
            _workload = _faker.Random.Double(50, 5684);
            _cost = _faker.Finance.Random.Double(1235, 55654);

            _audience = Audience.NonDev;
        }

        public static BuilderCourse New()
        {
            return new BuilderCourse();
        }

        public BuilderCourse WithId(int id)
        {
            _id = id;
            return this;
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
            var course = new Course(_name, _description, _workload, _audience, _cost);

            if (_id > 0)
            {
                var propertyInfo = course.GetType().GetProperty("Id");
                propertyInfo.SetValue(course, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return course;
        }
    }
}
