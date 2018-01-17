using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICustomersService {
        //Task<CustomersViewModel> GetOrCreateCustomer(string userName);
        //Task TransferBasketAsync(string anonymousId, string userName);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(int customerID);
        Task CreateCustomerAsync(Customer customer);
        Task ModifyCustomer(int customerID, string name, string surname, string telephoneNumber, string address);
        Task ModifyCustomer(Customer customer);
        Task<bool>DeleteCustomerAsync(int customerID);
    }
}
