namespace Manage.Offers.Mappers
{
    using AutoMapper;
    using Manage.Offers.Data.Entities;
    using Manage.Offers.Models;

    public class ModelsToEntitiesMapper : Profile
    {
        public ModelsToEntitiesMapper()
        {
            #region Broker mappers
            CreateMap<Broker, BrokerResponseModel>();
            CreateMap<BrokerRequestModel, Broker>();
            #endregion

            #region Offer mappers
            CreateMap<Offer, OfferResponseModel>().ForMember(dest => dest.BrokerName, opt =>
            {
                opt.MapFrom(src => src.Broker.Name);
            });

            CreateMap<OfferRequestModel, Offer>();
            #endregion
        }
    }
}
