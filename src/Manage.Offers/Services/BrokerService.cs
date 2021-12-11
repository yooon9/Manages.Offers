namespace Manage.Offers.Services
{
    using AutoMapper;
    using Manage.Offers.Data.Context;
    using Manage.Offers.Data.Entities;
    using Manage.Offers.Exceptions;
    using Manage.Offers.Helpers;
    using Manage.Offers.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BrokerService : IBrokerService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public BrokerService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        private async Task<Broker> GetBrokerEntityById(int brokerId)
        {
            if (!await ExistBroker(brokerId))
                throw new BrokerNotFoundException(brokerId, "Broker not found");
            return await _dataContext.Brokers.FirstOrDefaultAsync(b => b.Id == brokerId);
        }

        public async Task<BrokerResponseModel> GetBrokerById(int brokerId)
        {
            return _mapper.Map<BrokerResponseModel>(await GetBrokerEntityById(brokerId));
        }

        public async Task<PagedList<BrokerResponseModel>> GetBrokers(SearchBrokerModel search = null, int pageNumber = 1, int pageSize = 25)
        {
            if (search is null)
            {
                var pagedListWithoutSearch = await PagedList<Broker>.CreateAsync(_dataContext.Brokers.OrderByDescending(a => a.CreationDate), pageNumber, pageSize);
                return new PagedList<BrokerResponseModel>(_mapper.Map<List<BrokerResponseModel>>(pagedListWithoutSearch.ToList()), pagedListWithoutSearch.Count, pagedListWithoutSearch.CurrentPage, pagedListWithoutSearch.PageSize);
            }

            var Brokers = _dataContext.Brokers.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
                Brokers = Brokers.Where(a => a.Name.Contains(search.Name));
            if (search.Type != null)
                Brokers = Brokers.Where(a => a.Type == search.Type);
            if (!string.IsNullOrEmpty(search.PhoneNumber))
                Brokers = Brokers.Where(a => a.PhoneNumber.Contains(search.PhoneNumber));
            if (!string.IsNullOrEmpty(search.Email))
                Brokers = Brokers.Where(a => a.Email.Contains(search.Email));
            if (!string.IsNullOrEmpty(search.Address))
                Brokers = Brokers.Where(a => a.Address.Contains(search.Address));
            if (!string.IsNullOrEmpty(search.Bio))
                Brokers = Brokers.Where(a => a.Bio.Contains(search.Bio));
            var pagedList = await PagedList<Broker>.CreateAsync(Brokers.OrderByDescending(a => a.CreationDate), pageNumber, pageSize);
            return new PagedList<BrokerResponseModel>(_mapper.Map<List<BrokerResponseModel>>(pagedList.ToList()), pagedList.Count, pagedList.CurrentPage, pagedList.PageSize);
        }

        public async Task<int> AddBroker(BrokerRequestModel request)
        {
            var broker = _mapper.Map<Broker>(request);
            broker.CreationDate = DateTime.Now;
            broker.LastUpdateDate = DateTime.Now;
            await _dataContext.AddAsync(broker);
            await _dataContext.SaveChangesAsync();
            return broker.Id;
        }

        public async Task UpdateBroker(int brokerId, BrokerRequestModel request)
        {
            var broker = await GetBrokerEntityById(brokerId);
            broker.Name = request.Name;
            broker.PhoneNumber = request.PhoneNumber;
            broker.Type = request.Type;
            broker.Email = request.Email;
            broker.Address = request.Address;
            broker.Bio = request.Bio;
            broker.LastUpdateDate = DateTime.Now;
            _dataContext.Update(broker);
            await _dataContext.SaveChangesAsync();
        }

        private async Task<bool> ExistBroker(int brokerId) =>
            await _dataContext.Brokers.AnyAsync(b => b.Id == brokerId);
    }
}
