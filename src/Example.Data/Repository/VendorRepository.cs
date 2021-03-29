using Example.Business.Intefaces;
using Example.Business.Models;
using Example.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Repository
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        public VendorRepository(ExampleDbContext context) : base(context) { }

        public async Task<Vendor> GetVendorWithAddress(Guid id)
        {
            return await Db.Vendors.AsNoTracking()
                .Include(v => v.Address)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vendor> GetVendorWithAddressAndProducts(Guid id)
        {
            return await Db.Vendors.AsNoTracking()
                .Include(v=> v.Products)
                .Include(v => v.Address)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
