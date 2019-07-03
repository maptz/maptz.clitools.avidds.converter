using Maptz;
using Maptz.CliTools;
using Maptz.CliTools.AvidDS.Converter.Engine;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Maptz.CliTools.AvidDS.Converter.Tool
{

     public class CLIProgramRunner : ICliProgramRunner
    {
        /* #region Public Properties */
        public AppSettings AppSettings { get; }
        public IServiceProvider ServiceProvider { get; }
        /* #endregion Public Properties */
        /* #region Public Constructors */
        public CLIProgramRunner(IOptions<AppSettings> appSettings, IServiceProvider serviceProvider)
        {
            this.AppSettings = appSettings.Value;
            this.ServiceProvider = serviceProvider;
        }
        /* #endregion Public Constructors */
        /* #region Public Methods */
        public async Task RunAsync(string[] args)
        {
            await Task.Run(() =>
            {
                CommandLineApplication cla = new CommandLineApplication(throwOnUnexpectedArg: false);
                cla.HelpOption("-?|-h|--help");

                /* #region get */
                cla.Command("convert", config =>
                                {
                                    var inputOption = config.Option("-i|--input <inputFilePath>", "The AvidDS file to convert", CommandOptionType.SingleValue);
                                    var outputOption = config.Option("-o|--output <outputFilePath>", "The destination file path", CommandOptionType.SingleValue);
                                    var modeOption = config.Option("-m|--mode <conversionMode>", "The conversion mode (Excel)", CommandOptionType.SingleValue);
                                    config.OnExecute(() =>
                                    {
                                        var inputFilePath = inputOption.HasValue() ? inputOption.Value() : null;
                                        var outputFilePath = outputOption.HasValue() ? outputOption.Value() : null;
                                        var mode = modeOption.HasValue() ? (ConversionMode) Enum.Parse(typeof(ConversionMode), modeOption.Value()) : ConversionMode.Excel;
                                        var converterEngine = this.ServiceProvider.GetRequiredService<IAvidDSConverterEngine>();
                                        converterEngine.Convert(inputFilePath, mode, outputFilePath);
                                        return 0;
                                    });

                                });
                /* #endregion*/

                /* #region Default */
                //Just show the help text.
                cla.OnExecute(() =>
                {
                    cla.ShowHelp();
                    return 0;

                });
                /* #endregion*/
                cla.Execute(args);
            });
        }
        /* #endregion Public Methods */
    }
}