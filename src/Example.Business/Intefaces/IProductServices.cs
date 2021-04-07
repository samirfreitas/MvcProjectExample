using Example.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business.Intefaces
{
    public interface IProductServices : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);

    }
}
