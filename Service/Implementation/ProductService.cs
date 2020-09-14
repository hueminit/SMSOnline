using AutoMapper;
using Data.Infrastructure;
using Model.Entites;
using Model.ViewModel;
using Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product, int> productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> UpdateAsync(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            return await _productRepository.UpdateAsync(product.Id, product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.RemoveByIdAsync(id);
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var listProduct = await _productRepository.FindAllAsync();
            return _mapper.ProjectTo<ProductViewModel>(listProduct).ToList();
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            return _mapper.Map<Product, ProductViewModel>(product);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.Commit();
        }
    }
}