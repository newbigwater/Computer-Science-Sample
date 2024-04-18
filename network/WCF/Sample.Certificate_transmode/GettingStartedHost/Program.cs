using System;
using System.Collections.Generic;
using System.Linq;
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
            Uri baseAddress = new Uri("https://localhost:7070/GettingStarted/CalculatorService/mex");

            ServiceHost selfHost = new ServiceHost(typeof(CalculatorService), baseAddress);

            try
            {
                WSHttpBinding binding = new WSHttpBinding();
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;

                // Step 3: Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(ICalculator), binding, "");

                // Step 4: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.HttpsGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Credentials.ServiceCertificate.SetCertificate(
                    System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                    System.Security.Cryptography.X509Certificates.StoreName.My,
                    System.Security.Cryptography.X509Certificates.X509FindType.FindByThumbprint, "18B36B0F1956F01F8AE440D1B98AA73FC1742724");

                // Step 5: Start the service.
                selfHost.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}