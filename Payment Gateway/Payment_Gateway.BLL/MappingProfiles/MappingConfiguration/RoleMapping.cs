using AutoMapper;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Requests;
using Payment_Gateway.Shared.DataTransferObjects.Responses;

namespace Payment_Gateway.BLL.Configurations.MappingConfiguration
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<RoleDto, ApplicationRole>();
            //CreateMap<RoleDto, ApplicationRole>()
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<ApplicationRole, RoleDto>();
            CreateMap<ApplicationRole, RoleResponse>();
        }
    }
}
