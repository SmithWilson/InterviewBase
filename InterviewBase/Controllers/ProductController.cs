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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;

        public ProductController(
            IProductService productService,
            IProductTypeService productTypeService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int index = 0)
        {
            var products = await _productService.Get(10, index * 10);
            ViewBag.Index = index;
            ViewBag.Count = await _productService.GetCount();

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> Add(string errors)
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            ViewBag.Types = await _productTypeService.Get();
            return View("Add", listError);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Add", new { errors = value });
            }

            await _productService.Add(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Renove(int idproduct)
        {
            _productService.Remove(idproduct);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> Update(int idproduct, string errors = "")
        {
            var listError = new List<string>();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                listError = errors.Split('.').ToList();
            }

            var product = await _productService.GetById(idproduct);

            
            ViewBag.Types = await _productTypeService.Get();
            ViewBag.Errors = listError;
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Product product)
        {
            if (!ModelState.IsValid)
            {
                var value = ModelState.Values.GetParams();
                return RedirectToAction("Update", new { idproduct = product.Id, errors = value });
            }

            await _productService.Update(product);
            return RedirectToAction("Index");
        }
    }
}