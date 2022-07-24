using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Infrastructure.Entities.WeatherForecast, Infrastructure.Dto.WeatherForecast.WeatherForecastResponseViewModel>();
            CreateMap<Infrastructure.Dto.WeatherForecast.WeatherForecastRequestViewModel, Infrastructure.Entities.WeatherForecast>();

            //SmsService
            CreateMap<Infrastructure.Entities.SmsServiceEntity, Infrastructure.Dto.SmsService.SmsServiceGetResponseDTO>();
            CreateMap<Infrastructure.Dto.SmsService.SmsServiceCreateDTO, Infrastructure.Entities.SmsServiceEntity>();
        }
    }
}
