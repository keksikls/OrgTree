using Microsoft.EntityFrameworkCore;
using OrganizationTree.Application.Common.Mappings;
using OrganizationTree.Application.Departments.Queries;
using OrganizationTree.Application.Interfaces;
using OrganizationTree.Domain.Factories;
using OrganizationTree.Domain.Interfaces;
using OrganizationTree.Domain.Services;
using OrganizationTree.Infrastructure.Data;
using OrganizationTree.Infrastructure.Repositories;
using OrganizationTree.Infrastructure.Services;
using OrganizationTree.Web.Controllers;

namespace OrganizationTree.Web.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Department")));

            return builder;
        }

        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder) 
        {
            // Application
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssembly(typeof(GetDepartmentByIdQuery).Assembly));

            // Domain
            builder.Services.AddScoped<IDepartmentFactory, DepartmentFactory>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            // Infrastructure
            builder.Services.AddScoped<IOrderNumberGenerator, OrderNumberGenerator>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentXmlService, DepartmentXmlService>();

            //web
            builder.Services.AddScoped<DepartmentController>();

            return builder;
        }
    }
}
