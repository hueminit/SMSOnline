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

namespace Services
{
    public interface IMessageService : IRepository<Message>
    {
        Task<bool> CreateMessage(MessageViewModel message);

        Task<bool> CreateMessageProcess(MessageRequest message, AppUserViewModel user, bool deductingFromAccount, string currentUserId);

        Task<List<MessageViewModel>> GetMessagesByUserReceivedAsync(string userSent, string userReceived);

        Task<List<MessageCustomViewModel>> GetNewMessages(string userSent);

        List<MessageViewModel> GetMessagesByUserReceived(string userSent, string userReceived);

        //Task<bool> DeleteMessage(string contactSentId, string contactReceivedId);
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

        public async Task<bool> CreateMessageProcess(MessageRequest message, AppUserViewModel user, bool deductingFromAccount, string currentUserId)
        {
            var model = new MessageViewModel()
            {
                UserSentId = currentUserId,
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
                    user.Balance = user.Balance - Common.Constants.MessagePrice;
                }
                else
                {
                    user.TotalFreeMessage = user.TotalFreeMessage - 1;
                }
                var userMap = _mapper.Map<AppUserViewModel, AppUser>(user);
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

        public async Task<List<MessageCustomViewModel>> GetNewMessages(string userSent)
        {
            // var query = await GetMultiAsync(x => x.UserSentId == userSent);
            //todo
            // query = query.GroupJoin()
            return null;
        }

        public async Task<bool> DeleteMessage(string userSent, string userReceived)
        {
            try
            {
                await DeleteMulti(x => x.UserSentId == userSent && x.UserReceivedId == userReceived);
                return await _unitOfWork.Commit();
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