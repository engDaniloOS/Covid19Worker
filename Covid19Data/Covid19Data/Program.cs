using System;
using Covid19Data.Domain.Repositories;
using Covid19Data.Domain.Services;
using Covid19Data.Infrastructure;
using Covid19Data.Infrastructure.Repositories;
using Covid19Data.Repositories;
using Covid19Data.Services;
using Covid19Data.Services.DataService;
using Covid19Data.Services.FileServices;
using Covid19Data.Services.MailServices;
using Covid19Data.Services.SerializerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Covid19Data
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    #region DataBase
                    services.AddDbContext<ApplicationContext>(
                        option => option.UseSqlServer(ConfigureConnectionString(hostContext)), ServiceLifetime.Singleton);
                    #endregion

                    #region Dependency Injection
                    services.AddTransient<IDataRepository, DataRepository>();
                    services.AddTransient<IDestinationEmailRepository, DestinationEmailRepository>();

                    services.AddTransient<IXmlService, XmlService>();
                    services.AddTransient<IJsonService, JsonService>();
                    services.AddTransient<IDataService, DataService>();
                    services.AddTransient<IBusinessService, BusinessService>();
                    services.AddTransient<ISendEmailService, SendEmailService>();
                    services.AddTransient<IDataAccessService, DataAccessService>();
                    #endregion
                });

        private static string ConfigureConnectionString(HostBuilderContext hostContext) =>
            hostContext.Configuration
                        .GetConnectionString("Default")
                        .Replace("?path?", Environment.CurrentDirectory);

    }
}
