global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using Application;
using Data.Context;
using Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using System.Reflection;
using System.Text;
using WebBlazor;
using WebBlazor.AutoMapperWebBlazor;
using WebBlazor.ServiceBlazor;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<AppDbContext>(option => 
        option.UseSqlServer(builder.Configuration.GetConnectionString("PlannerV5ConnectionStrings"))
    );

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddMediatR(typeof(MediatorConfiguration).GetTypeInfo().Assembly);
builder.Services.AddSingleton(AutoMapperConfigurationWebBlazor.Initialize());

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7205/") });

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IAccountService, AccountService>();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();



var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accesToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accesToken) && path.StartsWithSegments("/Hubs"))
            {
                context.Token = accesToken;
            }
            return Task.CompletedTask;
        }
    };
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtIssuer)),
    };
});

/*builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("HasRole", builder => builder.RequireClaim("RoleId", "2"));
});*/



//builder.Services.AddHttpClient<IEmployeeService, EmployeeService>
//    (client =>
//    {
//        client.BaseAddress = new Uri(("https://localhost:7205/"));
//    });
//builder.Services.AddHttpClient<IRequestService, RequestService>
//    (client =>
//    {
//        client.BaseAddress = new Uri(("https://localhost:7205/"));
//    });

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();
