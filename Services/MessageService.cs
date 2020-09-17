using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Infrastructure;
using Models.AutoMapper;
using Models.Entities;
using Models.ViewModel;
using Models.ViewModel.Others;

namespace Services
{

    public interface IMessageService : IRepository<Message>
    {
        Task<bool> CreateMessage(MessageViewModel message);
        Task<List<MessageViewModel>> GetMessagesByUserReceived(string userSent, string userReceived);
        Task<List<MessageCustomViewModel>> GetNewMessages(string userSent);

        Task<bool> DeleteMessage(string contactSentId, string contactReceivedId);
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
                return await _unitOfWork.Commit();
            }

            catch (Exception e)
            {
                //todo
            }
            return false;
        }

        public async Task<List<MessageViewModel>> GetMessagesByUserReceived(string userSent, string userReceived)
        {
            var query = await GetMulti(x => x.UserSentId == userSent && x.UserReceivedId == userReceived);
            query = query.OrderByDescending(x=>x.DateCreated);
            return await _mapper.ProjectTo<MessageViewModel>(query).ToListAsync();
        }

        public async Task<List<MessageCustomViewModel>> GetNewMessages(string userSent)
        {
            var query = await GetMulti(x => x.UserSentId == userSent);
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