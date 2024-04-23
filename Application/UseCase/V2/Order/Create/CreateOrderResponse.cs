using Application.UseCase.V2.Menu.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.V2.Order.Create
{
    public class CreateOrderResponse
    {
        public Guid Id {  get; set; }
        public string User { get; set; } = null!;
        public string State { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; } = null!;
    }

    public class OrderItemResponse
    {
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
