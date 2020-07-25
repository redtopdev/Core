using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Text.Json.Serialization;

namespace Engaze.Core.Web
{
    public abstract class EngazeStartup
    {
        public EngazeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); 

            ConfigureComponentServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app)
        {           

            app.UseRouting();            
            //app.UseAuthorization();
            app.UseCorrelationHeader();
            app.UseRequestResponseLogging();
            app.UseSerilogRequestLogging();
            app.UseAppException();
            app.UseAppStatus();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureComponent(app);

            RemoteConfiguration.Initialize(this.Configuration, true);            
        }
        public abstract void ConfigureComponentServices(IServiceCollection services);

        public abstract void ConfigureComponent(IApplicationBuilder app);

    }
}
