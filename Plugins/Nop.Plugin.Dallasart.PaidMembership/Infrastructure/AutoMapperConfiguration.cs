using AutoMapper;
using DallasArt.Models.Models;

namespace Nop.Plugin.DallasArt.PaidMembership.Infrastructure
{
   public  class   AutoMapperConfiguration
    {

        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;

        /// <summary>
        /// Initialize mapper
        /// </summary>
        public void Init()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PaaMember , IRegisterModel>();

            });
            _mapper = _mapperConfiguration.CreateMapper();
        }


        public  IMapper Mapper => _mapper;

        public static MapperConfiguration MapperConfiguration => _mapperConfiguration;
    }
}
