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
    public interface ISystemConfigService : IRepository<SystemConfig>
    {
        Task<bool> CreateOrUpdateConfigAsync(SystemConfigViewModel config);
        Task<bool> UpdateConfigAsync(SystemConfigViewModel config);
        Task<bool> DeleteConfigAsync(string code);
        Task<SystemConfigViewModel> GetConfigByCodeAsync(string code);
        Task<List<SystemConfigViewModel>> GetAllConfigAsync();
        Task<bool> Save();
    }

    public class SystemConfigService : RepositoryBase<SystemConfig>, ISystemConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SystemConfigService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
        }


        public async Task<bool> CreateOrUpdateConfigAsync(SystemConfigViewModel config)
        {
            try
            {
                var currentConfig = await GetConfigByCodeAsync(config.Code);
               
                if (currentConfig == null)
                {
                    var model = _mapper.Map<SystemConfigViewModel, SystemConfig>(config);
                    await Add(model);
                    return await _unitOfWork.Commit();
                }
                else
                {
                    currentConfig.ValueNumber = config.ValueNumber;
                    currentConfig.ValueString = config.ValueString;
                    return await UpdateConfigAsync(currentConfig);
                }
            }
            catch (Exception e)
            {
                
            }

            return false;
        }

        public async Task<bool> UpdateConfigAsync(SystemConfigViewModel config)
        {
            try
            {
                var model = _mapper.Map<SystemConfigViewModel, SystemConfig>(config);
                await Update(model);
                return await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                
            }
            return false;

        }

        public async Task<bool> DeleteConfigAsync(string code)
        {
            try
            {
                await DeleteMulti(x => x.Code == code);
                return await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
               
            }
            return false;
        }

        public async Task<SystemConfigViewModel> GetConfigByCodeAsync(string code)
        {
            var model = await GetSingleByConditionAsync(x => x.Code == code);

            if (model != null)
            {
                return _mapper.Map<SystemConfig, SystemConfigViewModel>(model);
            }

            return null;
        }

        public async Task<List<SystemConfigViewModel>> GetAllConfigAsync()
        {
            var query = await GetAll();
            return _mapper.ProjectTo<SystemConfigViewModel>(query).ToList();
        }

        public async Task<bool> Save()
        {
            return await _unitOfWork.Commit();
        }
    }
}
