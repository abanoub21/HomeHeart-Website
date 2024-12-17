using AutoMapper;
using Home_Heart.Application.Contracts;
using Home_Heart.Application.Dtos;
using Home_Heart.Domain;

namespace Home_Heart.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IAsyncRepository<Category> repository;
        private readonly IMapper mapper;

        public CategoryAppService(IAsyncRepository<Category> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto category)
        {
            var getter = mapper.Map<Category>(category);
            var addCategory = await repository.AddAsync(getter);
            return mapper.Map<CategoryDto>(addCategory);
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var allData = await repository.GetAllAsync();
            return mapper.Map<List<CategoryDto>>(allData);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await repository.GetAllAsync();
            return mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var catId = await repository.GetByIDAsync(id);
            return mapper.Map<CategoryDto>(catId);
        }
    }
}
