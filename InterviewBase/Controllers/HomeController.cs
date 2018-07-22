using System.Web.Mvc;
using InterviewBase.Services.Abstractions.DbSevice;
using System.Threading.Tasks;
using InterviewBase.Models.Entities;
using InterviewBase.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace InterviewBase.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        
        public async Task<ActionResult> Index()
        {
            
            return View();
        }
    }
}