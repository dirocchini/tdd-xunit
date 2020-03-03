using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }


        public IActionResult Index()
        {
            return View(_studentService.GetAll());
        }

        public IActionResult CreateOrEdit(int id)
        {
            if(id == 0)
                return View(new StudentDTO());

            return View(_studentService.Get(id));
        }

        [HttpPost]
        public IActionResult CreateOrEdit(StudentDTO studentDto)
        {
            _studentService.Save(studentDto);
            return RedirectToAction("Index");
        }
    }
}