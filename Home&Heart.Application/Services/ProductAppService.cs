using AutoMapper;
using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Home_Heart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IAsyncRepository<Product> repository;
        private readonly IMapper mapper;

        public ProductAppService(IAsyncRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ProductDto> AddProductAsync(ProductDto product)
        {
            if (string.IsNullOrEmpty(product.Image) || !File.Exists(product.Image))
            {
                throw new FileNotFoundException("The provided image path is invalid or the file does not exist.");
            }

            byte[] imageData = File.ReadAllBytes(product.Image);
            var obj = new Product
            {
                Id = product.Id,
                Image = imageData, // خزن الصورة كـ byte[]
                CompanyId = product.CompanyId,
            };
            var addProduct = await repository.AddAsync(obj);
            return mapper.Map<ProductDto>(addProduct);
        }

        public Task<List<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var productId = await repository.GetByIDAsync(id);
            var productDto = mapper.Map<ProductDto>(productId);

            // Convert image to Base64
            if (productId.Image != null)
            {
                productDto.Image = Convert.ToBase64String(productId.Image);
            }

            return productDto;
        }

        public async Task<List<ProductDto>> GetProductsByCompanyIdAsync(int companyId)
        {
            var products = await repository.GetByForeignKeyAsync(p => p.CompanyId == companyId);
            var productDtos = mapper.Map<List<ProductDto>>(products);
            foreach (var productDto in productDtos)
            {
                var product = products.FirstOrDefault(p => p.Id == productDto.Id);
                if (product != null && product.Image != null)
                {
                    productDto.Image = Convert.ToBase64String(product.Image);
                }
            }

            return productDtos;
        }
    }
}
