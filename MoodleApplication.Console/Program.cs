// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoodleApplication.Application.Users.User;
using MoodleApplication.Infrastructure.Database;

Console.WriteLine("Hello, World!");
var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CreateUserRequestHandler>();
builder.Build();


