using Microsoft.EntityFrameworkCore;
using Sporting.Data;
using Sporting.Repository.Implementation;
using Sporting.Repository.Interfacees;

namespace Sporting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            
            builder.Services.AddScoped<ILeagueServices, LeagueServices>();
            builder.Services.AddScoped<ITeamServices, TeamServices>();
            builder.Services.AddHttpClient<FootballApiService>();

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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



