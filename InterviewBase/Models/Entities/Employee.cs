using System.ComponentModel.DataAnnotations;

namespace InterviewBase.Models.Entities
{
    public class Employee : Contact
    {
        [Required]
        public int Age { get; set; }
    }
}