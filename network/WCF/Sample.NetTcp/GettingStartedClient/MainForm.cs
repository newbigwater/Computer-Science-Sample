using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GettingStartedLib;

namespace GettingStartedClient
{
    public partial class MainForm : Form
    {
        ICalculator client = null;

        public MainForm()
        {
            InitializeComponent();

            ChannelFactory<ICalculator> factory = new ChannelFactory<ICalculator>();

            // Address
            string address = "net.tcp://localhost:8081/GettingStarted/CalculatorService";
            factory.Endpoint.Address = new EndpointAddress(address);

            // Binding : TCP 사용
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            factory.Endpoint.Binding = binding;

            // Contract 설정
            factory.Endpoint.Contract.ContractType = typeof(ICalculator);

            // Channel Factory 만들기
            client = factory.CreateChannel();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            CalculateResult result = client.Add(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ResultVal.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void btn_sub_Click(object sender, EventArgs e)
        {
            CalculateResult result = client.Subtract(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ResultVal.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void btn_mul_Click(object sender, EventArgs e)
        {
            CalculateResult result = client.Multiply(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ResultVal.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void btn_div_Click(object sender, EventArgs e)
        {
            CalculateResult result = client.Divide(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
            lb_result.Text = result.ResultVal.ToString();
            Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((ICommunicationObject)client).Close();
        }
    }
}
