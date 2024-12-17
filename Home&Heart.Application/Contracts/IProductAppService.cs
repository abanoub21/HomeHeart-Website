using Home_Heart.Application.Dtos;
using Home_Heart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Contracts
{
    public interface IProductAppService
    {
        Task<ProductDto> AddProductAsync(ProductDto product);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<List<ProductDto>> GetProductsByCompanyIdAsync(int companyId);
    }
}
