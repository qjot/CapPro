using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace Infrastructure.Data {
    public class ManagementContextSeed {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder,
    ManagementContext managementContext,
    ILoggerFactory loggerFactory) {
            try {
                if (!managementContext.Customers.Any()) {
                    managementContext.Customers.AddRange(
                        GetPreconfiguredListOfCustomers());

                    await managementContext.SaveChangesAsync();
                }
            }
            catch (Exception ex) {
                var log = loggerFactory.CreateLogger<ManagementContextSeed>();
                log.LogError(ex.Message);
                await SeedAsync(applicationBuilder, managementContext, loggerFactory);
            }
        }

         static IEnumerable<Customer> GetPreconfiguredListOfCustomers() {
            return new List<Customer>() {
                new Customer() {
                    name = "Michał",
                    surname = "Bieliński",
                    address = "Teatralna 9/27, Poznań, 39-981, Polska",
                    telephoneNumber = "758334092"
                },
                new Customer() {
                    name = "Tymon",
                    surname = "Kowal",
                    address = "Racławicka 18, Warszawa, 48-145, Polska",
                    telephoneNumber = "881785219"
                },
                 new Customer() {
                    name = "Hanna",
                    surname = "Wójcik",
                    address = "Opolska 91, Łódź, 28-421, Polska",
                    telephoneNumber = "726672044"
                }
        };
    }
  }
}
