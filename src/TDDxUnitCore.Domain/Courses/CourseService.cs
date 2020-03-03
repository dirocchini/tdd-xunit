using System;
using System.Collections.Generic;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;

namespace TDDxUnitCore.Domain.Courses
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAudienceConverter _audienceConverter;

        public CourseService(ICourseRepository courseRepository, IAudienceConverter audienceConverter)
        {
            _courseRepository = courseRepository;
            _audienceConverter = audienceConverter;
        }

        public void Save(DTOCourse dtoCourse)
        {
            var courseAlreadySaved = _courseRepository.GetByName(dtoCourse.Name);

            RulerValidator.New()
                .When(courseAlreadySaved != null && courseAlreadySaved.Id != dtoCourse.Id, Resources.NameAlreadyUsed)
                .ThrowException();

            var audience = _audienceConverter.Convert(audienceGiven: dtoCourse.Audience);

            var course = new Course(dtoCourse.Name, dtoCourse.Description, dtoCourse.Workload, audience, dtoCourse.Cost);

            if (dtoCourse.Id > 0)
            {
                var courseFromDb = _courseRepository.GetById(dtoCourse.Id);
                courseFromDb.ChangeName(dtoCourse.Name);
                courseFromDb.ChangeWorkload(dtoCourse.Workload);
                courseFromDb.ChangeCost(dtoCourse.Cost);
            }

            if (dtoCourse.Id == 0)
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