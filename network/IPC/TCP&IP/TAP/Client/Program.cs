using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client : TcpClient
    {
        // 메시지는 개행으로 구분한다.
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;

        private byte[] buffer = new byte[1024];
        private StringBuilder sb = new StringBuilder();

        private TcpClient socket;
        private IPEndPoint remoteAddr;

        public Client(string ip, int port)
        {
            this.socket = new TcpClient(ip, port);
            remoteAddr = (IPEndPoint) socket.Client.RemoteEndPoint;

            // case 01. TAP
            // Task.Factory.StartNew(Receive, this);

            // case 02. APM 응용
            socket.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, this);
        }

        private async void Receive(object obj)
        {
            while (socket.Connected)
            {
                int size = await socket.GetStream().ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

                sb.Append(Encoding.Unicode.GetString(buffer, 0, size).Trim('\0'));
                // 메시지의 끝이 이스케이프 \r\n와 >의 형태이면 클라이언트에 표시한다.        
                if (sb.Length >= 3 && sb[sb.Length - 3] == CR && sb[sb.Length - 2] == LF && sb[sb.Length - 1] == '>')
                {
                    string msg = sb.ToString();
                    // 콘솔에 출력한다.          
                    Console.Write(msg);
                    // 버퍼 초기화
                    sb.Clear();
                }
            }

            Console.WriteLine($"Disconnected :  (From: {remoteAddr.Address.ToString()}:{remoteAddr.Port}, Connection time: {DateTime.Now})");
        }


        //메시지가 오면 호출된다.
        private async void Receive(IAsyncResult result)
        {
            if (socket.Connected)
            {
                //EndReceive를 호출하여 데이터 사이즈를 받는다.
                // EndReceive는 대기를 끝내는 것이다.
                int size = socket.Client.EndReceive(result);

                sb.Append(Encoding.Unicode.GetString(buffer, 0, size).Trim('\0'));
                // 메시지의 끝이 이스케이프 \r\n와 >의 형태이면 클라이언트에 표시한다.        
                if (sb.Length >= 3 && sb[sb.Length - 3] == CR && sb[sb.Length - 2] == LF && sb[sb.Length - 1] == '>')
                {
                    string msg = sb.ToString();
                    // 콘솔에 출력한다.          
                    Console.Write(msg);
                    // 버퍼 초기화
                    sb.Clear();
                }

                // buffer로 메시지를 바독 Recive함수로 메시지가 올 때까지 대기한다.
                socket.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, this);
            }
            else
            {
                // 접속이 끊겼다..
                Console.WriteLine($"Disconnected :  (From: {remoteAddr.Address.ToString()}:{remoteAddr.Port}, Connection time: {DateTime.Now})");
            }
        }

        public void Send(string msg)
        {
            byte[] data = Encoding.Unicode.GetBytes(msg);

            socket.Client.Send(data, data.Length, SocketFlags.None);
        }
    }

    class Program
    {
        public Program()
        {
            var client = new Client("127.0.0.1", 1004);
            while (true)
            {
                string k = Console.ReadLine();
                client.Send(k + "\r\n");
                if ("exit".Equals(k, StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }

        static void Main(string[] args)
        {
            new Program();
        }
    }
}
