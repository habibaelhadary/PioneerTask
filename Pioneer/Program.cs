using Application_Layer.Services.EmpServices;
using Application_Layer.Services.Propert;
using Domin.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repository.EmpRepositor;
using Infrastructure.Repository.PropertyRepositor;
using Microsoft.EntityFrameworkCore;

namespace Pioneer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PioneerManagementDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Myconnection")));


            builder.Services.AddScoped<IPropReprositry, ProRepository>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
