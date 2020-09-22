using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;

namespace Services
{
    public interface ISystemConfigService
    {
        Task<bool> CreateDepositAsync();
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

        public Task<bool> CreateDepositAsync()
        {
            throw new NotImplementedException();
        }
    }
}
