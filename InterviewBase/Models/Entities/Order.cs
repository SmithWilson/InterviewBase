using DataAnnotationsExtensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewBase.Models.Entities
{
    public class Order
    {
        public Order()
        {

        }

        public int Id { get; set; }

        public DateTime Date { get; private set; } = DateTime.Now;

        [Required]
        [Min(1)]
        public int Count { get; set; }
        
        public float Price { get; set; }

        [Required]
        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public int? ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}