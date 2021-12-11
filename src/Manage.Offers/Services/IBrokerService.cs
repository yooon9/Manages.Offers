namespace Manage.Offers.Services
{
    using Manage.Offers.Helpers;
    using Manage.Offers.Models;
    using System.Threading.Tasks;

    public interface IBrokerService
    {
        Task<BrokerResponseModel> GetBrokerById(int brokerId);
        Task<PagedList<BrokerResponseModel>> GetBrokers(SearchBrokerModel search = null, int pageNumber = 1, int pageSize = 25);
        Task<int> AddBroker(BrokerRequestModel request);
        Task UpdateBroker(int brokerId, BrokerRequestModel request);
    }
}
