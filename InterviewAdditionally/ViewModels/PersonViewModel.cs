using InterviewAdditionally.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewAdditionally.ViewModels
{
    public class PersonViewModel
    {
        public Fio Fio { get; set; }

        public string BirthDate { get; set; }

        public Car Car { get; set; }
    }
}