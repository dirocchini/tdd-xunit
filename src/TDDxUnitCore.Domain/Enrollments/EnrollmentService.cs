using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.Domain.Enrollments
{
    public class EnrollmentService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(ICourseRepository courseRepository, IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
        }


        public void Save(EnrollmentDTO enrollmentDto)
        {
            var course = _courseRepository.GetById(enrollmentDto.CourseId);
            var student = _studentRepository.GetById(enrollmentDto.StudentId);

            RulerValidator.New()
                .When(course == null, Resources.CourseNotFound)
                .When(student == null, Resources.StudentNotFound)
                .ThrowException();

            _enrollmentRepository.Add(new Enrollment(student, course, enrollmentDto.CostPaid));

        }
    }
}