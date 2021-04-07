using Example.Business.Intefaces;
using Example.Business.Models;
using Example.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Business.Services
{
    public class VendorServices : BaseServices, IVendorServices
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IAddressRepository _addressRepository;

        public VendorServices(IVendorRepository vendorRepository, IAddressRepository addressRepository, INotificator notificator)
            :base(notificator)
        {
            _vendorRepository = vendorRepository;
            _addressRepository = addressRepository;
        }
        public async Task Add(Vendor vendor)
        {
            if (!ExecuteValidation(new VendorValidation(), vendor) || !ExecuteValidation(new AddressValidation(),vendor.Address)) return;

        
            if (_vendorRepository.Search(x => x.Document == vendor.Document).Result.Any())
            {
                Notify("Ja existe um fornecedor com este documento informado");
                return;
            }

            await _vendorRepository.Add(vendor);
       
        }      

        public async Task Update(Vendor vendor)
        {
            if (!ExecuteValidation(new VendorValidation(), vendor) && !ExecuteValidation(new AddressValidation(), vendor.Address)) return;

            if (_vendorRepository.Search(x => x.Document == vendor.Document && x.Id != vendor.Id).Result.Any())
            {
                Notify("Ja existe um fornecedor com este documento informado");
                return;
            }

            await _vendorRepository.Update(vendor);

        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);

        }

        public async Task Delete(Guid id)
        {
            if (_vendorRepository.GetVendorWithAddressAndProducts(id).Result.Products.Any())
            {
                Notify("O Fornecedor possui produtos cadastrados!");
                return;
            }

            await _vendorRepository.Delete(id);
        }

        public void Dispose()
        {
            _vendorRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
