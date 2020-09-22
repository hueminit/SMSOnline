using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.Enums;
using Models.ViewModel;
using Models.ViewModel.Others;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data.Helpers;
using Models.Shared;

namespace Services
{
    public interface IMessageService : IRepository<Message>
    {
        Task<bool> CreateMessage(MessageViewModel message);

        Task<bool> CreateMessageProcess(MessageRequest message, AppUserViewModel currentUser, bool deductingFromAccount);

        Task<List<MessageViewModel>> GetMessagesByUserReceivedAsync(string userSent, string userReceived);
        Task<List<MessageViewModel>> GetAllMessagesOfCurrentUser(string currentUserId);
        Task<PaginationSet<MessageViewModel>> GetAllMessagesOfCurrentUserPaging(string currentUserId, int page = 1, int pageSize = 8);
        List<MessageViewModel> GetMessagesByUserReceived(string userSent, string userReceived);
        Task<bool> Save();
    }

    public class MessageService : RepositoryBase<Message>, IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Mapper;
        }

        public async Task<bool> CreateMessage(MessageViewModel message)
        {
            try
            {
                var model = _mapper.Map<MessageViewModel, Message>(message);
                await Add(model);
                return true;
            }
            catch (Exception e)
            {
                //todo
            }
            return false;
        }

        public async Task<bool> CreateMessageProcess(MessageRequest message, AppUserViewModel currentUser, bool deductingFromAccount)
        {
            var model = new MessageViewModel()
            {
                FullNameSent = currentUser.UserName,
                FullNameReceived = message.FullNameReceived,
                UserSentId = currentUser.Id,
                UserReceivedId = message.UserReceivedId,
                Content = message.Content,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Status = Status.Active
            };
            var res = await CreateMessage(model);
            if (res)
            {
                if (deductingFromAccount)
                {
                    currentUser.Balance = currentUser.Balance - Common.Constants.MessagePrice;
                }
                else
                {
                    currentUser.TotalFreeMessage = currentUser.TotalFreeMessage - 1;
                }
                var userMap = _mapper.Map<AppUserViewModel, AppUser>(currentUser);
                DbContext.Users.Attach(userMap);
                DbContext.Entry(userMap).State = EntityState.Modified;
                return await _unitOfWork.Commit();
            }

            return false;
        }

        public async Task<List<MessageViewModel>> GetMessagesByUserReceivedAsync(string userSent, string userReceived)
        {
            var query = await GetMultiAsync(x => (x.UserSentId == userSent && x.UserReceivedId == userReceived)
                                            || (x.UserSentId == userReceived && x.UserReceivedId == userSent));
            query = query.OrderBy(x => x.DateCreated);
            var message = await _mapper.ProjectTo<MessageViewModel>(query).ToListAsync();

            var res = message?.Select(
                c =>
                {
                    if (userSent == c.UserSentId)
                    {
                        c.IsCurrentUserSent = true;
                    }
                    return c;
                }).ToList();
            return res;
        }

        public async Task<List<MessageViewModel>> GetAllMessagesOfCurrentUser(string currentUserId)
        {
            var query = await GetMultiAsync(x => x.UserReceivedId == currentUserId || x.UserSentId == currentUserId);
            query = query.OrderByDescending(x => x.DateCreated);
            var data = query.AsEnumerable().DistinctBy(x => new { x.UserSentId, x.UserReceivedId });
            var res = _mapper.ProjectTo<MessageViewModel>(data.AsQueryable()).ToList();
            var listMessage = new List<MessageViewModel>();
            var listMessageRemove = new List<MessageViewModel>();
            foreach (var message in res)
            {
                var model = res.FirstOrDefault(x => x.UserReceivedId == message.UserSentId && x.UserSentId == message.UserReceivedId);
                if (model != null && message.DateCreated > model.DateCreated)
                {
                    //two way : a=>b and b=>a
                    listMessage.Add(message);
                    listMessageRemove.Add(model);
                }
                else
                {  //one way : a=>b or b=>a
                    listMessage.Add(message);
                }
            }

            listMessage = listMessage.Except(listMessageRemove).ToList();
            return listMessage;
        }

        public async Task<PaginationSet<MessageViewModel>> GetAllMessagesOfCurrentUserPaging(string currentUserId, int page = 1, int pageSize = 8)
        {
            var query = await GetMultiAsync(x => x.UserReceivedId == currentUserId || x.UserSentId == currentUserId);
            query = query.OrderByDescending(x => x.DateCreated);
            var data = query.AsEnumerable().DistinctBy(x => new { x.UserSentId, x.UserReceivedId });
            var res = _mapper.ProjectTo<MessageViewModel>(data.AsQueryable()).ToList();
            var listMessage = new List<MessageViewModel>();
            var listMessageRemove = new List<MessageViewModel>();
            foreach (var message in res)
            {
                var model = res.FirstOrDefault(x => x.UserReceivedId == message.UserSentId && x.UserSentId == message.UserReceivedId);
                if (model != null && message.DateCreated > model.DateCreated)
                {
                    //two way : a=>b and b=>a
                    listMessage.Add(message);
                    listMessageRemove.Add(model);
                }
                else
                {  //one way : a=>b or b=>a
                    listMessage.Add(message);
                }
            }

            listMessage = listMessage.Except(listMessageRemove).ToList();
            int totalRow = listMessage.Count();
            var messages = listMessage.OrderByDescending(x => x.DateCreated)
               .Skip((page - 1) * pageSize).Take(pageSize);
            return new PaginationSet<MessageViewModel>()
            {
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                Items = messages,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"))
            };
        }


        public List<MessageViewModel> GetMessagesByUserReceived(string userSent, string userReceived)
        {
            var query = GetMulti(x => (x.UserSentId == userSent && x.UserReceivedId == userReceived)
                                                 || (x.UserSentId == userReceived && x.UserReceivedId == userSent));
            query = query.OrderBy(x => x.DateCreated);
            var message = _mapper.ProjectTo<MessageViewModel>(query).ToList();

            var res = message.Select(
                c =>
                {
                    if (userSent == c.UserSentId)
                    {
                        c.IsCurrentUserSent = true;
                    }
                    return c;
                }).ToList();
            return res;
        }


        public async Task<bool> Save()
        {
            return await _unitOfWork.Commit();
        }
    }
}