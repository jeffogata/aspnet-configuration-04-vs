namespace AspNetConfigurationVS
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.OptionsModel;

    public class MyClass
    {
        private readonly OtherSettings _otherSettings;

        public MyClass(IOptions<OtherSettings> otherSettingsAccessor)
        {
            _otherSettings = otherSettingsAccessor.Value;
        }

        public async Task WriteOtherSettingsAsync(HttpResponse response)
        {
            await response.WriteAsync(
                $"OtherSettings Strings: {string.Join(", ", _otherSettings.Strings)}<br>" +
                $"OtherSettings Numbers: {string.Join(", ", _otherSettings.Numbers)}");
        }
    }
}