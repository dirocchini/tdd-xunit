using System.Collections.Immutable;
using Bogus;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Enrollments;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Domain.UnitTest._Builders;
using TDDxUnitCore.Domain.UnitTest.Enrollments;

namespace TDDxUnitCore.Domain.Test._Builders
{
    public class BuilderEnrollment
    {
        private Student _student { get; set; }
        private Course _course { get; set; }
        private double _paidValue { get; set; }

        private Faker _faker;


        public static BuilderEnrollment New()
        {
            return new BuilderEnrollment();
        }

        private BuilderEnrollment()
        {
            _faker = new Faker();

            _student = BuilderStudent.New().Build();
            _course = BuilderCourse.New().Build();
            _paidValue = _faker.Random.Double(100, 150);
        }


        public BuilderEnrollment WithStudent(Student student)
        {
            _student = student;
            return this;
        }

        public BuilderEnrollment WithCourse(Course course)
        {
            _course = course;
            return this;
        }

        public BuilderEnrollment WithPaidValue(double paidValue)
        {
            _paidValue = paidValue;
            return this;
        }

        public Enrollment Build()
        {
            return new Enrollment(_student, _course, _paidValue);
        }
    }
}
