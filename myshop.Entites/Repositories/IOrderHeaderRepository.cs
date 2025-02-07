using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myshop.Entities.Models;

namespace myshop.Entities.Repositories
{
    public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
    {

        void Update(OrderHeader OrderHeader);
        void UpdateStatus(int id, string? OrderStatus, string? PaymentStatus);
    }
}
