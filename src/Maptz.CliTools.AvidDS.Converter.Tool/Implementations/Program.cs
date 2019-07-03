//using Maptz.CliTools.AvidDS.Converter.Tool.Engine
using Maptz.CliTools.AvidDS.Converter.Engine;
using Maptz.Editing.Avid.DS;
using Maptz.Editing.Avid.DS.Converters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Maptz.CliTools.AvidDS.Converter.Tool
{

    class Program : CliProgramBase<AppSettings>
    {
        public static void Main(string[] args)
        {
            new Program(args);
        }

        public Program(string[] args) : base(args)
        {

        }

        protected override void AddServices(IServiceCollection services)
        {
            base.AddServices(services);
            services.AddTransient<ICliProgramRunner, CLIProgramRunner>();

            services.AddLogging(loggingBuilder => loggingBuilder.AddConfiguration(Configuration.GetSection("Logging")).AddConsole().AddDebug());

            services.AddTransient<IAvidDSDocumentReader, AvidDSDocumentReader>();
            services.AddTransient<IAvidDSToExcelConverter, AvidDSToExcelConverter>();
            services.AddTransient<IAvidDSConverterEngine, AvidDSConverterEngine>();
        }
    }

}


