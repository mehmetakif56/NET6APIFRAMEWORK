using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TTBS.Core.Interfaces;
using TTBS.Helper;
using TTBS.Infrastructure;
using TTBS.Services;

var builder = WebApplication.CreateBuilder(args);
;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TTBSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});


builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDonemService, DonemService>();
builder.Services.AddSingleton(typeof(GenericSharedResourceService));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {

                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = builder.Configuration.GetValue<string>("JWT:ValidAudience"),
                            ValidIssuer = builder.Configuration.GetValue<string>("JWT:ValidIssuer"),
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Secret")))
                        };
            
                });

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
