using System;
using Grpc.Core;

namespace Server
{
    class Program
    {
        private const int PORT = 34567;
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GRPC_VERBOSITY", "NONE");

            var server = new Grpc.Core.Server()
            {
                Services = { SomeSimpleEcommerce.ShopService.BindService(new EcommerceService()) },
                Ports = { new ServerPort("localhost", PORT, ServerCredentials.Insecure) }
            };

            server.Start();

            Console.WriteLine($"Service running on port {PORT}");
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
