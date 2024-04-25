using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program : Socket
    {
        // 메시지는 개행으로 구분한다.
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;

        private byte[] buffer = new byte[1024];
        private StringBuilder sb = new StringBuilder();

        public Program()
            : base(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream,
                System.Net.Sockets.ProtocolType.Tcp)
        {
            // 접속 요청
            base.BeginConnect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1004), Connect, this);
            while (true)
            {
                string k = Console.ReadLine();
                Send(k + "\r\n");
                if ("exit".Equals(k, StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }

        // 접속되면 호출된다.
        private void Connect(IAsyncResult result)
        {
            // 접속 대기를 끝낸다.
            base.EndConnect(result);
            // buffer로 메시지를 바독 Recive함수로 메시지가 올 때까지 대기한다.
            base.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, this);
        }

        //메시지가 오면 호출된다.
        private void Receive(IAsyncResult result)
        {
            if (Connected)
            {
                //EndReceive를 호출하여 데이터 사이즈를 받는다.
                // EndReceive는 대기를 끝내는 것이다.
                int size = this.EndReceive(result);

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
                base.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, Receive, this);
            }
            else
            {
                // 접속이 끊겼다..
                var remoteAddr = (IPEndPoint)RemoteEndPoint;
                Console.WriteLine($"Disconnected :  (From: {remoteAddr.Address.ToString()}:{remoteAddr.Port}, Connection time: {DateTime.Now})");
            }
        }

        private void Send(string msg)
        {
            byte[] data = Encoding.Unicode.GetBytes(msg);
            Send(data, data.Length, SocketFlags.None);
        }

        static void Main(string[] args)
        {
            new Program();
        }
    }
}
