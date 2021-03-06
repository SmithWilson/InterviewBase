﻿using InterviewBase.Extensions;
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
        public async Task<ActionResult> Add(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Add", new { errors = value });
            }

            await _customerService.Add(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Renove(int idcustomer)
        {
            _customerService.Remove(idcustomer);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> Update(int idcustomer, string errors = "")
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            var customer = await _customerService.GetById(idcustomer);

            ViewBag.Errors = listError;
            return View(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Update", new { idcustomer = customer.Id, errors = value });
            }

            await _customerService.Update(customer);
            return RedirectToAction("Index");
        }
    }
}