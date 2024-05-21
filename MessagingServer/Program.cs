using System;
using System.ServiceModel;

namespace MessagingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MessagingService _server = new MessagingService();
            using (ServiceHost host = new ServiceHost(_server))
            {
                host.Open();
                Console.WriteLine("Initiated Server........\n Running.........");
                Console.ReadLine();
            }
        }
    }
}