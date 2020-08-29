using AmplifiersAPI.Models;
using AmplifiersAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AmplifiersAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<AmplifiersDatabaseSettings>(
                Configuration.GetSection(nameof(AmplifiersDatabaseSettings)));

            services.AddSingleton<IAmplifiersDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AmplifiersDatabaseSettings>>().Value);

            services.Configure<AppsSettings>(
                Configuration.GetSection(nameof(AppsSettings)));

            services.AddSingleton<IAppsSettings>(sp =>
                sp.GetRequiredService<IOptions<AppsSettings>>().Value);

            services.AddSingleton<UserService>();

            services.AddSingleton<AmplifierService>();

            services.AddControllers();

            //JWT          
            var appSetting = Configuration.GetSection(nameof(AppsSettings)).Get<AppsSettings>();
            var Key = Encoding.ASCII.GetBytes(appSetting.Secret);
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer( d =>
                {
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true;
                    d.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
