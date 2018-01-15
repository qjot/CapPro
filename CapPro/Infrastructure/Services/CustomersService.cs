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

        public async Task CreateCustomerAsync(string clientName, string clientSurname, string clientTelephoneNumber, string clientAddress) {
            var newCustomer = new Customer() {
                name = clientName,
                surname = clientSurname,
                telephoneNumber = clientTelephoneNumber,
                address = clientAddress
            };
            await _customerRepository.AddAsync(newCustomer);
        }
        public async Task ModifyCustomer(int customerID, string name, string surname, string telephoneNumber, string address) {
            var customer = await _customerRepository.GetByIdAsync(customerID);
            customer.name = name;
            customer.surname = surname;
            customer.telephoneNumber = telephoneNumber;
            customer.address = address;
            await _customerRepository.UpdateAsync(customer);
        }
        public async Task DeleteCustomerAsync(int customerID) {
            var customer = await _customerRepository.GetByIdAsync(customerID);
            await _customerRepository.DeleteAsync(customer);
        }
    }
}