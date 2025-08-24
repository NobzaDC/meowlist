using System.Text;
using MeowCore.Data;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using MeowCore.Service;
using MeowCore.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using MeowCore.Controllers;
using MeowCore.Helpers.Interfaces;
using MeowCore.Helpers;

namespace MeowCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            const string defaultLogPathFile = "C:\\whiskerWatch\\Logs\\Index.txt";
            const string _mCors = "prrr..._what_did_u_spected?_a_serious_cors_id?";

            //Logger
            builder.Host.UseSerilog((hostContext, services, configuration) => {
                configuration
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                    .WriteTo.File(builder.Configuration.GetSection("AppSettings")["LogPathFile"] ?? defaultLogPathFile, rollingInterval: RollingInterval.Day);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: _mCors, builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            //Configure authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is not configured in appsettings.json"))
                    )
                };
            });

            builder.Services.AddAuthorization();


            // Configure Database
            builder.Services.AddDbContext<MeowDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PawnectionString")));


            // Repositories & Services & Helpers DI
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IListsRepository, ListsRepository>();
            builder.Services.AddScoped<ITodosRepository, TodosRepository>();
            builder.Services.AddScoped<ITagsRepository, TagsRepository>();

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IListsService, ListsService>();
            builder.Services.AddScoped<ITodosService, TodosService>();
            builder.Services.AddScoped<ITagsService, TagsService>();

            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle   
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Must be the first middleware
            app.UseMiddleware<MeowCore.Middlewares.ErrorLoggingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
