using Bogus;
using ExpectedObjects;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Domain.Test._Tooling;
using TDDxUnitCore.Domain.UnitTest._Builders;
using Xunit;

namespace TDDxUnitCore.Domain.UnitTest.Students
{

    public class StudentTest
    {
        private readonly Faker _faker;
        public StudentTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void MustCreateStudent()
        {
            var expectedStudent = BuilderStudant.New().Build();
            var student = new Student(expectedStudent.Name, expectedStudent.Document, expectedStudent.Email, expectedStudent.Audience);

            expectedStudent.ToExpectedObject().ShouldMatch(student);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MustHaveValidName(string invalidName)
        {
            Assert.Throws<DomainCustomException>(() => BuilderStudant.New().WithName(invalidName).Build())
                .WithMessage(Resources.InvalidName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("654321654")]
        public void MustHaveValidCpf(string invalidDocument)
        {
            Assert.Throws<DomainCustomException>(() => BuilderStudant.New().WithDocument(invalidDocument).Build())
                .WithMessage(Resources.InvalidDocument);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("teste@asdd")]
        [InlineData("testeasdd")]
        [InlineData("teste@")]
        public void MustHaveValidEmail(string invalidEmail)
        {
            Assert.Throws<DomainCustomException>(() => BuilderStudant.New().WithEmail(invalidEmail).Build())
                .WithMessage(Resources.InvalidEmail);
        }

        [Fact]
        public void ChangeName_MustChangeName_StudentWithChangedName()
        {
            var validName = _faker.Person.FullName;
            var student = BuilderStudant.New().Build();

            student.ChangeName(validName);

            Assert.Equal(validName, student.Name);
        }
    }
}
