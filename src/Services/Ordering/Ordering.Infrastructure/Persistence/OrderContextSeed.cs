using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {
                  UserName= "swn",
                  TotalPrice= 100,
                  FirstName = "swn",
                  LastName= "swn",
                  EmailAddress= "string",
                  AddressLine= "string",
                  Country="string",
                  State= "string",
                  ZipCode="string",
                  CardName="string",
                  CardNumber="string",
                  Expiration="string",
                  CVV="string",
                  PaymentMethod= 1
                }
            };
        }
    }
}
