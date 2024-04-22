using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using GettingStartedLib;

namespace GettingStartedHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Step 1: Create a URI to serve as the base address.
            Uri baseAddress = new Uri($"http://localhost:8081/GettingStarted/CalculatorService");
            ServiceHost host = new ServiceHost(typeof(CalculatorService), baseAddress);
            try
            {
                WSHttpBinding binding = new WSHttpBinding();
                binding.Security.Mode = SecurityMode.Message;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;

                // Step 3: Add a service endpoint.
                host.AddServiceEndpoint(typeof(ICalculator), binding, "");

                // Step 4: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);

                host.Credentials.ServiceCertificate.SetCertificate(
                    System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                    System.Security.Cryptography.X509Certificates.StoreName.My,
                    System.Security.Cryptography.X509Certificates.X509FindType.FindByThumbprint, "592BC3217DAFF33EF614D8B91AA04A2FC655A2E9");

                // Step 5: Start the service.
                host.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                host.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                host.Abort();
            }
        }
    }
}