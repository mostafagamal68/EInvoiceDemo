using EInvoiceDemo.Server;
using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Services;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

services.AddDbContext<EInvoiceContext>(
    op => op.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddExceptionHandler<ExceptionHandler>();
services.AddProblemDetails();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
