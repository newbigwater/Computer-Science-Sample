using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using soapClient.ServiceReference1;

namespace soapClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // SimpleSoapServer Class Name + Client 로 객체 할당 가능
            var client = new SimpleSoapServerSoapClient();

            List<String> list = client.GetCurrentors("C#");
            Console.WriteLine($"Count : {client.Count()}");
            Console.WriteLine($"User List");
            foreach (var addr in list)
            {
                Console.WriteLine($" {addr}");
            }
            Console.WriteLine($"[Add] 1 + 2 = {client.Add(1, 2)}");
            Console.WriteLine($"[Sub] 1 - 2 = {client.Sub(1, 2)}");
            Console.WriteLine($"[Div] 1 / 2 = {client.Div(1, 2)}");
            Console.WriteLine($"[Mul] 1 * 2 = {client.Mul(1, 2)}");

            Console.ReadKey();
        }
    }
}
