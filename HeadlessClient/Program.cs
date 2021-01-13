using System;
using Grpc.Core;
using SomeSimpleEcommerce;

namespace HeadlessClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GRPC_VERBOSITY", "NONE");

            Console.WriteLine("Press any key to begin.");
            Console.ReadKey();

            var client = new EcommerceClient();

            var currentTime = client.GiveMeTheTime();

            Console.WriteLine($"Current time is: {currentTime.Hour}:{currentTime.Minute}");

            var validCart = client.ShowChart(1);

            Console.WriteLine($"User id 1 has {validCart.Items.Count} items in his cart with the toals sum of {validCart.Total} money.");
            foreach (var item in validCart.Items)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.ItemName);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" qty: {item.ItemQuantity}");
            }

            Console.WriteLine("Example Exception response: ");
            try
            {
                var invalidcart = client.ShowChart(2);
            }
            catch (RpcException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Status Code:{e.StatusCode}, Message: {e.Status.Detail}");
                Console.ForegroundColor = ConsoleColor.White;
            }


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


    }
}
