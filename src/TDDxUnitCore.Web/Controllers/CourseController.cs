using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new DTOCourse());
        }
    }
}