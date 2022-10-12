


using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using SharedInterfaces.Ilogging;
using SharedInterfaces.IService;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Ioc
{
    public static class IocContainer
    {
        public static void ConfigureIOC(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionStrings connectionStrings = new ConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            services.AddDbContextPool<PTPPDBContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("PrimaryDatabaseConnectionString"))
                );


            services.AddTransient<ILogging, NLogging>();
            services.AddTransient<ITitle, TitleService>();
        }
    }
}
