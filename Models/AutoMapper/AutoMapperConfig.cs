﻿using AutoMapper;
using Models.Entities;
using Models.ViewModel;
using Models.ViewModel.Others;

namespace Models.AutoMapper
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUser, AppUserViewModel>().ReverseMap();
                cfg.CreateMap<AppUserViewModel, AppUser>().ReverseMap();

                cfg.CreateMap<Contact, ContactViewModel>().ReverseMap();
                cfg.CreateMap<ContactViewModel, Contact>().ReverseMap();

                cfg.CreateMap<CreditCard, CreditCardViewModel>().ReverseMap();
                cfg.CreateMap<CreditCardViewModel, CreditCard>().ReverseMap();

                cfg.CreateMap<CreditCardRequestModel, CreditCard>().ReverseMap();

                cfg.CreateMap<Deposit, DepositViewModel>().ReverseMap();
                cfg.CreateMap<DepositViewModel, Deposit>().ReverseMap();

                cfg.CreateMap<Message, MessageViewModel>().ReverseMap();
                cfg.CreateMap<MessageViewModel, Message>().ReverseMap();

                cfg.CreateMap<Transaction, TransactionViewModel>().ReverseMap();
                cfg.CreateMap<TransactionViewModel, Transaction>().ReverseMap();

                cfg.CreateMap<SystemConfigViewModel, SystemConfig>().ReverseMap();
                cfg.CreateMap<SystemConfig, SystemConfigViewModel>().ReverseMap();
            });

            Mapper = config.CreateMapper();
        }
    }
}