namespace Manage.Offers.Services
{
    using Manage.Offers.Data.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IParcelService
    {
        Task<int> AddParcel(Parcel parcel);
        Task<List<Parcel>> GetParcels();
    }
}
