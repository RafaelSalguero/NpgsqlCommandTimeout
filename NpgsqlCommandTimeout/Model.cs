using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpgsqlCommandTimeout
{
    public class table1
    {
        [Key]
        public int key { get; set; }
        public double value1 { get; set; }
        public double value2 { get; set; }
        public double value3 { get; set; }
    }

    public class Model : DbContext
    {
        public Model() : base("C1")
        {
            this.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);
        }
        public virtual DbSet <table1> table1 { get; set; }
    }
}
