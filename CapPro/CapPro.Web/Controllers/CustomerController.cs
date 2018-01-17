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
            var customerViewModel = new CustomersViewModel() { customer = new Customer() };
            return View(customerViewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id) {
            var customer = await _customersService.GetCustomer(id);
            return Json(customer);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers() {
            var customerList = await _customersService.GetAllCustomers();
            return Json(customerList);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]Customer customer) {
            await _customersService.ModifyCustomer(customer);
            return Json(customer);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Customer customer) {
            if (ModelState.IsValid) {
                _customersService.CreateCustomerAsync(customer);
                return Json(customer);
            }
                return Json("Model is not valid");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var deleteStatus = await _customersService.DeleteCustomerAsync(id);
            return Json(deleteStatus);
        }
    }
}