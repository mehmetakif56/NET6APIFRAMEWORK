using AutoMapper;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using TTBS;
using TTBS.Core.Interfaces;
using TTBS.Extensions;
using TTBS.Helper;
using TTBS.Infrastructure;
using TTBS.MongoDB;
using TTBS.Services;

var builder = WebApplication.CreateBuilder(args);
;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TTBSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = builder.Configuration
        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
    options.Database = builder.Configuration
        .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
});
builder.Services.AddSingleton<IGorevAtamaGKMBusiness, GorevAtamaGKMBusiness>();
builder.Services.AddSingleton<IGorevAtamaKomMBusiness, GorevAtamaKomMBusiness>();
var culture = CultureInfo.CreateSpecificCulture("tr-TR");
var dateformat = new DateTimeFormatInfo
{
    ShortDatePattern = "dd/MM/yyyy",
    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
};
culture.DateTimeFormat = dateformat;

var supportedCultures = new[]
{
    culture
};



builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:4200",
                                                  "https://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});


builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGlobalService, GlobalService>();
builder.Services.AddSingleton(typeof(GenericSharedResourceService));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISessionHelper, SessionHelper>();
builder.Services.AddScoped<IStenografService, StenografService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IGorevAtamaService, GorevAtamaService>();

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
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuth();
;

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.DisplayRequestDuration());
    
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.UseSession();
app.UseContextUserSession();
app.MapControllers();

app.Run();
