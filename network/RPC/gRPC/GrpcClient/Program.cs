using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                            new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);

            var ret = await client.TestAPIAsync(
                            new TestMsg { Param = "TestParameter" });
            Console.WriteLine("Greeting: " + ret.Param);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
