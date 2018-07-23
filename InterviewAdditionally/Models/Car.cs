using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterviewAdditionally.Models
{
    public class Car
    {
        [Key]
        [ForeignKey("Person")]
        public int Id { get; set; }

        public string CarName { get; set; }

        public string CarNumber { get; set; }

        [JsonIgnore]
        public Person Person { get; set; }
    }
}