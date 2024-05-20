using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Connection
    {
        private Program parent = null;

        // 메시지는 개행으로 구분한다.
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;

        private TcpClient connection;
        public IPEndPoint info;

        private byte[] buffer = new byte[1024];
        private StringBuilder sb = new StringBuilder();

        public string ConnID => $"{info.Address.ToString()}:{info.Port}";

        public Connection(Program parent, TcpClient connection)
        {
            this.parent = parent;
            this.connection = connection;
            this.info = (IPEndPoint)connection.Client.RemoteEndPoint;
            Task.Factory.StartNew(Receive, this);

            Console.WriteLine($"Clinet:(From:{info.Address.ToString()}:{info.Port}, Connection Time : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            Send("Welcome server!\r\n>");
        }

        private async void Receive(object obj)
        {
            while(connection.Connected)
            {
                int size = await connection.GetStream().ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                sb.Append(Encoding.Unicode.GetString(buffer, 0, size).Trim('\0'));
                // 메시지의 끝이 이스케이프 \r\n의 형태이면 서버에 표시한다.
                if (sb.Length >= 2 && sb[sb.Length - 2] == CR && sb[sb.Length - 1] == LF)
                {
                    // 개행은 없애고..
                    sb.Length = sb.Length - 2;
                    string msg = sb.ToString();
                    Console.WriteLine(msg);

                    string[] protoclMessage = msg.Split('=');
                    if (protoclMessage.Length == 2)
                    {
                        parent.socketRun(protoclMessage[1], this);
                    }

                    parent.Send($"[From: {info.Address.ToString()}:{info.Port}] {msg}\r\n>");
                    if ("exit".Equals(msg, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Disconnected : (From: {info.Address.ToString()}:{info.Port}, Connection time: {DateTime.Now})");
                        // 접속을 중단한다.
                        connection.Close();
                        return;
                    }

                    // 버퍼를 비운다.
                    sb.Clear();
                }
            }

            // 접속이 끊겼다..
            Console.WriteLine($"Disconnected :  (From: {info.Address.ToString()}:{info.Port}, Connection time: {DateTime.Now})");
        }

        public void Send(string msg)
        {
            byte[] data = Encoding.Unicode.GetBytes(msg);

            connection.Client.Send(data, data.Length, SocketFlags.None);
        }
    }

    class Program : TcpListener
    {
        public Program()
            : base(IPAddress.Any, 1004)
        {
            base.Start(1);
            // 비동기 소켓으로 Accept 클래스로 대기한다.
            BeginAccept();
        }

        public List<Connection> _connectionList = new List<Connection>();
        public Dictionary<string, Connection> _readyConnectionList = new Dictionary<string, Connection>();
        public Dictionary<string, Connection> _runningConnectionList = new Dictionary<string, Connection>();

        private async Task BeginAccept()
        {
            while (true)
            {
                // 비동기 Accept                
                var client = new Connection(this, await AcceptTcpClientAsync().ConfigureAwait(false));
                _connectionList.Add(client);
                _readyConnectionList.Add(client.ConnID, client);
            }
        }

        public void socketRun(string id, Connection conn )
        {
            _runningConnectionList.Add(id, conn);
            _readyConnectionList.Remove(conn.ConnID);
        }

        public void Send(string msg)
        {
            foreach (var connection in _connectionList)
            {
                connection.Send(msg);
            }
        }


        static void Main(string[] args)
        {
            new Program();

            Console.WriteLine($"Press the q key to exit.");
            while (true)
            {
                string k = Console.ReadLine();
                if ("q".Equals(k, StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }
}
