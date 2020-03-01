using Bogus;
using Bogus.Extensions.Brazil;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.Domain.UnitTest._Builders
{
    public class BuilderStudant
    {
        private string _name { get; set; }
        private string _document { get; set; }
        private string _email { get; set; }
        private Audience _audience { get; set; }

        private Faker _faker { get; set; }

        public BuilderStudant()
        {
            _faker = new Faker();
            _name = _faker.Person.FullName;
            _email = _faker.Person.Email;
            _document = _faker.Person.Cpf(true);
            _audience = Audience.CTO;
        }


        public static BuilderStudant New()
        {
            return new BuilderStudant();
        }


        public BuilderStudant WithName(string name)
        {
            _name = name;
            return this;
        }

        public BuilderStudant WithDocument(string document)
        {
            _document = document;
            return this;
        }

        public BuilderStudant WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public Student Build()
        {
            return new Student(_name, _document, _email, _audience);
        }
    }
}
