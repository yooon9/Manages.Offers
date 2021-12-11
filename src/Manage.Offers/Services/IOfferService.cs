namespace Manage.Offers.Services
{
    using Manage.Offers.Helpers;
    using Manage.Offers.Models;
    using System.Threading.Tasks;

    public interface IOfferService
    {
        Task<OfferResponseModel> GetOfferById(int offerId);
        Task<PagedList<OfferResponseModel>> GetOffers(SearchOfferModel search = null, int pageNumber = 1, int pageSize = 25);
        Task<int> AddOffer(OfferRequestModel request);
        Task UpdateOffer(int offerId, OfferRequestModel request);
        Task DeleteOffer(int offerId);
    }
}
