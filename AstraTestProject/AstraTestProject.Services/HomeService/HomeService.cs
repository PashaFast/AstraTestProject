using AstraTestProject.Contracts.Responses;
using AstraTestProject.Data.AstraTestProjectDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace AstraTestProject.Services.HomeService
{
    public class HomeService : IHomeService
    {
        //todo проверить, что в Контексете НЕТ хпрдкода со сторокой подключения к БД

        private readonly AstraTestProjectContext _context;
        private readonly IMapper _mapper;

        public HomeService(AstraTestProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OrderResponse>> GetAllOrders()
        {
            var orders = _context.Orders
                .Include(o => o.Car)
                .Include(o => o.Customer);

            var response = await orders.Select(x => _mapper.Map<OrderResponse>(x)).ToListAsync();
            return response;

        }
    }
}
