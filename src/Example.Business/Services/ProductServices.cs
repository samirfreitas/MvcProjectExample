using Example.Business.Intefaces;
using Example.Business.Models;
using Example.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Example.Business.Services
{
    public class ProductServices : BaseServices, IProductServices
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository, INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product) ) return;

            await _productRepository.Add(product);

        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
