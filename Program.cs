using System.Net.Sockets;
using System.Net;
using System.Text;

TcpListener server = null;


try
{
    var port = 4444;
    IPAddress localAddress = IPAddress.Parse("127.0.0.1"); 

    server = new TcpListener(localAddress, port);

    server.Start();

    string data = null;

    while (true)
    {
        Console.WriteLine("Waiting for a connection... ");
        TcpClient client = server.AcceptTcpClient();


        data = null;

        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytes = stream.Read(buffer, 0, buffer.Length);
        data = Encoding.UTF8.GetString(buffer, 0, bytes);

        Console.WriteLine("Received: {0}", data);

        string httpResponse = "HTTP/1.1 200 OK\r\nContent-Type: text/html; charset=UTF-8\r\n\r\n" + "<!DOCTYPE html><html><head><title>From C#</title></head><body><h1>Hello from C#</h1></body></html>";
        byte[] msg = Encoding.UTF8.GetBytes(httpResponse);
        stream.Write(msg, 0, msg.Length);

        client.Close();
    }


}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
finally
{
    server.Stop();
}