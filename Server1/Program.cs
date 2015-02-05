using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace Server1
{
    class Program
    {

        static void Main(string[] args)
        {
            // luodaan tvpListener palvelinta varten
            TcpListener tcpListener = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                tcpListener =
                    new TcpListener(localAddr, 8221);
                // käynnistetään palvelin
                tcpListener.Start();

                while (true)
                {
                    // jäädään odottamaan yhteyspyyntöä (blokkaavasti)
                    TcpClient client = tcpListener.AcceptTcpClient();

                    ClientThread ct = new ClientThread(client);
                    Thread t = new Thread(ct.Run);

                    t.Start();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                tcpListener.Stop();
            }
        }

    }
}
