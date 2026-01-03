using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoodleApplication.Application.Users.Admin;
using MoodleApplication.Application.Users.Chats;
using MoodleApplication.Application.Users.Courses;
using MoodleApplication.Application.Users.User;
using MoodleApplication.Console.Actions;
using MoodleApplication.Console.Views;
using MoodleApplication.Infrastructure.Database;

Console.WriteLine("Hello, World!");
var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<CreateUserRequestHandler>();
builder.Services.AddScoped<GetUserRequestHandler>();
builder.Services.AddScoped<GetUserCoursesRequestHandler>();
builder.Services.AddScoped<GetAllUsersRequestHandler>();
builder.Services.AddScoped<GetTeachingCoursesRequestHandler>();

builder.Services.AddScoped<GetCourseMaterialsRequestHandler>();
builder.Services.AddScoped<GetCourseAnnouncementsRequestHandler>();
builder.Services.AddScoped<GetCourseStudentsRequestHandler>();

builder.Services.AddScoped<GetUserChatRoomsRequestHandler>();
builder.Services.AddScoped<GetChatMessagesRequestHandler>();
builder.Services.AddScoped<SendMessageRequestHandler>();

builder.Services.AddScoped<GetUsersByRoleRequestHandler>();
builder.Services.AddScoped<DeleteUserRequestHandler>();
builder.Services.AddScoped<UpdateUserEmailRequestHandler>();
builder.Services.AddScoped<ChangeUserRoleRequestHandler>();

builder.Services.AddScoped<MenuManager>();
builder.Services.AddScoped<UserActions>();
builder.Services.AddScoped<CourseActions>();
builder.Services.AddScoped<ChatActions>();
builder.Services.AddScoped<AdminActions>();



var host = builder.Build();



using (var scope = host.Services.CreateScope())
{
    var menuManager = scope.ServiceProvider.GetRequiredService<MenuManager>();
    await menuManager.RunAsync();
}
