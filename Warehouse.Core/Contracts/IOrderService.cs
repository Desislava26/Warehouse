using Warehouse.Core.Models;
//поръчка
namespace Warehouse.Core.Contracts
{
    public interface IOrderService
    {
        Task PlaceOrder(CustomerOrder order);
    }
}