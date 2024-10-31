using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Ecomove.Api.Data;
using Ecomove.Api.Data.Fixtures;
using Ecomove.Api.Data.Models;
using Ecomove.Api.Interfaces.IRepositories;
using Ecomove.Api.Repositories;
using Ecomove.Api.Services.Brands;
using Ecomove.Api.Services.Categories;
using Ecomove.Api.Services.Models;
using Ecomove.Api.Services.Motorizations;
using Ecomove.Api.Services.RentalVehicles;
using Ecomove.Api.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMotorizationRepository, MotorizationRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<ICarpoolAddressRepository, CarpoolAddressRepository>();
builder.Services.AddScoped<ICarpoolBookingRepository, CarpoolBookingRepository>();
builder.Services.AddScoped<ICarpoolAnnouncementRepository, CarpoolAnnouncementRepository>();
builder.Services.AddScoped<IRentalVehicleRepository, RentalVehicleRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRentalVehicleService, RentalVehicleService>();
builder.Services.AddScoped<IMotorizationService, MotorizationService>();
builder.Services.AddScoped<IModelService, ModelService>();

// Configurer OpenStreetMap
builder.Services.AddScoped<OpenStreetMapHttpRequest>();

// JSON et Swagger
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecomove Web API", Version = "v1.0.0" });
    option.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
        }
    );

    option.OperationFilter<SecurityRequirementsOperationFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);

    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        }
    );
});

// Base de données et authentification
builder.Services.AddDbContext<EcoMoveDbContext>();

builder.Services.AddAuthorization();
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<EcoMoveDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:4200", "http://localhost:4200");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.WithHeaders("*");
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
    };
});

// Construction de l'application
var app = builder.Build();

// Middleware et routage
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seeding de données initiales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EcoMoveDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    UsersFixtures.SeedRole(context);
    UsersFixtures.SeedAdminUser(userManager);
    UsersFixtures.SeedUser(userManager);
}

// Exécute l'application
app.Run();