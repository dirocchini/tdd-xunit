using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Domain.Courses
{
    public class DTOCourse 
    {
        public DTOCourse()
        {

        }
        public DTOCourse(string name, string description, double workload, string audience, double cost)
        {
            Name = name;
            Description = description;
            Workload = workload;
            Audience = audience;
            Cost = cost;
        }
        public DTOCourse(int id, string name, string description, double workload, string audience, double cost)
        {
            Id = id;
            Name = name;
            Description = description;
            Workload = workload;
            Audience = audience;
            Cost = cost;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Workload { get; set; }
        public string Audience { get; set; }
        public double Cost { get; set; }
    }
}