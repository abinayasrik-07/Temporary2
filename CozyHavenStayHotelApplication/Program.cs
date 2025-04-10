
using AspNetCoreRateLimit;
using CozyHavenStayHotelApplication.Contexts;
using CozyHavenStayHotelApplication.Interfaces;
using CozyHavenStayHotelApplication.Misc;
using CozyHavenStayHotelApplication.Models;
using CozyHavenStayHotelApplication.Repositories;
using CozyHavenStayHotelApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CozyHavenStayHotelApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer ",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
            });
            });
            //builder.Logging.AddLog4Net();

            #region Contexts
            builder.Services.AddDbContext<CozyHavenStayHotelContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Guest>, GuestRepository>();
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Admin>, AdminRepository>();
            //builder.Services.AddScoped<IRepository<int, HotelOwner>, HotelOwnerRepository>();
            #endregion

            #region Mappers
            builder.Services.AddAutoMapper(typeof(Guest));
            //builder.Services.AddAutoMapper(typeof(GuestMapper));
            //builder.Services.AddAutoMapper(typeof(AdminMapper));
            #endregion

            //#region CustomFilters
            //builder.Services.AddScoped<CustomExceptionFilter>();
            //#endregion

            #region Services
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IGuestService, GuestService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            #endregion

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:JwtToken"]))
                    };
                });
            #endregion

            #region RateLimiting
            builder.Services.AddMemoryCache();
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIpRateLimiting();
            app.MapControllers();

            app.Run();
        }
    }
}

