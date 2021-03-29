using Example.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business.Intefaces
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        Task<Vendor> GetVendorWithAddress(Guid id);
        Task<Vendor> GetVendorWithAddressAndProducts(Guid id);
    }
}
