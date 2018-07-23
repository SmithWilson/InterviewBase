using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InterviewAdditionally.Models
{
    public class AdditionallyContext : DbContext
    {
        public AdditionallyContext() : base("DefaultConnection")
        {
        }

        public DbSet<ModelOne> ModelOnes { get; set; }

        public DbSet<ModelTwo> ModelTwos { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Fio> Fios { get; set; }
    }
}