using AstraTestProject.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstraTestProject.Services.HomeService
{
    public interface IHomeService
    {
        Task<List<OrderResponse>> GetAllOrders();
    }
}
