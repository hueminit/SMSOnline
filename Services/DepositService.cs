using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.Enums;
using Models.ViewModel;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Services
{
    public interface IDepositService
    {
        Task<bool> CreateDepositAsync(DepositViewModel model, AppUserViewModel user);
    }

    public class DepositService : RepositoryBase<Deposit>, IDepositService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public DepositService(IDbFactory dbFactory, IUnitOfWork unitOfWork, ITransactionService transactionService) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
            _mapper = AutoMapperConfig.Mapper;
        }

        public async Task<bool> CreateDepositAsync(DepositViewModel model, AppUserViewModel user)
        {
            try
            {
                if (model.Amount < 0)
                {
                    return false;
                }

                var transaction = await _transactionService.CreateTransactionAsync(user.Id, model.Amount, TransactionType.Deposit);
                if (transaction)
                {
                    var deposit = _mapper.Map<DepositViewModel, Deposit>(model);
                    deposit.UserId = user.Id;
                    deposit.CreatedAt = DateTime.Now;
                    user.Balance += model.Amount;
                    await Add(deposit);
                    var us = _mapper.Map<AppUserViewModel, AppUser>(user);
                    DbContext.Users.Attach(us);
                    DbContext.Entry(us).State = EntityState.Modified;
                    return await _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
            }

            return false;
        }
    }
}