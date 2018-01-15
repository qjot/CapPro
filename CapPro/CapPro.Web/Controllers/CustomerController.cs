using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using Infrastructure.Services;
using CapPro.Web.Models;
using ApplicationCore.Entities;

namespace CapPro.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly IAppLogger<CustomersService> _logger;

        public CustomerController(ICustomersService customersService, IAppLogger<CustomersService> logger) {
            _customersService = customersService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index() {
            var customerListModel = await GetCustomerListViewModelAsync();

            return View(customerListModel);
        }

        private async Task<CustomersViewModel> GetCustomerListViewModelAsync() {
                var customerList = await _customersService.GetAllCustomers();
                return new CustomersViewModel() { customerList = customerList };
            
            }
    }
}