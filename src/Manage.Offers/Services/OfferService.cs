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

    public class OfferService : IOfferService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public OfferService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<OfferResponseModel> GetOfferById(int offerId)
        {
            return _mapper.Map<OfferResponseModel>(await GetOfferEntityById(offerId));
        }

        public async Task<PagedList<OfferResponseModel>> GetOffers(SearchOfferModel search = null, int pageNumber = 1, int pageSize = 25)
        {
            var offers = _dataContext.Offers.Include(a => a.Broker).AsNoTracking();

            if (search is null)
            {
                var pagedListWithoutSearch = await PagedList<Offer>.CreateAsync(offers.OrderByDescending(a => a.CreationDate), pageNumber, pageSize);
                return new PagedList<OfferResponseModel>(_mapper.Map<List<OfferResponseModel>>(pagedListWithoutSearch.ToList()), pagedListWithoutSearch.Count, pagedListWithoutSearch.CurrentPage, pagedListWithoutSearch.PageSize);
            }


            if (!string.IsNullOrEmpty(search.Title))
                offers = offers.Where(a => a.Title.Contains(search.Title));

            if (search.BrokerId != null)
                offers = offers.Where(a => a.BrokerId == search.BrokerId);

            if (search.ParcelId != null)
                offers = offers.Where(a => a.ParcelId == search.ParcelId);

            if (!string.IsNullOrEmpty(search.BrokerName))
            {
                offers = offers.Where(a => a.Broker.Name.Contains(search.BrokerName));
            }

            if (search.BlockNumber != null)
            {
                offers = offers.Include(a => a.Parcel).AsNoTracking();
                offers = offers.Where(a => a.Parcel.BlockNumber == search.BlockNumber);
            }

            var pagedList = await PagedList<Offer>.CreateAsync(offers.OrderByDescending(a => a.CreationDate), pageNumber, pageSize);
            return new PagedList<OfferResponseModel>(_mapper.Map<List<OfferResponseModel>>(pagedList.ToList()), pagedList.Count, pagedList.CurrentPage, pagedList.PageSize);
        }

        public async Task<int> AddOffer(OfferRequestModel request)
        {
            try
            {
                var offer = _mapper.Map<Offer>(request);
                offer.CreationDate = DateTime.Now;
                offer.LastUpdateDate = DateTime.Now;
                await _dataContext.AddAsync(offer);
                await _dataContext.SaveChangesAsync();
                return offer.Id;
            }
            catch (Exception ex)
            {
                if (ex.Source == "Microsoft.EntityFrameworkCore.Relational")
                    throw new ForeignKeyException("Plase insert correct data, BrokerId or ParcelId is incorrect");
                throw;
            }
        }

        public async Task UpdateOffer(int offerId, OfferRequestModel request)
        {
            try
            {
                var offer = await GetOfferEntityById(offerId);
                offer.Title = request.Title;
                offer.Description = request.Description;
                offer.PricePerMeter = request.PricePerMeter;
                offer.BrokerId = request.BrokerId;
                offer.ParcelId = request.ParcelId;
                offer.LastUpdateDate = DateTime.Now;
                _dataContext.Update(offer);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.Source == "Microsoft.EntityFrameworkCore.Relational")
                    throw new ForeignKeyException("Plase insert correct data, BrokerId or ParcelId is incorrect");
                throw;
            }
        }

        public async Task DeleteOffer(int offerId)
        {
            var offer = await GetOfferEntityById(offerId);
            _dataContext.Remove(offer);
            await _dataContext.SaveChangesAsync();
        }

        private async Task<Offer> GetOfferEntityById(int offerId)
        {
            if (!await ExistOffer(offerId))
                throw new OfferNotFoundException(offerId, "Offer not found");
            return await _dataContext.Offers.FirstOrDefaultAsync(b => b.Id == offerId);
        }
        private async Task<bool> ExistOffer(int offerId) =>
            await _dataContext.Offers.AnyAsync(b => b.Id == offerId);
    }
}
