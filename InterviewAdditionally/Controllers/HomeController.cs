using InterviewAdditionally.Models;
using InterviewAdditionally.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text;
using Newtonsoft.Json;

namespace InterviewAdditionally.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Init();

            return View();
        }

        [HttpGet]
        public ActionResult TaskOne()
        {
            var response = new List<MOViewModel>();
            using (var context = new AdditionallyContext())
            {
                var models = context.ModelOnes.ToList();
                foreach (var model in models)
                {
                    response.Add(new MOViewModel
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        CarName = model.CarName,
                        BirthDate = model.BirthDate.ToShortDateString()
                    });
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TaskOneView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TaskTwoView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TaskTwo()
        {
            var response = new List<MTViewModel>();
            using (var context = new AdditionallyContext())
            {
                var models = context.ModelTwos.ToList();
                foreach (var model in models)
                {
                    response.Add(new MTViewModel
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        CarName = model.CarName,
                        CarNumber = model.CarNumber,
                        BirthDate = model.BirthDate.ToShortDateString()
                    });
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TaskThreeView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TaskThree()
        {
            var response = new List<MTViewModel>();
            using (var context = new AdditionallyContext())
            {
                var models = context.ModelTwos.ToList();
                foreach (var model in models)
                {
                    response.Add(new MTViewModel
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleName = model.MiddleName,
                        CarName = model.CarName,
                        CarNumber = model.CarNumber,
                        BirthDate = model.BirthDate.ToShortDateString()
                    });
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TaskFour()
        {
            var response = new List<PersonViewModel>();
            using (var context = new AdditionallyContext())
            {
                var models = context.People
                    .Include(p => p.Fio)
                    .Include(p => p.Car)
                    .ToList();
                foreach (var model in models)
                {
                    response.Add(new PersonViewModel
                    {
                        Fio = model.Fio,
                        Car = model.Car,
                        BirthDate = model.BirthDate.ToShortDateString()
                    });
                }
            }

            return MyJson(response);
        }

        [HttpGet]
        public ActionResult TaskFourView()
        {
            return View();
        }


        public JsonResult MyJson<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return base.Json(json, JsonRequestBehavior.AllowGet);
        }

        private void Init()
        {
            using (var context = new AdditionallyContext())
            {
                if (!context.ModelOnes.Any())
                {
                    context.ModelOnes.AddRange(new List<ModelOne>
                    {
                        new ModelOne
                        {
                            FirstName = "Иван",
                            LastName = "Иванов",
                            BirthDate = new DateTime(1978,04,15),
                            CarName = "bmw x3"
                        },
                        new ModelOne
                        {
                            FirstName = "Петр",
                            LastName = "Петров",
                            BirthDate = new DateTime(1998,08,04),
                            CarName = "mb s500"
                        }
                    });
                }

                context.SaveChanges();

                if (!context.ModelTwos.Any())
                {
                    context.ModelTwos.AddRange(new List<ModelTwo>
                    {
                        new ModelTwo
                        {
                            FirstName = "Иван",
                            LastName = "Иванов",
                            BirthDate = new DateTime(1978,04,15),
                            CarName = "bmw x3",
                            CarNumber = "Т777ТТ 777",
                            MiddleName = "Иванович"
                        },
                        new ModelTwo
                        {
                            FirstName = "Петр",
                            LastName = "Петров",
                            BirthDate = new DateTime(1998,08,04),
                            CarName = "mb s500",
                            CarNumber = "А123АА 16",
                            MiddleName = "Петрович"
                        }
                    });
                }

                context.SaveChanges();

                if (!context.People.Any())
                {
                    var people = new List<Person>
                    {
                        new Person
                        {
                            BirthDate = new DateTime(1978,04,15)
                        },
                        new Person
                        {
                            BirthDate = new DateTime(1998,08,04),
                        }
                    };

                    context.People.AddRange(people);
                    context.SaveChanges();

                    context.Cars.AddRange(new List<Car>
                    {
                        new Car
                        {
                            Id = people[0].Id,
                            CarName = "bmw x3",
                            CarNumber = "Т777ТТ 777"
                        },
                        new Car
                        {
                            Id = people[1].Id,
                            CarName = "mb s500",
                            CarNumber = "А123АА 16"
                        }
                    });

                    context.Fios.AddRange(new List<Fio>
                    {
                        new Fio {
                            Id = people[0].Id,
                            FirstName = "Иван",
                            LastName = "Иванов",
                            MiddleName = "Иванович"
                        },
                        new Fio {
                            Id = people[1].Id,
                            FirstName = "Петр",
                            LastName = "Петров",
                            MiddleName = "Петрович"
                        }
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}