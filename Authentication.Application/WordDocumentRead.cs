using Authentication.Domain.Services;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System.Xml.Linq;

namespace Authentication.Application
{
    public class WordDocumentRead : IWordDocumentRead
    {
        private readonly string _basePath = @"D:\MT";
        public WordDocumentRead()
        {
             
        }
        public string GetDocumentHtml(string doctorId, string modality, string fileName)
        {
            // Build the full file path, e.g., D:\MT\DOC1530\CT\hii.docx
            string filePath = Path.Combine(_basePath, doctorId, modality, fileName);

            if (!File.Exists(filePath))
                return string.Empty;

            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            using var wordDoc = WordprocessingDocument.Open(fs, true); // Open in editable mode

            var htmlSettings = new HtmlConverterSettings()
            {
                PageTitle = fileName
            };

            XElement html = HtmlConverter.ConvertToHtml(wordDoc, htmlSettings);
            return html.ToString(SaveOptions.DisableFormatting);

        }

    }
}
