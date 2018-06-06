using ConsoleApp1.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Setup DI");
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IProtocolService, ProtocolService>()
            .AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()))
            .BuildServiceProvider();

            Console.WriteLine("Do the actual work...");
            var protocolService = serviceProvider.GetService<IProtocolService>();

            protocolService.GeneratePDF();

            Console.WriteLine("All done!");
            Thread.Sleep(2000);
        }
    }
}
