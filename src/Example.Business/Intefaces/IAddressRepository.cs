using Example.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business.Intefaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByVendor(Guid vendorId);      
    }
}
