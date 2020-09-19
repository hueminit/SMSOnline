using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Helpers;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.Shared;
using Models.ViewModel;
using PagedList;

namespace Services
{
    public interface IContactService : IRepository<Contact>
    {
        Task<bool> CreateContact(string contactSentId, AppUserViewModel userReceived, string currentUserName);
        Task<PaginationSet<ContactViewModel>> GetAllContact(bool isFriend,string contactSentId,int page = 1,int pageSize = 8);
        Task<List<ContactViewModel>> GetAllRequestFriend(string currentUserId);
        Task<List<ContactViewModel>> GetAllUserLocked(string currentUserId);
        Task<bool> AcceptRequestFriend(string currentUserId,string contactReceivedId);
        Task<bool> CancelRequestFriend(string currentUserId,string contactReceivedId);
        Task<bool> BlockUser(string contactSentId, string contactBlockId,string currentUserId, string currentUserName);
        Task<bool> Save();
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

        public async Task<bool> CreateContact(string contactSentId, AppUserViewModel userReceived, string currentUserName)
        {
            try
            {
                var model = await GetSingleByConditionAsync(x => (x.ContactReceivedId == userReceived.Id
                                                                    && x.ContactSentId == contactSentId)
                                                                   || (x.ContactReceivedId == contactSentId
                                                                       && x.ContactSentId == userReceived.Id));
                if (model == null)
                {
                    Contact contact = new Contact()
                    {
                        FullNameContactReceived = userReceived.FullName,
                        FullNameContactSent = currentUserName,
                        PhoneNumber = userReceived.PhoneNumber,
                        StatusRequest = true,
                        ContactSentId = contactSentId,
                        ContactReceivedId = userReceived.Id
                    };
                    await Add(contact);
                    return await _unitOfWork.Commit();
                }
                else
                {
                    model.FullNameContactReceived = userReceived.FullName;
                    model.FullNameContactSent = currentUserName;
                    model.PhoneNumber = userReceived.PhoneNumber;
                    model.StatusRequest = true;
                    model.ContactSentId = contactSentId;
                    model.ContactReceivedId = userReceived.Id;

                    await Update(model);
                    return await _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                //todo
            }
            return false;
        }

        public async Task<PaginationSet<ContactViewModel>> GetAllContact(bool isFriend, string contactSentId, int page = 1, int pageSize = 8)
        {
            var query = await GetMulti(x => x.ContactSentId == contactSentId && x.IsFriend);
            query = query.OrderByDescending(x => x.FullNameContactReceived).Skip(page * pageSize).Take(pageSize);
            int totalRow = query.Count();
            var res = new PaginationSet<ContactViewModel>()
            {
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                Items = _mapper.ProjectTo<ContactViewModel>(query).ToList(),
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"))
            };
            return res;
        }

        public async Task<List<ContactViewModel>> GetAllRequestFriend(string currentUserId)
        {
            var query = await GetMulti(x => x.ContactReceivedId == currentUserId
                                            && x.IsFriend == false);
            return await _mapper.ProjectTo<ContactViewModel>(query).ToListAsync();
        }

        public async Task<List<ContactViewModel>> GetAllUserLocked(string currentUserId)
        {
            var query = await GetMulti(x => x.ContactReceivedId == currentUserId && x.IsFriend == false);
            return await _mapper.ProjectTo<ContactViewModel>(query).ToListAsync();
        }

        public async Task<bool> AcceptRequestFriend(string currentUserId, string contactReceivedId)
        {
            var contact = await GetSingleByConditionAsync(x => x.IsFriend == false
                                                               && x.ContactReceivedId == currentUserId
                                                               && x.ContactSentId == contactReceivedId);
            if (contact != null)
            {
                contact.IsFriend = true;
                await Update(contact);
                return await _unitOfWork.Commit();
            }

            return false;
        }

        public async Task<bool> CancelRequestFriend(string currentUserId, string contactReceivedId)
        {
            var contact = await GetSingleByConditionAsync(x =>  (x.ContactReceivedId == currentUserId
                                                               && x.ContactSentId == contactReceivedId) 
                                                                || (x.ContactReceivedId == contactReceivedId
                                                                    && x.ContactSentId == currentUserId));
            if (contact != null)
            {
                contact.IsFriend = false;
                contact.StatusRequest = false;
                await Update(contact);
                return await _unitOfWork.Commit();
            }

            return false;
        }


        public async Task<bool> BlockUser(string contactSentId, string contactBlockId, string currentUserId,string currentUserName)
        {
            try
            {
                //var user = await _userService.GetUserById(contactBlockId,currentUserId);
                //if (user != null)
                //{
                //    Contact contact = new Contact()
                //    {
                //        F = user.FullName,
                //        PhoneNumber = user.PhoneNumber,
                //        ContactSentId = contactSentId,
                //        ContactReceivedId = contactBlockId,
                //        IsBlock = true
                //    };
                //    await Add(contact);
                //    return await _unitOfWork.Commit();
                //}

            }
            catch (Exception e)
            {
                //todo
            }
            return false;
        }

        public async Task<bool> Save()
        {
            return await _unitOfWork.Commit();
        }
    }
}
