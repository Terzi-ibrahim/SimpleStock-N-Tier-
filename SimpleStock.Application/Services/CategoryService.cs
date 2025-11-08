using AutoMapper;
using SimpleStock.Application.DTOs;
using SimpleStock.Application.Interfaces;
using SimpleStock.Domain.Entities;
using SimpleStock.Domain.Interfaces;


namespace SimpleStock.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            // Null-safe dönüş
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList() ?? new List<CategoryDto>();
        }


        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task CreateAsync(CategoryDto categoryDto)
        {
            // Business rule: Name boş olamaz
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
                throw new Exception("Kategori adı boş olamaz");

            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
                await _categoryRepository.DeleteAsync(category);
        }
    }
}
