using AutoMapper;
using Business.Abstract;
using Business.AutoMapper;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolver
{
    
    public static class Serviceregistration
    {
        public static void Create(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            // AddScoped, AddSingleton, AddTransiet
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDAL, EFProductDAL>();

            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDAL, EFOrderDAL>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDAL, EFUserDAL>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
