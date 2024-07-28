using DevExpress.Utils.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatcherPhotoProject.SocketClass
{
    public class TcpListenerSocket
    {
        IPAddress myIP = IPAddress.Parse("127.0.0.1");
        public Task<string> StartSocketServer()/////
        {
            return (Task<string>)Task.Run(() =>
            {
                TcpListener server = null;
                string id = null;
                try
                {
                    int port = 8888;
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();

                    while (true)
                    {
                        TcpClient client = server.AcceptTcpClient();
                        NetworkStream stream = client.GetStream();

                        byte[] buffer = new byte[256];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        id = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        client.Close();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("SocketException: " + ex.Message);
                }
                finally
                {
                    server.Stop();
                }
                return id;
            });
        }
    }
}
