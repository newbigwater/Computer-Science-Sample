using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GettingStartedClient.CalculatorService;

namespace GettingStartedClient
{
    public partial class MainForm : Form
    {
        private CalculatorClient client;

        public MainForm()
        {
            InitializeComponent();

            client = new CalculatorClient();
            client.ClientCredentials.ClientCertificate.SetCertificate(
                System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                System.Security.Cryptography.X509Certificates.StoreName.My,
                System.Security.Cryptography.X509Certificates.X509FindType.FindByThumbprint, "4667E3F94B2087D948E180328B05BE90FE8F07F6");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            double result = client.Add(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void btn_sub_Click(object sender, EventArgs e)
        {
            double result = client.Subtract(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void btn_mul_Click(object sender, EventArgs e)
        {
            double result = client.Multiply(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void btn_div_Click(object sender, EventArgs e)
        {
            double result = client.Divide(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Close();
        }
    }
}