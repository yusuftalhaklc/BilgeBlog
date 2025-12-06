using BilgeBlog.Application.DependencyResolvers;
using BilgeBlog.Persistence.DependencyResolvers;
using BilgeBlog.WebApi.DependencyResolvers;
using BilgeBlog.WebApi.Middleware;
using BilgeBlog.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BilgeBlog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContextService();
            builder.Services.AddRepositoryService();
            builder.Services.AddAutoMapperService();
            builder.Services.AddWebApiAutoMapperService();
            builder.Services.AddMediatRService();

            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found")))
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
