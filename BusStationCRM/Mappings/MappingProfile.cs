using AutoMapper;
using System;
using BusStationCRM.BLL.Models;
using BusStationCRM.BLL.Models.Search;
using BusStationCRM.Models;

namespace CampusCRM.MVC.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusStop, BusStopModel>().ReverseMap();
            CreateMap<Voyage, VoyageModel>().ForMember(model => model.BusStopArrival,
                opts => opts.MapFrom(source => source.BusStopArrival))
                .ForMember(model => model.BusStopDeparture,
                    opts => opts.MapFrom(source => source.BusStopDeparture))
                .ReverseMap();
            CreateMap<Order, OrderModel>()
                .ForMember(model => model.Voyage,
                    opts => opts.MapFrom(source => source.Voyage))
                .ReverseMap();
            CreateMap<Ticket, TicketModel>().ReverseMap();
            CreateMap<VoyageFilterModel, VoyageFilter>().ReverseMap();
        }
    }
}
