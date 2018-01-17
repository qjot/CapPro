using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class CustomersService : ICustomersService {

        private readonly IAsyncRepository<Customer> _customerRepository;
        private readonly IAppLogger<CustomersService> _logger;

        public CustomersService(IAsyncRepository<Customer> customerRepository, IAppLogger<CustomersService> logger) {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public Task<List<Customer>> GetAllCustomers() {
            {
                return _customerRepository.ListAllAsync();
            }
        }

        public Task<Customer> GetCustomer(int customerID) {
            return _customerRepository.GetByIdAsync(customerID);
        }

        public async Task CreateCustomerAsync(Customer customer) {
            await _customerRepository.AddAsync(customer);
        }
        public async Task ModifyCustomer(int customerID, string name, string surname, string telephoneNumber, string address) {
            var customer = await _customerRepository.GetByIdAsync(customerID);
            customer.name = name;
            customer.surname = surname;
            customer.telephoneNumber = telephoneNumber;
            customer.address = address;
            await _customerRepository.UpdateAsync(customer);
        }
        public async Task ModifyCustomer(Customer updatedCustomer) {
            var customerToUpdate = _customerRepository.GetByIdAsync(updatedCustomer.ID).Result;
            if(customerToUpdate != null) {            
            customerToUpdate.name = updatedCustomer.name;
            customerToUpdate.surname = updatedCustomer.surname;
            customerToUpdate.telephoneNumber = updatedCustomer.telephoneNumber;
            customerToUpdate.address = updatedCustomer.address;
            await _customerRepository.UpdateAsync(customerToUpdate);
            }
        }
        public async Task<bool> DeleteCustomerAsync(int customerID) {
            var customer =  _customerRepository.GetByIdAsync(customerID).Result;
            if(customer != null) {
                await _customerRepository.DeleteAsync(customer);
                return true;
            }
            return false;
        }
    }
}