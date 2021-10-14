using AutoMapper;
using System;
using BusStationCRM.BLL.Models;
using BusStationCRM.Models;

namespace CampusCRM.MVC.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, TicketModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<BusStop, BusStopModel>().ReverseMap();
            CreateMap<Voyage, VoyageModel>().ReverseMap();

        }
    }
}
