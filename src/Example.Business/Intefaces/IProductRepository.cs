using Example.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business.Intefaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByVendor(Guid vendorId);
        Task<IEnumerable<Product>> GetProductsVendors();
        Task<Product> GetProductWithVendor(Guid id);
    }
}
