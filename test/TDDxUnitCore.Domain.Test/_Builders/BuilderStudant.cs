using System;
using Bogus;
using Bogus.Extensions.Brazil;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.Domain.UnitTest._Builders
{
    public class BuilderStudent
    {
        private int _id { get; set; }
        private string _name { get; set; }
        private string _document { get; set; }
        private string _email { get; set; }
        private Audience _audience { get; set; }

        private Faker _faker { get; set; }

        private BuilderStudent()
        {
            _faker = new Faker();
            _id = 0;
            _name = _faker.Person.FullName;
            _email = _faker.Person.Email;
            _document = _faker.Person.Cpf(true);
            _audience = Audience.CTO;
        }


        public static BuilderStudent New()
        {
            return new BuilderStudent();
        }

        public BuilderStudent WithId(int id)
        {
            _id = id;
            return this;
        }


        public BuilderStudent WithName(string name)
        {
            _name = name;
            return this;
        }

        public BuilderStudent WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public BuilderStudent WithDocument(string document)
        {
            _document = document;
            return this;
        }

        public BuilderStudent WithAudience(Audience audience)
        {
            _audience = audience;
            return this;
        }


        public Student Build()
        {
            var student = new Student(_name, _document, _email, _audience);

            if (_id > 0)
            {
                var propertyInfo = student.GetType().GetProperty("Id");
                propertyInfo.SetValue(student, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return student;
        }
    }
}
