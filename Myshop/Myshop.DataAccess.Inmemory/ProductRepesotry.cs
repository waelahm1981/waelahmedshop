using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;
using System.Linq;

namespace Myshop.DataAccess.Inmemory
{
    public class ProductRepesotry
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>(); 
        
        public ProductRepesotry()
    {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();

            }
           
        }

        public void Commit()
        {

            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product ProductToUpdate = products.Find(p => p.Id == product.Id);
            if (ProductToUpdate != null)
            {
                ProductToUpdate = product;
            }
            else
            {
                throw new Exception("product Not found");
            }
        }

        public Product Find(string id)
        {
            Product product = products.Find(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("product Not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product ProductToDelete = products.Find(i => i.Id == Id);

            if (ProductToDelete != null)
            {
                products.Remove(ProductToDelete);
            }
            else
            {
                throw new Exception( " product Not found");
            }
        }

    }
   
}
