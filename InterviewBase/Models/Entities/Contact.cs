using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterviewBase.Models.Entities
{
    public class Contact
    {
        public Contact()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }


        public List<Order> Orders { get; set; }
    }
}