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
            return View(_courseService.GetAll());
        }

        public IActionResult CreateOrEdit(int id)
        {
            if(id == 0)
                return View(new DTOCourse());

            return View(_courseService.Get(id));
        }

        [HttpPost]
        public IActionResult Create(DTOCourse course)
        {
            _courseService.Save(course);
            return View();
        }
    }
}