using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO.Ports;
using System.IO;

using NetConnectionApi;

namespace Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            // otetaan yhteys palvelimeen
            NetConnection conn = new NetConnection("localhost", 8221);

            conn.Open();

            conn.Write("aika");
            // luetaan vastaus syötevirrasta
            string vastaus = conn.Read();
            Console.WriteLine(vastaus);

            // suljetaan
            conn.Close();
        }
    }
}
