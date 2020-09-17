using System;
using System.Collections.Generic;
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
    public interface ITestService : IRepository<Test>
    {
        Task<List<TestViewModel>> GetAllAsync();
        Task AddAsync(TestViewModel test);
        Task UpdateAsync(TestViewModel test);
        Task DeleteAsync(int testId);
        Task<bool> Save();
    }

    public class TestService : RepositoryBase<Test>, ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TestService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
        }

        public async Task<List<TestViewModel>> GetAllAsync()
        {
            var res = await GetAll();
            return  _mapper.ProjectTo<TestViewModel>(res).ToList();
        }

        public async Task AddAsync(TestViewModel test)
        {
            var model = _mapper.Map<TestViewModel, Test>(test);
            await Add(model);
        }

        public async Task UpdateAsync(TestViewModel test)
        {
            var model = _mapper.Map<TestViewModel, Test>(test);
            await Update(model);
        }

        public async Task DeleteAsync(int testId)
        {
            await Delete(testId);
        }

        public async Task<bool> Save()
        {
            return await _unitOfWork.Commit();
        }
    }
}
