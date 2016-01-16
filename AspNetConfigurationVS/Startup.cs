namespace AspNetConfigurationVS
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.OptionsModel;

    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MySettings>(_configuration);
            services.Configure<OtherSettings>(_configuration.GetSection("otherSettings"));

            services.AddScoped<MyClass>();
        }

        public void Configure(
            IApplicationBuilder app,
            IOptions<MySettings> settingsAccessor,
            MyClass myClass)
        {
            var settings = settingsAccessor.Value;

            app.UseIISPlatformHandler();

            app.Run(async context =>
            {
                await context.Response.WriteAsync(
                    $"String Setting: {settings.StringSetting}<br>" +
                    $"Date Setting: {settings.DateSetting.ToString("d")}<br>" +
                    $"Boolean Setting: {settings.BooleanSetting}<br><br>" +
                    $"Nested Integer Setting: {settings.ObjectSettings.IntegerSetting}<br>" +
                    $"Nested Decimal Setting: {settings.ObjectSettings.DecimalSetting}<br>" +
                    $"Nested DateTime Setting: {settings.ObjectSettings.DateTimeSetting}<br><br>");

                await myClass.WriteOtherSettingsAsync(context.Response);
            });
        }
    }
}