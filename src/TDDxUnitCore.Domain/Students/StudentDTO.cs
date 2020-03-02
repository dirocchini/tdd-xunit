using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Domain.Students
{
    public class StudentDTO
    {
        public StudentDTO(string name, string document, string email, string audience)
        {
            Name = name;
            Document = document;
            Email = email;
            Audience = audience;
        }

        public StudentDTO(int id, string name, string document, string email, string audience)
        {
            Id = id;
            Name = name;
            Document = document;
            Email = email;
            Audience = audience;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Audience { get; set; }
    }
}