using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using KeyCloakAuth.Handler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace KeyCloakAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var Config = builder.Configuration;
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            services.AddKeycloakAuthentication(Config);
            services.AddAuthorization();
            services.AddKeycloakAuthorization(Config);
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("KeycloakRolePolicy", policy =>
            //        policy.Requirements.Add(new KeycloakRoleRequirement("admin")));
            //});

            // Register the handler
            //services.AddSingleton<IAuthorizationHandler, KeycloakRoleHandler>();


            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
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

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.MapGet("/", (ClaimsPrincipal user) =>
            {
                app.Logger.LogInformation(user.Identity.Name);
            }).RequireAuthorization("IsAdmin");


            app.Run();
        }
    }
}
