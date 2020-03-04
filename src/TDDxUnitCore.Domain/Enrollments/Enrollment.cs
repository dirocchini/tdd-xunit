using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.Domain.Enrollments
{
    public class Enrollment
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public double PaidValue { get; private set; }
        public bool HasDiscount { get; private set; }

        public Enrollment(Student student, Course course, double paidValue)
        {
            RulerValidator.New()
                .When(student == null, Resources.InvalidStudent)
                .When(course == null, Resources.InvalidCourse)
                .When(paidValue <= 0, Resources.InvalidPaidValue)
                .When(course != null && paidValue > course.Cost, Resources.InvalidPaidOriginalCost)
                .When(course != null && student != null && course.Audience != student.Audience, Resources.AudienceNotEquals)
                .ThrowException();


            Student = student;
            Course = course;
            PaidValue = paidValue;
            HasDiscount = paidValue < course.Cost;
        }
    }
}