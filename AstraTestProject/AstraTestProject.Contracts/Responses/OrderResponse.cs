using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstraTestProject.Contracts.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public int Amount { get; set; }
        public string CarModel { get; set; }
        public decimal CarCost { get; set; }
        public decimal TotalCost { get; set; }

    }
}
