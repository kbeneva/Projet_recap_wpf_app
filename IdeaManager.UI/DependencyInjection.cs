using IdeaManager.UI.ViewModels;
using IdeaManager.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace IdeaManager.UI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            // Vue principale
            services.AddSingleton<MainWindow>();

            // Vues
            services.AddSingleton<DashboardView>();
            services.AddTransient<IdeaFormView>();
            services.AddTransient<IdeaListView>();

            // ViewModels
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<IdeaFormViewModel>();
            services.AddTransient<IdeaListViewModel>();
            services.AddTransient<ProjectListViewModel>();

            return services;
        }
    }
}
