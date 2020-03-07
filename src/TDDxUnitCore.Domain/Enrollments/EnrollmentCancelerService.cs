using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Domain.Enrollments
{
    public class EnrollmentCancelerService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentCancelerService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public void CancelEnrollment(int enrollmentId)
        {
            var enrollment = _enrollmentRepository.GetById(enrollmentId);

            RulerValidator.New()
                .When(enrollment == null, Resources.EnrollmentNotFound)
                .ThrowException();

            enrollment.Cancel();
        }
    }
}