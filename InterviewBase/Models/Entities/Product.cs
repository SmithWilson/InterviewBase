using DataAnnotationsExtensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterviewBase.Models.Entities
{
    public class Product
    {
        public Product()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Min(1)]
        public float Price { get; set; }

        [Required]
        public int TypeId { get; set; }

        public ProductType Type { get; set; }

        public List<Order> Orders { get; set; }
    }
}