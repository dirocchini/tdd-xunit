namespace TDDxUnitCore.Domain.Courses
{
    public class DTOCourse
    {
        public DTOCourse(string name, string description, double workload, string audience, double cost)
        {
            Name = name;
            Description = description;
            Workload = workload;
            Audience = audience;
            Cost = cost;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Workload { get; private set; }
        public string Audience { get; set; }
        public double Cost { get; private set; }
    }
}