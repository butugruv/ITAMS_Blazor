using BlazorApp.Areas.Identity;
using BlazorApp.Data;
using BlazorApp.Services;
using ITAMS_DAL.Data;
using ITAMS_DAL.DataAccess;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            //builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Default"
            });

            builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddScoped<IDeviceData, DeviceData>();
            builder.Services.AddScoped<IDeviceTypeData, DeviceTypeData>();
            builder.Services.AddScoped<IDeviceIpData, DeviceIpData>();
            builder.Services.AddScoped<IRmfPackageData, RmfPackageData>();
            builder.Services.AddScoped<ILocationData, LocationData>();
            builder.Services.AddScoped<INetworkData, NetworkData>();
            builder.Services.AddScoped<IGridStateData, GridStateData>();
            builder.Services.AddSingleton<BannerService>();


            builder.Services.Configure<HubOptions>(options =>
            {
                // 10 MB size limit
                options.MaximumReceiveMessageSize = 10 * 1024 * 1024;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}