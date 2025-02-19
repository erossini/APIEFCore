using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEFCore.Mapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Client, Domain.Client>()
                .ForMember(x => x.Channels, opt => opt.Ignore());
            CreateMap<Domain.Channel, Domain.Channel>();
        }
    }
}