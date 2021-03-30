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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ExampleDbContext context): base(context){}    

        public async Task<Address> GetAddressByVendor(Guid vendorId)
        {
            return await Db.Addresses.AsNoTracking().Include(a => a.Vendor).FirstOrDefaultAsync(a => a.VendorId == vendorId);
        }
    }
}
