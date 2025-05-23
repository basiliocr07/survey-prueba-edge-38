
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using MediatR;
using SurveyApp.Domain.Repositories;
using SurveyApp.Domain.Services;
using SurveyApp.Infrastructure.Repositories;
using SurveyApp.Infrastructure.Services;
using SurveyApp.Application.Customers.Queries.GetAllCustomers;
using SurveyApp.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

// Registrar MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(GetAllCustomersQuery).Assembly);
});

// Registrar servicios de infraestructura
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Registrar servicios de email
builder.Services.AddScoped<IEmailService, MailKitEmailService>();

// Agregar servicios de aplicación
builder.Services.AddApplicationServices();

// Agregar las conexiones a la base de datos
builder.Services.AddDbContext<SurveyApp.Infrastructure.Data.ApplicationDbContext>();

var app = builder.Build();

// Configurar pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
