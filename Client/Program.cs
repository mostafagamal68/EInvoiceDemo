using Blazored.Toast;
using EInvoiceDemo.Client;
using EInvoiceDemo.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(c => new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
//builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped<IModalService, ModalService>();
builder.Services.AddScoped<LoaderService>();
builder.Services.AddScoped<HeaderService>();
await builder.Build().RunAsync();
