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
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int index = 0)
        {
            var productTypes = await _productTypeService.Get();

            return View(productTypes);
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
        public async Task<ActionResult> Add(ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Add", new { errors = value });
            }

            await _productTypeService.Add(productType);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Renove(int idpt)
        {
            _productTypeService.Remove(idpt);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> Update(int idpt, string errors = "")
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            var productType = await _productTypeService.GetById(idpt);

            ViewBag.Errors = listError;
            return View(productType);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Update", new { idpt = productType.Id, errors = value });
            }

            await _productTypeService.Update(productType);
            return RedirectToAction("Index");
        }
    }
}