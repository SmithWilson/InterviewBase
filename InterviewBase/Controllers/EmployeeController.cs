using InterviewBase.Extensions;
using InterviewBase.Models.Entities;
using InterviewBase.Services.Abstractions.DbSevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InterviewBase.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int index = 0)
        {
            var employees = await _employeeService.Get(10, index * 10);
            ViewBag.Index = index;
            ViewBag.Count = await _employeeService.GetCount();

            return View(employees);
        }

        [HttpGet]
        public ActionResult Add(string errors)
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            return View("Add", listError);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Add", new { errors = value });
            }

            await _employeeService.Add(employee);
            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public ActionResult Renove(int idemployee)
        {
            _employeeService.Remove(idemployee);
            return RedirectToAction("Index", "Employee");
        }


        [HttpGet]
        public async Task<ActionResult> Update(int idemployee, string errors = "")
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            var employee = await _employeeService.GetById(idemployee);

            ViewBag.Errors = listError;
            return View(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Update", new { idemployee = employee.Id, errors = value });
            }

            await _employeeService.Update(employee);
            return RedirectToAction("Index", "Employee");
        }
    }
}