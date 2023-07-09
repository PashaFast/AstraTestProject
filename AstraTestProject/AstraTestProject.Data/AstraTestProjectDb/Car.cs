using System;
using System.Collections.Generic;

#nullable disable

namespace AstraTestProject.Data.AstraTestProjectDb
{
    public partial class Car
    {
        public Car()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
