﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GettingStartedClient.CalculatorService;

namespace GettingStartedClient
{
    public partial class MainForm : Form
    {
        CalculatorClient client = null;

        public MainForm()
        {
            InitializeComponent();
            client = new CalculatorClient();

            client.ClientCredentials.ClientCertificate.SetCertificate(
                System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                System.Security.Cryptography.X509Certificates.StoreName.My,
                System.Security.Cryptography.X509Certificates.X509FindType.FindByThumbprint, "5619207BCAFC85D7D2FEC7A621458A9CBA863C3A");
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