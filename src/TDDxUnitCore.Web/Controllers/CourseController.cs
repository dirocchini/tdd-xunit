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
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new DTOCourse());
        }

        [HttpPost]
        public IActionResult Create(DTOCourse model)
        {
            _courseService.Save(model);
            return View();
        }
    }
}