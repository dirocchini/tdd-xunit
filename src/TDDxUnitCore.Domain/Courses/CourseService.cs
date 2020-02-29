using System;
using System.Collections.Generic;
using TDDxUnitCore.Domain._Base;

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

            RulerValidator.New()
                .When(courseAlreadySaved != null, Resources.NameAlreadyUsed)
                .When(!Enum.TryParse<Audience>(dtoCourse.Audience, out var audience), Resources.InvalidAudience)
                .ThrowException();

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