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
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IEmployeeService _employeeService;

        public OrderController(
            IOrderService orderService,
            ICustomerService customerService,
            IProductService productService,
            IEmployeeService employeeService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<ActionResult> Index(int index = 0)
        {
            var orders = await _orderService.Get(10, index * 10);
            ViewBag.Index = index;
            ViewBag.Count = await _orderService.GetCount();

            return View(orders);
        }

        [HttpGet]
        public async Task<ActionResult> Add(string errors)
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            ViewBag.Customers = await _customerService.Get();
            ViewBag.Products = await  _productService.Get();
            ViewBag.Employees = await _employeeService.Get();
            return View("Add", listError);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Order order)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Add", new { errors = value });
            }

            await _orderService.Add(order);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<ActionResult> Update(int idOrder, string errors = "")
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            var order = await _orderService.GetById(idOrder);

            ViewBag.Errors = listError;
            return View(order);
        }
    }
}