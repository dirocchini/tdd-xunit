using System;

namespace TDDxUnitCore.Domain.Courses
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Save(DTOCourse dtoCourse)
        {
            var courseAlreadySaved = _courseRepository.GetByName(dtoCourse.Name);
            if(courseAlreadySaved != null)
                throw new ArgumentException("Course Name Already Used by Another Course");

            if(!Enum.TryParse<Audience>(dtoCourse.Audience, out var audience))
                throw new ArgumentException("Must Enter a Valid Audience");

            var course = new Course(dtoCourse.Name, dtoCourse.Description, dtoCourse.Workload, audience, dtoCourse.Cost);

            _courseRepository.Add(course);
        }
    }
}