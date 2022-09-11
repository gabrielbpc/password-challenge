using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using User.Domain.Configurations;
using User.Domain.Constants;
using User.IoC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var entryAssembly = Assembly.GetEntryAssembly();
builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{entryAssembly.GetName().Name}.xml");
    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "sys-users",
        Description = "Aplicação que gerencia os usuarios da aplicação",
        Contact = new OpenApiContact()
        {
            Name = "Gabriel",
            Email = "gabriel@invalid.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = AuthConstants.AUTHORIZATION_NAME,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        In = ParameterLocation.Header,
        Description = AuthConstants.Jwt.SWAGGER_DESCRIPTION
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

});
builder.Services.AddSwaggerExamplesFromAssemblies(entryAssembly);
_ = entryAssembly;

var secretConfig = builder.Configuration.GetSection("JwtSecret").Get<SecretSettings>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, bearerOptions =>
{
    bearerOptions.IncludeErrorDetails = true;
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretConfig.Secret));
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = securityKey,
        RequireSignedTokens = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});
builder.Services.Configure<SecretSettings>(builder.Configuration.GetSection("JwtSecret"));
builder.Services.AddApplicationLayerDependencies();
builder.Services.AddRepositoryLayerDependencies();
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection(nameof(MongoSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
