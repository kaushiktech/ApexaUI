using ApexApi.Data;
using ApexApi.Data.Data;
using ApexApi.Data.Repository;
using ApexApi.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseInMemoryDatabase(databaseName: "ApexaDB");
}
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidIssuer = "localhost",//Would be replaced with valid issuer in production
         ValidateAudience = false,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["APEXA_JWT_KEY"]))
     };
 });
//Added Global Filter for rate limiting with a failure code of 429, these values making it conservative, but could be changed per requirement
//This should prevent DDOS attacks from a certain ip.
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = 429;
    rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.CreateChained(
       PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
           RateLimitPartition.GetFixedWindowLimiter(httpContext.Connection.RemoteIpAddress.ToString(), partition =>
               new FixedWindowRateLimiterOptions
               {
                   AutoReplenishment = true,
                   PermitLimit = 10,
                   Window = TimeSpan.FromSeconds(10)
               })));
});
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
});
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            policyBuilder => policyBuilder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .SetIsOriginAllowed(_ => true)
        );
    });
}


builder.Services.AddScoped<IAdvisorRepository, AdvisorRepository>();
builder.Services.AddScoped<AdminInitializer>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Apexa Advisors API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
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


var app = builder.Build();
app.UseRateLimiter();
//Initializing/seeding a admin user for test purposes
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var adminInit = services.GetRequiredService<AdminInitializer>();
    var appContext = services.GetRequiredService<ApplicationDbContext>();
    bool doSeedData = false;
    bool.TryParse(app.Configuration["SeedData"]?.ToString(), out doSeedData);
    //Seed Data
    if (doSeedData)
    {
        DataGenerator.Initialize(services);
        app.Logger.Log(LogLevel.Information, "Database seeded");
    }
    //Seed 'admin' username - Remember to add environment variable APEXA_ADMIN_PASSWORD 
    adminInit.RegisterAdmin();
    app.Logger.Log(LogLevel.Information, "Database admin added.");
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAllOrigins");
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
