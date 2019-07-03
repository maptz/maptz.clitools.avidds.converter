using Maptz.Editing.Avid.DS;
using Maptz.Editing.Avid.DS.Converters;
using System.IO;
namespace Maptz.CliTools.AvidDS.Converter.Engine
{

    public interface IAvidDSConverterEngine
    {
        void Convert(string filePath, ConversionMode mode, string outputFilePath);
    }
}