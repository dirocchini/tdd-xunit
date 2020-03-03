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
            return View(_studentService.Getall());
        }
    }
}