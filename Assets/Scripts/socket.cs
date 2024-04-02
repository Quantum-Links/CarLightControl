
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TcpLink
{
    async static void TCPClientInit()
    {
        TcpClient tcpClient = new TcpClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 23333));
        await tcpClient.ConnectAsync(IPAddress.Parse("127.0.0.1"), 8999);
        var netStream = tcpClient.GetStream();
        var sendTask = TCPClientSend(tcpClient);
        var receiveTask = TCPClientReceive(netStream);
    }
    async static Task TCPClientSend(TcpClient tcpClient)
    {
        var netStream = tcpClient.GetStream();
        byte[] buffer = Encoding.UTF8.GetBytes("你好世界");
        while (tcpClient.Connected)
        {
            await netStream.WriteAsync(buffer);
            await Task.Delay(500);
        }
    }
    async static Task TCPClientReceive(NetworkStream networkStream)
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            using MemoryStream stream = new MemoryStream();
            do
            {
                int recvLength = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                //连接断开关闭接受
                if (recvLength == 0)
                {
                    return;
                }
                await stream.WriteAsync(buffer, 0, recvLength);
            }
            while (networkStream.DataAvailable);
        }
    }

}