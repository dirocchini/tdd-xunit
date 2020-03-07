using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.Domain.Enrollments
{
    public class Enrollment : Entity
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public double PaidValue { get; private set; }
        public bool HasDiscount { get; private set; }
        public decimal StudentGrade { get; private set; }
        public bool CourseFinished { get; private set; }
        public bool IsCancelled { get; private set; }

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

        public void SetStudentGrade(decimal studentGrade)
        {
            RulerValidator.New()
                .When(studentGrade < 0 || studentGrade > 10, Resources.InvalidGrade)
                .When(IsCancelled, Resources.CantFinishACancelledEnrollment)
                .ThrowException();

            StudentGrade = studentGrade;
            CourseFinished = true;
        }

        public void Cancel()
        {
            RulerValidator.New()
                .When(CourseFinished, Resources.ThisEnrollmentIsAlreadyFinished)
                .ThrowException();

            IsCancelled = true;
        }
    }
}