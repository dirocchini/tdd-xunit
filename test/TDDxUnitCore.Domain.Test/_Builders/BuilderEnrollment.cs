using System.Collections.Immutable;
using System.Net.Http.Headers;
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
        private bool Cancelled { get; set; }
        private bool Finished { get; set; }


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

        public BuilderEnrollment WithCancelled(bool cancelled)
        {
            Cancelled = cancelled;
            return this;
        }


        public BuilderEnrollment WithFinished(bool finished)
        {
            Finished = finished;
            return this;
        }


        public Enrollment Build()
        {
            var enrollment = new Enrollment(_student, _course, _paidValue);

            if (Cancelled)
                enrollment.Cancel();

            if (Finished)
                enrollment.SetStudentGrade(8);

            return enrollment;
        }
    }
}
