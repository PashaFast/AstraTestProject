using AstraTestProject.Services.HomeService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using AstraTestProject.Contracts.Responses;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;

namespace AstraTestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {

            var response = await _homeService.GetAllOrders();

            return Ok(response);

        }

    }
}
