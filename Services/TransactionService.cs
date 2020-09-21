using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.Enums;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Services
{
    public interface ITransactionService
    {
        Task<bool> CreateTransactionAsync(string customerId, decimal price, TransactionType type);

        Task<List<TransactionViewModel>> GetAllTransactionsById(string currentUserId);
    }

    public class TransactionService : RepositoryBase<Transaction>, ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
        }

        public async Task<bool> CreateTransactionAsync(string customerId, decimal price, TransactionType type)
        {
            try
            {
                var transaction = new Transaction();
                transaction.UserId = customerId;
                transaction.Price = price;
                transaction.Type = type;
                transaction.CreatedAt = DateTime.Now;

                await Add(transaction);
                return true;
            }
            catch (Exception e)
            {
            }

            return false;
        }

        public async Task<List<TransactionViewModel>> GetAllTransactionsById(string currentUserId)
        {
            var query = await GetMultiAsync(x => x.UserId == currentUserId);
            return await _mapper.ProjectTo<TransactionViewModel>(query).ToListAsync();
        }
    }
}