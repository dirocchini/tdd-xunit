using Bogus;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Domain.Test._Builders
{
    public class BuilderCourse
    {
        private string _name { get; set; }
        private string _description { get; set; }
        private double _workload { get; set; }
        private Audience _audience { get; set; }
        private double _cost { get; set; }

        private Faker _faker;

        private BuilderCourse()
        {
            _faker = new Faker();
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
