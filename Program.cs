using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace IotHubSender
{
    class Program
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=raspiiothub.azure-devices.net;DeviceId=mydeviceid;SharedAccessKey=bMR6lFZrIrauI5LuyAMxUSWHAXr5iMWucmFqFtaEeFc=";
        static void Main(string[] args)
        {
            Console.WriteLine("Send message to IOT device");
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            Console.WriteLine("Press any key to send message");
            Console.ReadLine();
            SendC2DMsgAsync().Wait();
            Console.ReadLine();
        }
        private async static Task SendC2DMsgAsync()
        {
            var commandMessage = new
                Message(Encoding.ASCII.GetBytes("Temperature data sent"));
            await serviceClient.SendAsync("mydeviceid", commandMessage);
        }
    }
}