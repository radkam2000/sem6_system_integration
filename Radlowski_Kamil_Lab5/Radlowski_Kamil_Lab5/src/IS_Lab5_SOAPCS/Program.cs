using ServiceReference1;
using System;
using System.Threading.Tasks;

namespace LAB5_SOAP_client
{
    internal class Program
    {
            static async Task Main(string[] args)
            {
                Console.WriteLine("My First SOAP Client!");
                MyFirstSOAPInterfaceClient client = new MyFirstSOAPInterfaceClient();
                string text = await client.getHelloWorldAsStringAsync("Karol");
                long text2 = await client.getDaysBetweenDatesAsync("22 01 2000", "28 01 2000");
                Console.WriteLine(text);
                Console.WriteLine(text2);
            }
        
    }
}