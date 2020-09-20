using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.ViewModel;

namespace Services
{
    public interface IProductService : IRepository<Product>
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task AddAsync(ProductViewModel product);
        Task UpdateAsync(ProductViewModel product);
        Task DeleteAsync(int productId);
        Task<bool> Save();
    }

    public class ProductService : RepositoryBase<Product>, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
        }
        public async Task AddAsync(ProductViewModel product)
        {
            var model = _mapper.Map<ProductViewModel, Product>(product);
            await Add(model);
            await Save();
        }

        public async Task DeleteAsync(int productId)
        {
            await Delete(productId);
            await Save();
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var res = await GetAll();
            return await _mapper.ProjectTo<ProductViewModel>(res).ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _unitOfWork.Commit();
        }

        public async Task UpdateAsync(ProductViewModel product)
        {
            var model = _mapper.Map<ProductViewModel, Product>(product);
            await Update(model);
            await Save();
        }
    }
}
