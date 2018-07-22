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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int index = 0)
        {
            var customers = await _customerService.Get(10, index * 10);
            ViewBag.Index = index;
            ViewBag.Count = await _customerService.GetCount();

            return View(customers);
        }

        [HttpGet]
        public ActionResult Add(string errors = "")
        {
            return View(errors);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.GetParams();
                return View("/Customer/Add", errors );
            }

            await _customerService.Add(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Renove()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
    }
}