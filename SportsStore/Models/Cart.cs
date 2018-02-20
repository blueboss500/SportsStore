using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        //member vars
        private List<CartLine> mLineCollection = new List<CartLine>();



        //methods
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = mLineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                mLineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) =>
            mLineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() =>
            mLineCollection.Sum(l => l.Product.Price * l.Quantity);

        public virtual void Clear() =>
            mLineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => mLineCollection;
    }

    
}
