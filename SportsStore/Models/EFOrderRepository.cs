using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        //member vars
        private ApplicationDbContext mContext;

        //constructor
        public EFOrderRepository(ApplicationDbContext context)
        {
            mContext = context;
        }

        //properties
        public IQueryable<Order> Orders => mContext.Orders
                                           .Include(o => o.Lines)
                                           .ThenInclude(l => l.Product);


        //interface methods
        public void SaveOrder(Order order)
        {
            mContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                mContext.Orders.Add(order);
            }

            mContext.SaveChanges();
        }
    }
}
