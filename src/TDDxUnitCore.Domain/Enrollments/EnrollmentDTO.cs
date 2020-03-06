namespace TDDxUnitCore.Domain.Enrollments
{
    public class EnrollmentDTO
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public double CostPaid { get; set; }


        public EnrollmentDTO(int courseId, int studentId, double costPaid)
        {
            CourseId = courseId;
            StudentId = studentId;
            CostPaid = costPaid;
        }
    }
}