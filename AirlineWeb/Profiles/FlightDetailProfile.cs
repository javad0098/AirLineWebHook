using AirlineWeb.Dtos;
using AirlineWeb.Models;

using AutoMapper;

namespace AirlineWeb.Profiles
{
    public class FlightDetailProfile : Profile
    {
        public FlightDetailProfile()
        {
            this.CreateMap<FlightDetail, FlightDetailReadDto>();
            this.CreateMap<FlightDetailCreateDto, FlightDetail>();
            this.CreateMap<FlightDetailUpdateDto, FlightDetail>();
        }
    }
}
