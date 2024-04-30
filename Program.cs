using ecomove_back.Data;
using ecomove_back.Data.Models;
using ecomove_back.Interfaces.IRepositories;
using ecomove_back.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Serivce to call external api (Nominatim open street map)
builder.Services.AddHttpClient();
builder.Services.AddScoped<OpenStreetMapHttpRequest>();

builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMotorizationRepository, MotorizationRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<ICarpoolAddressRepository, CarpoolAddressRepository>();
builder.Services.AddScoped<ICarpoolAnnouncementRepository, CarpoolAnnouncementRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecomove Web API", Version = "v1.0.0" });
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    option.OperationFilter<SecurityRequirementsOperationFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddDbContext<EcoMoveDbContext>(dbContextOptionsBuilder =>
{
    dbContextOptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DbString"));
});

// Identity Framework
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<AppUser>(c =>
{
    //c.Password
}) // config mdp ...
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EcoMoveDbContext>();


var app = builder.Build();

app.MapIdentityApi<AppUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();