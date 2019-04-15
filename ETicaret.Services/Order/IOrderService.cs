using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Order
{
    public interface IOrderService
    {
        Siparis PlaceOrder(int userId, IEnumerable<Sepet> sepet, int billingAddressId, int shippingAddressId);
        IQueryable<Siparis> GetAllOrders();
        Siparis GetOrderById(int id);
        void ChangeOrderStatus(int id, int statusId);
    }
}
