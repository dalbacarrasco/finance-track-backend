using FinTrackAPI.Repositories;
using FinTrackAPI.Repositories.Interfaces;
using FinTrackAPI.Services;
using FinTrackAPI.Services.Interfaces;

namespace FinTrackAPI.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            // Repositorios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Servicios
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IDashboardService, DashboardService>();

            return services;
        }
    }
}