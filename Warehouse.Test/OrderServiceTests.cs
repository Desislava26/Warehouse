using Microsoft.Extensions.DependencyInjection;
using Warehouse.Core.Contracts;
using Warehouse.Core.Models;
using Warehouse.Core.Services;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Data.Repositories;

namespace Warehouse.Test
{
    public class OrderServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDBContext dbContext;
        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDBContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IOrderService, OrderService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);

        }

        [Test]
        public void UnknownCustomerMustThrow()
        {
            var order = new CustomerOrder()
            {
                CustomerNumber = "numbernumber"
            };

            var service = serviceProvider.GetService<IOrderService>();
            
            Assert.CatchAsync<ArgumentException>(async () => await service.PlaceOrder(order), "Unknown customer");
        }

        [Test]
        public void KnownCustomerMustNotThrow()
        {
            var order = new CustomerOrder()
            {
                CustomerNumber = "number123"
            };

            var service = serviceProvider.GetService<IOrderService>();

            Assert.DoesNotThrowAsync(async () => await service.PlaceOrder(order));
        }

        [Test]
        public void NotEnoughItemsShoudThrow()
        {
            var order = new CustomerOrder()
            {
                CustomerNumber = "number123",
                Items = new List<ItemOrder>()
                {
                    new ItemOrder()
                    {
                        Barcode = "1234567890",
                        Count = 9
                    }
                }
            };

            var service = serviceProvider.GetService<IOrderService>();
            Assert.CatchAsync<ArgumentException>(async () => await service.PlaceOrder(order), "Not enough quantity");
        }

        [Test]
        public void EnoughItemsShoudNotThrow()
        {
            var order = new CustomerOrder()
            {
                CustomerNumber = "number123",
                Items = new List<ItemOrder>()
                {
                    new ItemOrder()
                    {
                        Barcode = "1234567890",
                        Count = 4
                    }
                }
            };

            var service = serviceProvider.GetService<IOrderService>();
            Assert.DoesNotThrowAsync(async () => await service.PlaceOrder(order));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var customer = new Contragent()
            {
                CustomNumber = "number123",
                Name = "Desi"

            };

            var item = new Item()
            {
                Barcode = "1234567890",
                Category = new Category()
                {
                    DateFrom = DateTime.Now,
                    Label = "Computers"
                },
                DateFrom = DateTime.Now,
                Label = "Laptop",
                Racks = new List<Rack>()
                {
                    new Rack()
                    {
                        ItemsCount = 5,
                        Number = "24",
                        Section = "3C",
                        IsInUse = true
                    }
                }
            };
            await repo.AddAsync(item);
            await repo.AddAsync(customer);
            await repo.SaveChangesAsync();
        }
    }
}