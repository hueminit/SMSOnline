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
    public interface IContactService
    {
        //Task<bool> CreateContact(string userId);
        Task<ContactViewModel> GetAllContact();
        Task<bool> DeleteContact(string idContact);
        Task<bool> BlockUser(string idContact);
    }

    public class ContactService : RepositoryBase<Contact>, IContactService
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IDbFactory dbFactory, IUnitOfWork unitOfWork, IUserService userService) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = AutoMapperConfig.Mapper;
        }

        //public async Task<bool> CreateContact(string userId)
        //{
        //    var user = await _userService.GetUserById(userId);
        //    if (user != null)
        //    {
        //        Contact contact = new Contact()
        //        {
        //            FullName = user.FullName,
        //            PhoneNumber = user.PhoneNumber,
        //            StatusRequest = true,
        //            UserId = user.Id
        //        };
        //    }
        //    // var contact = _mapper.Map<ContactViewModel, Contact>(model);
        //}

        public async Task<ContactViewModel> GetAllContact()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteContact(string idContact)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> BlockUser(string idContact)
        {
            throw new NotImplementedException();
        }
    }
}
