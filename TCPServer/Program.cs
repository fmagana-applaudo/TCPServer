using System.Net.Sockets;
using System.Net;
using System.Text;

IPAddress ipAddress = IPAddress.Any;
int port = 5050;

TcpListener listener = new TcpListener(ipAddress, port);

try
{
    listener.Start();
    Console.WriteLine($"Listening events on port {port}...");

    while (true)
    {
        using (TcpClient client = listener.AcceptTcpClient())
        {
            Console.WriteLine("Connection accepted.");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Received event data: {data}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
finally
{
    listener.Stop();
}