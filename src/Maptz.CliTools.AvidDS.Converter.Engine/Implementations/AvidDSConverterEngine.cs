using Maptz.Editing.Avid.DS;
using Maptz.Editing.Avid.DS.Converters;
using System.IO;
namespace Maptz.CliTools.AvidDS.Converter.Engine
{

    public class AvidDSConverterEngine : IAvidDSConverterEngine
    {
        public AvidDSConverterEngine(IAvidDSDocumentReader avidDSDocumentReader, IAvidDSToExcelConverter avidDSToExcelConverter)
        {
            AvidDSDocumentReader = avidDSDocumentReader;
            AvidDSToExcelConverter = avidDSToExcelConverter;
        }

        public IAvidDSDocumentReader AvidDSDocumentReader { get; }
        public IAvidDSToExcelConverter AvidDSToExcelConverter { get; }

        public void Convert(string filePath, ConversionMode mode, string outputFilePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            string ds;
            using (var str = new FileInfo(filePath).OpenText()) { ds = str.ReadToEnd(); }
            var dsDocument = this.AvidDSDocumentReader.Read(ds);

            if (string.IsNullOrEmpty(outputFilePath))
            {
                outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".xlsx");
            }

            this.AvidDSToExcelConverter.Convert(dsDocument, outputFilePath);

        }
    }
}