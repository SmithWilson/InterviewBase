using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewAdditionally.Models
{
    public class Person
    {
        public int Id { get; set; }
        
        public Fio Fio { get; set; }

        public DateTime BirthDate { get; set; }

        public Car Car { get; set; }
    }
}