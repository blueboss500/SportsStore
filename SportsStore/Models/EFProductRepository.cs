using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        //member vars
        private ApplicationDbContext mContext;

        //constructor
        public EFProductRepository(ApplicationDbContext ctx)
        {
            mContext = ctx;
        }

        public IQueryable<Product> Products => mContext.Products;
    }
}
