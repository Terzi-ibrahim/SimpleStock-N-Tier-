using AutoMapper;
using SimpleStock.Application.DTOs;
using SimpleStock.Application.Interfaces;
using SimpleStock.Domain.Entities;
using SimpleStock.Domain.Interfaces;

namespace SimpleStock.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(product);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task CreateAsync(ProductDto productDto)
        {
            // Business rule: Name boş olamaz
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new Exception("Ürün adı boş olamaz");

            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(product);

        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
                await _productRepository.DeleteAsync(product);
        }
    }
}
