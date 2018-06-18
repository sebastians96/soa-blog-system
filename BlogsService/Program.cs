using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;
using System;

namespace BlogsService
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8080/Blog");

            WebServiceHost myHost = new WebServiceHost(typeof(BlogService), baseAddress);
            Console.WriteLine("Starting Service");
             myHost.Open();

            Console.WriteLine("The service is ready at {0}", baseAddress);
            Console.WriteLine("Press <Enter> to stop the service.");
            Console.ReadLine();

            myHost.Close();
        }
    }
}
