using Application.Common;
using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<IEnumerable<GetOrderDTO>>> GetAllOrders(bool sortByTotal = false);
        Task<Result<GetOrderDTO>> GetOrderById(int id);
        Task<Result<object>> AddOrder(PostOrderDTO order);
        Task<Result<object>> UpdateOrderStatus(int id, int orderStatusId);
    }
}
