using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class OrderService: IOrderService
    {
        private readonly IApplicationDbContext _context;

        public OrderService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<GetOrderDTO>>> GetAllOrders(bool sortByTotal = false)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentMethod)
                .ToListAsync();

            var orderDTOs = orders.Select(o => MapToDTO(o));

            var result = sortByTotal
                ? orderDTOs.OrderByDescending(o => o.TotalAmount)
                : orderDTOs;

            return Result<IEnumerable<GetOrderDTO>>.Success(result);
        }

        public async Task<Result<GetOrderDTO>> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentMethod)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Result<GetOrderDTO>.Failure(new List<string> { "Order not found." });

            return Result<GetOrderDTO>.Success(MapToDTO(order));
        }

        public async Task<Result<object>> AddOrder(PostOrderDTO dto)
        {
            var order = dto.ToModel();
            order.OrderTime = DateTime.UtcNow;
            order.OrderStatusId = 1;

            var validation = ValidateOrder(order);
            if (!validation.IsSuccess)
                return Result<object>.Failure(validation.ValidationItems);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Result<object>.Success();
        }
        
        

        public async Task<Result<object>> UpdateOrderStatus(int id, int orderStatusId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Result<object>.Failure(new List<string> { "Order not found." });

            var statusExists = await _context.OrderStatuses
                .AnyAsync(s => s.Id == orderStatusId);

            if (!statusExists)
                return Result<object>.Failure(new List<string> { "Order status not found." });

            order.OrderStatusId = orderStatusId;
            await _context.SaveChangesAsync();
            return Result<object>.Success();
        }

        
        private static ValidationResult ValidateOrder(Order order)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(order.CustomerName))
                result.ValidationItems.Add("Customer name is required.");
            if (order.CustomerName?.Length > 100)
                result.ValidationItems.Add("Customer name cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(order.DeliveryAddress))
                result.ValidationItems.Add("Delivery address is required.");
            if (order.DeliveryAddress?.Length > 100)
                result.ValidationItems.Add("Delivery address cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(order.ContactNumber))
                result.ValidationItems.Add("Contact number is required.");
            if (order.ContactNumber?.Length > 100)
                result.ValidationItems.Add("Contact number cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(order.Currency))
                result.ValidationItems.Add("Currency is required.");
            if (order.Currency?.Length > 10)
                result.ValidationItems.Add("Currency cannot exceed 10 characters.");

            if (order.Note?.Length > 1000)
                result.ValidationItems.Add("Note cannot exceed 1000 characters.");

            if (order.PaymentMethodId <= 0)
                result.ValidationItems.Add("Payment method is required.");
            if (order.PaymentMethodId > 2)
                result.ValidationItems.Add("Payment method can only be 1=cash and 2=card.");

            if (!order.Items.Any())
                result.ValidationItems.Add("Order must have at least one item.");


            foreach (var item in order.Items)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    result.ValidationItems.Add("Item name is required.");
                if (item.Name?.Length > 100)
                    result.ValidationItems.Add("Item name cannot exceed 100 characters.");
                if (item.UnitPrice <= 0)
                    result.ValidationItems.Add("Item unit price must be greater than 0.");
                if (item.Quantity <= 0)
                    result.ValidationItems.Add("Item quantity must be greater than 0.");
            }

            return result;
        }
        private static decimal CalculateItemTotal(OrderItem item)
                    => item.Quantity * item.UnitPrice;

        private static decimal CalculateOrderTotal(Order order)
            => order.Items.Sum(i => CalculateItemTotal(i));

        private static GetOrderDTO MapToDTO(Order order)
        {
            if (order == null)
                return null;

            return new GetOrderDTO
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderTime = order.OrderTime,
                DeliveryAddress = order.DeliveryAddress,
                ContactNumber = order.ContactNumber,
                Note = order.Note,
                Currency = order.Currency,
                TotalAmount = CalculateOrderTotal(order),
                PaymentMethodId = order.PaymentMethodId,
                OrderStatusId = order.OrderStatusId,
                OrderStatus = order.OrderStatus != null ? new GetOrderStatusDTO
                {
                    Id = order.OrderStatus.Id,
                    Name = order.OrderStatus.Name
                } : null,
                PaymentMethod = order.PaymentMethod != null ? new GetPaymentMethodDTO
                {
                    Id = order.PaymentMethod.Id,
                    Name = order.PaymentMethod.Name
                } : null,
                Items = order.Items?.Select(i => new GetOrderItemDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TotalPrice = CalculateItemTotal(i)
                }).ToList() ?? new List<GetOrderItemDTO>()
            };
        }

    }
}
