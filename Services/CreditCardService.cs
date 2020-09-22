using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.ViewModel;
using Models.ViewModel.Others;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Services
{
    public interface ICreditCardService
    {
        Task<bool> Create(CreditCardRequestModel model, string userId);
        Task<List<CreditCardViewModel>> GetAllCreditCardsAsync(string customerId);
    }

    public class CreditCardService : RepositoryBase<CreditCard>, ICreditCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreditCardService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
        }

        public async Task<bool> Create(CreditCardRequestModel model, string userId)
        {
            try
            {
                var creditCard = _mapper.Map<CreditCardRequestModel, CreditCard>(model);
                creditCard.UserId = userId;
                creditCard.DateRegistered = DateTime.Now;
                await Add(creditCard);
                return await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
            }

            return false;
        }

        public async Task<List<CreditCardViewModel>> GetAllCreditCardsAsync(string customerId)
        {
            var query = await GetMultiAsync(x => x.UserId == customerId);
            return await _mapper.ProjectTo<CreditCardViewModel>(query).ToListAsync();
        }
    }
}