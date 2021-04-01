using Example.Business.Intefaces;
using Example.Business.Models;
using Example.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Example.Business.Services
{
    public class ProductServices : BaseServices, IProductServices
    {
        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product) ) return;

        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

      
    }
}
