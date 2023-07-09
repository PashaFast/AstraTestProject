using System;
using System.Collections.Generic;

#nullable disable

namespace AstraTestProject.Data.AstraTestProjectDb
{
    public partial class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
