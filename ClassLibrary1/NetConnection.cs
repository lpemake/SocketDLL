using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO.Ports;
using System.IO;

namespace NetConnectionApi
{
    public class NetConnection
    {
        // jäsenmuuttujat
        private bool bServer;
        private int port;
        private string ipAddress;
        private TcpClient client;
        private NetworkStream ns;
        private StreamWriter streamWriter;
        private StreamReader streamReader;

        // konstruktori clientia varten
        public NetConnection(string ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.bServer = false;
        }

        // konstruktori serveriä varten
        public NetConnection(TcpClient client)
        {
            this.client = client;
            this.bServer = true;
        }

        public bool Open()
        {
            bool bOK = true;
            try
            {
                if (bServer == false)
                {
                    this.client = new TcpClient(this.ipAddress, this.port);
                }

                // avataan NetworkStream client olion kautta
                this.ns = client.GetStream();

                // avataan syöte- ja tulostevirrat
                this.streamWriter = new StreamWriter(ns);
                this.streamReader = new StreamReader(ns);

                // Autoflush kannattaa laittaa päälle
                this.streamWriter.AutoFlush = true;
            }
            catch (SocketException e)
            {
                bOK = false;
            }
            return bOK;
        }

        public string Read()
        {
            return streamReader.ReadLine();
        }

        public void Write(string data)
        {
            streamWriter.WriteLine(data);
        }

        public void Close()
        {
            streamWriter.Close();
            streamReader.Close();
            ns.Close();
            client.Close();
        }
    }
}
