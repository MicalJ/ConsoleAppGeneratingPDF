using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.IO;

namespace ConsoleApp1.Services
{
    public class ProtocolService : IProtocolService
    {
        private readonly IConverter _converter;

        public ProtocolService(IConverter converter)
        {
            _converter = converter;
        }

        public void GeneratePDF()
        {
           var converter = new BasicConverter(new PdfTools());

            string directory = "Protocols";
            if (!Directory.Exists(Path.GetFullPath(directory)))
            {
                Directory.CreateDirectory(Path.GetFullPath(directory));
            }

            string fileName = "protocol_" + Guid.NewGuid() + ".pdf";
            string protocolFile = Path.Combine(directory, fileName);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings
                    {
                        Top = 10,
                        Bottom = 10
                    },
                    Out = protocolFile,
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = "<!doctype html> <html><body>TEST</body></html>",
                        PagesCount=true,
                        FooterSettings = {Right="strona [page] stron [toPage]", Line=true, FontSize=7},
                        WebSettings = {DefaultEncoding="utf-8",EnableIntelligentShrinking = true}
                    }
                }
            };

            converter.Convert(doc);
        }
    }
}
