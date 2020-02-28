using System;
using System.Collections.Generic;

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

        public List<DTOCourse> GetAll()
        {
            var courses = _courseRepository.Get();
            List<DTOCourse> dtoCourses = new List<DTOCourse>();

            courses.ForEach(c => 
                    dtoCourses.Add(new DTOCourse(c.Id, c.Name, c.Description, c.Workload, c.Audience.ToString(), c.Cost))
                );

            return dtoCourses;
        }

        public DTOCourse Get(int id)
        {
            var course = _courseRepository.Get(id);
            return new DTOCourse(course.Id, course.Name, course.Description, course.Workload, course.Audience.ToString(), course.Cost);
        }
    }
}