using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIEFCore.Domain;
using AutoMapper;

namespace APIEFCode.Domain.Mapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, Client>()
                .ForMember(x => x.Channels, opt => opt.Ignore());
            CreateMap<Channel, Channel>();
        }
    }
}