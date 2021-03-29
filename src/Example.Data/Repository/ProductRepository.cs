using Example.Business.Intefaces;
using Example.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Example.Data.Context;

namespace Example.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ExampleDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsByVendor(Guid vendorId)
        {
            return await Search(p => p.VendorId == vendorId);
        }

        public async Task<IEnumerable<Product>> GetProductsVendors()
        {
            return await Db.Products.AsNoTracking()
                .Include(v => v.Vendor)
                .OrderBy(p=> p.Name)
                .ToListAsync();
        }

        public async Task<Product> GetProductWithVendor(Guid id)
        {
            return await Db.Products.AsNoTracking()                
                .Include(v => v.Vendor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
