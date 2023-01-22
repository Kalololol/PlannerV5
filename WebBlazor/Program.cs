using Application;
using Data.Context;
using Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System.Reflection;
using WebBlazor.AutoMapperWebBlazor;
using WebBlazor.ServiceBlazor;

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
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();
