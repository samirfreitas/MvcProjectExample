using Example.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business.Intefaces
{
    public interface IVendorServices
    {
        Task Add(Vendor vendor);
        Task Update(Vendor vendor);
        Task Delete(Guid id);
        Task UpdateAddress(Address address);
    }
}
