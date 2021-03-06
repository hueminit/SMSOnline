﻿using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.Enums;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data.Helpers;
using Models.Shared;
using Models.ViewModel.Others;

namespace Services
{
    public interface ITransactionService : IRepository<Transaction>
    {
        Task<bool> CreateTransactionAsync(string customerId, decimal price, TransactionType type);

        Task<PaginationSet<TransactionViewModel>> GetAllTransactionsById(string currentUserId, int page = 1, int pageSize = 8);
        Task<TransactionCustomViewModel> GetAllDeposits(string keyword, int page = 1, int pageSize = 8);
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

        public async Task<PaginationSet<TransactionViewModel>> GetAllTransactionsById(string currentUserId, int page = 1, int pageSize = 8)
        {
            var query = await GetMultiAsync(x => x.UserId == currentUserId);
            int totalRow = query.Count();
            query = query?.OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            var res = new PaginationSet<TransactionViewModel>()
            {
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                Items = _mapper.ProjectTo<TransactionViewModel>(query).ToList(),
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"))
            };
            return res;
        }

        public async Task<TransactionCustomViewModel> GetAllDeposits(string keyword, int page = 1, int pageSize = 8)
        {
            TransactionCustomViewModel model = new TransactionCustomViewModel();
            var query = await GetAll();

            int totalRow = query.Count();
            if(totalRow > 0)
            {
                var totalAmount = query?.Where(x => x.Type == TransactionType.Deposit)?.Sum(x => x.Price);
                query = query?.OrderByDescending(x => x.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
                model.Transaction = new PaginationSet<TransactionViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = _mapper.ProjectTo<TransactionViewModel>(query).ToList(),
                    MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"))
                };
                model.TotalAmount = totalAmount ?? 0;
            }
            
            return model;
        }

        
    }
}