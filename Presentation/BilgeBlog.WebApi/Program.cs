using BilgeBlog.Application.DependencyResolvers;
using BilgeBlog.Persistence.DependencyResolvers;
using BilgeBlog.WebApi.DependencyResolvers;
using BilgeBlog.WebApi.Middleware;
using BilgeBlog.WebApi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using AspNetCoreRateLimit;
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

            builder.Services.AddScoped<BilgeBlog.Application.Contracts.ITokenService, TokenService>();

            // CORS
            var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins(corsOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Health Checks
            builder.Services.AddHealthChecks();

            // Rate Limiting
            builder.Services.AddMemoryCache();
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(typeof(BilgeBlog.Application.Validators.PostValidators.CreatePostCommandValidator).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(BilgeBlog.WebApi.Validators.PostValidators.CreatePostRequestValidator).Assembly);

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
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "BilgeBlog API",
                    Version = "v1",
                    Description = "BilgeBlog Web API Documentation"
                });

                // JWT Authentication için Swagger yapılandırması
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Rate Limiting
            app.UseIpRateLimiting();

            app.UseHttpsRedirection();

            // CORS
            app.UseCors("AllowSpecificOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            // Health Checks
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(x => new
                        {
                            name = x.Key,
                            status = x.Value.Status.ToString(),
                            exception = x.Value.Exception?.Message,
                            duration = x.Value.Duration.ToString()
                        })
                    });
                    await context.Response.WriteAsync(result);
                }
            });

            app.MapControllers();

            app.Run();
        }
    }
}
