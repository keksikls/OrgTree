using AutoMapper;
using OrganizationTree.Application.Departments.Commands;
using OrganizationTree.Application.DTO;
using OrganizationTree.Domain.Entity;
using OrganizationTree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Common.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>()
                .ForMember(u => u.ParentName, opt => opt.MapFrom(src => src.Parent!.Name));

            CreateMap<CreateDepartmentCommand, Department>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Status.Active))
            .ForMember(dest => dest.Children, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore()); 
        }
    }
}
