using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TranDeviceManager.Services;
using TranDeviceManager.ViewModels;
using TranDeviceManager.Views;

namespace TranDeviceManager
{
    public partial class App : Application
    {
        // Déclarez ServiceProvider comme une propriété statique pour un accès global
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Enregistrez le TranApiHelper et autres services
            services.AddSingleton<TranApiHelper>(new TranApiHelper("http://localhost:5000"));

            // Enregistrez les ViewModels
            services.AddSingleton<AutoDetectAndOpenViewModel>();
            services.AddSingleton<TestRecordManagementViewModel>();
            services.AddSingleton<SendCommandViewModel>();

            // Enregistrez les Views
            services.AddTransient<AutoDetectAndOpenView>();
            services.AddTransient<TestRecordManagementView>();
            services.AddTransient<SendCommandView>();

            // Enregistrez la MainWindow
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Récupérez la MainWindow du ServiceProvider et affichez-la
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
