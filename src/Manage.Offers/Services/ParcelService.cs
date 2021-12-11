namespace Manage.Offers.Services
{
    using Manage.Offers.Data.Context;
    using Manage.Offers.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ParcelService : IParcelService
    {
        private readonly DataContext _dataContext;

        public ParcelService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Parcel>> GetParcels()
        {
            return await _dataContext.Parcels.ToListAsync();
        }

        public async Task<int> AddParcel(Parcel parcel)
        {
            parcel.CreationDate = DateTime.Now;
            await _dataContext.AddAsync(parcel);
            await _dataContext.SaveChangesAsync();
            return parcel.Id;
        }
    }
}
