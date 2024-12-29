using Home_Heart.Application.Contracts;
using Home_Heart.Application.Services;
using Home_Heart.Identity;
using Home_Heart.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<HomeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("homeContext")));

builder.Services.AddDbContextPool<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("homeContext")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<ICompanyAppService, CompanyAppService>();
builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;

})
               .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

var allowedOrigins = builder.Environment.IsDevelopment() ? "http://localhost:5102" : "https://your-production-url.com";
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
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
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' followed by your token in the Authorization header. Example: Bearer {eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSG9tZV9IZWFydF8yMDI1IiwianRpIjoiMmVmZjI4NWQtZWJiMy00YTQ0LTlmNzEtMGFiNTJiZTM0MWY2IiwiZXhwIjoxNzM1NDU1ODEwLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUxOTkiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUxOTkifQ.uvxpZY3UmN3tW-lT0ZiqGb1ZL7KFkWT5hPZVqyKdRw4}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                    Scheme = "Bearer",
                    Name = "Bearer",
                    In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = "An unexpected error occurred. Please try again later."
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        });
    });

    app.UseHsts();
}

app.UseHttpsRedirection();

// Security Headers Middleware
app.Use(async (context, next) =>
{
    //context.Response.Headers.Add("Content-Security-Policy",
    //    "default-src 'self'; script-src 'self' https://cdnjs.cloudflare.com; style-src 'self' https://cdnjs.cloudflare.com; img-src 'self' data:; connect-src 'self';");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

    await next();
});
 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
