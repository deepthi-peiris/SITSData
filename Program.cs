
using EntityCore.Data;
using EntityCore.Interfaces;
using EntityCore.Interfaces.DataImports;
using EntityCore.Repos;
using EntityCore.Repos.DataImports;
using EntityCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using EntityCore.Services;
using SITSData.Data;
using SITSData.Repositories;
using SITSData.Interfaces;

namespace SITSData
{
    class Program
    {
        static void Main(string[] args)
        {
            //This requires Microsoft.Extensions.DependencyInjections
            var services = new ServiceCollection();
            ConfigureServices(services);
            using (ServiceProvider serviceProvider =
                                   services.BuildServiceProvider())
            {
                BaseApplication app = serviceProvider.GetService<BaseApplication>();
                app.Run(args);
            }

        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<BaseApplication>()
                    .AddDbContext<LocalDbContext>()
                    .AddDbContext<EntityDbContext>()
                    .AddDbContext<ExamsDbContext>()
                    .AddDbContext<TestDbContext>()
                    .AddSingleton<IAppDetails, AdminApp>()
                    .AddTransient<IEmailSender, EmailSender>()
                    .AddTransient<IModuleRepo, ModuleRepo>()
                    .AddTransient<IModuleMarkChangeLogRepo, ModuleMarkChangeLogRepo>()
                    .AddTransient<ICMPMarksImportRepo, CMPMarksImportRepo>()
                    .AddTransient<IModuleMarkRepo, ModuleMarkRepo>()
                    //Test cases
                    .AddTransient<IStudentRepo, StudentRepo>()
                    ;
        }
    }
}
