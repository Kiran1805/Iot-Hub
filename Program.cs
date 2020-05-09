using System;
using System.Text;
using Microsoft.Azure.Devices.Client;

namespace IotHubReceiver
{
    class Program
    {
        static DeviceClient s_deviceClient;
        static string connectionstring = "HostName=raspiiothub.azure-devices.net;DeviceId=mydeviceid;SharedAccessKey=bMR6lFZrIrauI5LuyAMxUSWHAXr5iMWucmFqFtaEeFc=";
        static void Main(string[] args)
        {
            s_deviceClient = DeviceClient.CreateFromConnectionString(connectionstring);
            RecieveC2dAsync();
            Console.ReadLine();
        }
        private static async void RecieveC2dAsync()
        {
            Console.WriteLine("Message recieving from Cloud to device from service");
            while (true)
            {
                Microsoft.Azure.Devices.Client.Message recievedmsg = await s_deviceClient.ReceiveAsync();
                if (recievedmsg == null) continue;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Recieved message: {0}",
                    Encoding.ASCII.GetString(recievedmsg.GetBytes()));
                Console.ResetColor();

                await s_deviceClient.CompleteAsync(recievedmsg);
            }
        }
    }
}