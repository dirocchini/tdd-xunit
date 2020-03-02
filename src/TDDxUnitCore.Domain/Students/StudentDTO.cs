using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Domain.Students
{
    public class StudentDTO
    {
        public StudentDTO(string name, string document, string email, Audience audience)
        {
            Name = name;
            Document = document;
            Email = email;
            Audience = audience;
        }

        public StudentDTO(int id, string name, string document, string email, Audience audience)
        {
            Id = id;
            Name = name;
            Document = document;
            Email = email;
            Audience = audience;
        }

        public int Id { get; set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public Audience Audience { get; private set; }
    }
}