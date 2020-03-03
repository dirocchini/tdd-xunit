using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;

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
                .When(string.IsNullOrEmpty(Name), Resources.InvalidName)
                .When(Workload <= 0, Resources.InvalidWorkload)
                .When(Cost <= 0, Resources.InvalidCost)
                .ThrowException();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Workload { get; private set; }
        public Audience Audience { get; private set; }
        public double Cost { get; private set; }

        public void ChangeName(string newName)
        {
            RulerValidator.New()
                .When(string.IsNullOrEmpty(newName), Resources.InvalidName)
                .ThrowException();
            
            Name = newName;
        }

        public void ChangeCost(double newCost)
        {
            RulerValidator.New()
                .When(newCost <= 0, Resources.InvalidCost)
                .ThrowException();

            Cost = newCost;
        }

        public void ChangeWorkload(double newWorkload)
        {
            RulerValidator.New()
                .When(newWorkload <= 0, Resources.InvalidWorkload)
                .ThrowException();

            Workload = newWorkload;
        }
    }
}


