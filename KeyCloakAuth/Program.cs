using Keycloak.AuthServices.Authentication;
using KeyCloakAuthenticate;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace KeyCloakAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var Config = builder.Configuration;
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            services.AddKeycloakAuthentication(Config);


            services.AddSingleton<IKeycloakUserManagement, KeycloakUserManagement>();

            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                x.AddSecurityDefinition("Bearer ", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer "
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer "
                    },
                    Scheme = "oauth2",
                    Name = "Bearer ",

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
