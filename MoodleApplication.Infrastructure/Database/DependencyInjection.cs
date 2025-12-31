using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoodleApplication.Domain.Persistence.Chats;
using MoodleApplication.Domain.Persistence.Courses;
using MoodleApplication.Domain.Persistence.Users;
using MoodleApplication.Infrastructure.Persistence.Chats;
using MoodleApplication.Infrastructure.Persistence.Users;

namespace MoodleApplication.Infrastructure.Database
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDatabase(services, configuration);
            AddRepositories(services);

            return services;
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

             services.AddHttpClient();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IChatRoomRepository, ChatRepository>();
            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
        }
    }
}
