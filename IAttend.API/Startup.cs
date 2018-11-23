using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IAttend.API.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace IAttend.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Seed>();
            // services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));
            services.AddSignalR(option => option.EnableDetailedErrors = true);
            //services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddScoped<IStudentRepository,StudentRepositoryMSQl>();
            services.AddScoped<IInstructorRepository,InstructorRepositoryMSql>();
            services.AddScoped<IScheduleRepository,ScheduleRepositoryMSql>();
            services.AddScoped<IAttendanceRepository,AttendanceRepositoryMSql>();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // seeder.SeedStudent();
            // seeder.SeedContactPerson();
            // seeder.SeedInstructor();
            // seeder.SeedSchedule();

            app.UseHttpsRedirection();
            app.UseCors(s => s.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notifier");
            });

            app.UseMvc();
        }
    }
}
