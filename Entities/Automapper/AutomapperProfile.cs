using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Automapper
{
    class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Flight, FlightDataTransferObject>();
            CreateMap<FlightDataTransferObject, Flight>();
        }
        //Install-Package AutoMapper
        //Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
    }
}
