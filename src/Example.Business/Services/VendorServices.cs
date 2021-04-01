using Example.Business.Intefaces;
using Example.Business.Models;
using Example.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Example.Business.Services
{
    public class VendorServices : BaseServices, IVendorServices
    {
        public async Task Add(Vendor vendor)
        {
            if (!ExecuteValidation(new VendorValidation(), vendor) && !ExecuteValidation(new AddressValidation(),vendor.Address)) return;

       
        }      

        public async Task Update(Vendor vendor)
        {
            if (!ExecuteValidation(new VendorValidation(), vendor) && !ExecuteValidation(new AddressValidation(), vendor.Address)) return;

        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
