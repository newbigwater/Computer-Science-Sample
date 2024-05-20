using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client : SocketAsyncEventArgs
    {
        // 메시지는 개행으로 구분한다.
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;

        // 메시지를 모으기 위한 버퍼    
        private StringBuilder sb = new StringBuilder();

        public Socket Socket => base.UserToken is Socket ? base.UserToken as Socket : null;

        public Client(Socket socket, EndPoint pep)
        {
            base.UserToken = socket;
            RemoteEndPoint = pep;

            // 접속시 발생하는 이벤트를 등록한다.      
            base.Completed += Connected_Completed;
        }    

        private void Connected_Completed(object sender, SocketAsyncEventArgs e)    
        {      
            // 접속 이벤트는 해제한다.      
            base.Completed -= Connected_Completed;

            // 버퍼 설정
            base.SetBuffer(new byte[1024], 0, 1024);
            // 수신 이벤트를 등록한다.      
            base.Completed += Client_Completed;
            // 메시지가 오면 이벤트를 발생시킨다. (IOCP로 넣는 것)      
            Socket.ReceiveAsync(this);
        }    
        
        // 메시지가 오면 발생하는 이벤트    
        private void Client_Completed(object sender, SocketAsyncEventArgs e)    
        {      
            // 접속이 연결되어 있으면...
            if (Socket.Connected && base.BytesTransferred > 0)      
            {        
                // 수신 데이터는 e.Buffer에 있다.        
                byte[] data = e.Buffer;        
                // 데이터를 string으로 변환한다.        
                string msg = Encoding.Unicode.GetString(data);        
                // 메모리 버퍼를 초기화 한다. 크기는 1024이다        
                base.SetBuffer(new byte[1024], 0, 1024);        
                sb.Append(msg.Trim('\0'));        
                // 메시지의 끝이 이스케이프 \r\n와 >의 형태이면 클라이언트에 표시한다.        
                if (sb.Length >= 3 && sb[sb.Length - 3] == CR && sb[sb.Length - 2] == LF && sb[sb.Length - 1] == '>')
                {
                    msg = sb.ToString();          
                    // 콘솔에 출력한다.          
                    Console.Write(msg);          
                    // 버퍼 초기화
                    sb.Clear();
                }
                // 메시지가 오면 이벤트를 발생시킨다. (IOCP로 넣는 것)        
                Socket.ReceiveAsync(this);
            }      
            else      
            {        
                // 접속이 끊겼다..
                var remoteAddr = (IPEndPoint)Socket.RemoteEndPoint;
                Console.WriteLine($"Disconnected :  (From: {remoteAddr.Address.ToString()}:{remoteAddr.Port}, Connection time: {DateTime.Now})");
            }
        }    
        
        public void Send(String msg)    
        {      
            byte[] sendData = Encoding.Unicode.GetBytes(msg);
            Socket.Send(sendData, sendData.Length, SocketFlags.None);
        }
    }

    // 메인 Program은 Socket을 상속받고 클라이언트 Socket으로 사용한다.
    class Program : Socket
    {
        public Program()
            : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {
            var client = new Client(this, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1004));
            base.ConnectAsync(client);
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
