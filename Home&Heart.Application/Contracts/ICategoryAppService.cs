using Home_Heart.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Contracts
{
    public interface ICategoryAppService
    {
        Task<CategoryDto> AddCategoryAsync(CategoryDto category);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
    }
}
