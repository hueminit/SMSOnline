using AutoMapper;
using Data.Helpers;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.Shared;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IContactService : IRepository<Contact>
    {
        Task<bool> CreateContact(string contactSentId, AppUserViewModel userReceived, string currentUserName);

        Task<PaginationSet<ContactViewModel>> GetAllContact(bool isFriend, string currentUser, int page = 1, int pageSize = 8);

        Task<List<ContactViewModel>> GetAllContactOfCurrentUser(bool isFriend, string currentUser, int page = 1, int pageSize = 8);

        Task<PaginationSet<ContactViewModel>> GetAllRequestFriend(string currentUserId, int page = 1, int pageSize = 8);

        Task<List<ContactViewModel>> GetAllUserLocked(string currentUserId);

        Task<bool> AcceptRequestFriend(string currentUserId, string contactReceivedId);

        Task<bool> CancelRequestFriend(string currentUserId, string contactReceivedId);

        Task<bool> BlockUser(string contactSentId, string contactBlockId, string currentUserId, string currentUserName);

        Task<bool> Save();
    }

    public class ContactService : RepositoryBase<Contact>, IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<PaginationSet<ContactViewModel>> GetAllContact(bool isFriend, string currentUser, int page = 1, int pageSize = 8)
        {
            var query = await GetMultiAsync(x => (x.ContactSentId == currentUser || x.ContactReceivedId == currentUser) && x.IsFriend);
            query = query
                .OrderByDescending(x => x.FullNameContactReceived)
                .Skip((page - 1) * pageSize).Take(pageSize);
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

        public async Task<List<ContactViewModel>> GetAllContactOfCurrentUser(bool isFriend, string currentUser, int page = 1, int pageSize = 8)
        {
            var query = await GetMultiAsync(x => (x.ContactSentId == currentUser || x.ContactReceivedId == currentUser)  && x.IsFriend);
            return _mapper.ProjectTo<ContactViewModel>(query).ToList();
        }

        public async Task<PaginationSet<ContactViewModel>> GetAllRequestFriend(string currentUserId, int page = 1, int pageSize = 8)
        {
            var query = await GetMultiAsync(x => x.ContactReceivedId == currentUserId
                                                 && x.IsFriend == false);
            query = query?.OrderByDescending(x => x.FullNameContactReceived)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
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

        public async Task<List<ContactViewModel>> GetAllUserLocked(string currentUserId)
        {
            var query = await GetMultiAsync(x => x.ContactReceivedId == currentUserId && x.IsFriend == false);
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
            var contact = await GetSingleByConditionAsync(x => (x.ContactReceivedId == currentUserId
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

        public async Task<bool> BlockUser(string contactSentId, string contactBlockId, string currentUserId, string currentUserName)
        {
            try
            {
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