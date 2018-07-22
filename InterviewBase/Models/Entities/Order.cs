using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewBase.Models.Entities
{
    public class Order
    {
        public Order(Product p, Customer c, Employee e, int count = 1)
        {
            if (count <= 0)
            {
                throw new ArgumentException(nameof(count));
            }

            Product = p ?? throw new ArgumentNullException(nameof(p));
            Customer = c ?? throw new ArgumentNullException(nameof(c));
            Employee = e ?? throw new ArgumentNullException(nameof(e));
            Count = count;
            Price = Count * Product.Price;
        }

        public Order()
        {

        }

        public int Id { get; set; }

        public DateTime Date { get; private set; } = DateTime.Now;

        [Required]
        public int Count { get; set; }
        
        public float Price { get; private set; }

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