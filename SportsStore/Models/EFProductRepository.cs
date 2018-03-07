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
        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                mContext.Products.Add(product);
            }
            else
            {
                Product record = mContext.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                if (record != null)
                {
                    record.Name = product.Name;
                    record.Description = product.Description;
                    record.Price = product.Price;
                    record.Category = product.Category;
                }
            }

            mContext.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = mContext.Products.FirstOrDefault(p => p.ProductID == productID);

            if (dbEntry != null)
            {
                mContext.Products.Remove(dbEntry);
                mContext.SaveChanges();
            }

            return dbEntry;
        }
    }
}
