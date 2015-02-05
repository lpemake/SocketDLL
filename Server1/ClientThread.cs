using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.IO;

using NetConnectionApi;

namespace Server1
{
    class ClientThread
    {
        private TcpClient client;
        
        public ClientThread(TcpClient client)
        {
            this.client = client;
        }

        public void Run()
        {
            NetConnection conn =
                new NetConnection(client);

            conn.Open();

            // luetaan komento
            string komento = conn.Read();


            if (komento == "aika")
            {
                // palautetaan kellonaika asiakkaalle
                DateTime date = DateTime.Now;
                string s = date.ToString();
                Console.WriteLine(s);
                conn.Write(s);
            }
            // suljetaan            
            conn.Close();
        }
    }
}
