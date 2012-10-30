using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TCP
{
    class Client
    {
        public static void Run()
        {
            int serverPort = 8080;
            IPAddress serverIp = IPAddress.Parse("10.25.232.214"); //IPAdress
            Console.WriteLine("Server IP: {0}", serverIp);

            Socket echoSocket = new Socket(
                serverIp.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            echoSocket.Connect(serverIp, serverPort);

            using (Stream echoServiceStream = new NetworkStream(echoSocket, true))

            {
                string msg = "Hello world";
                Byte[] msgByte = encodeString(msg);
                //Gets the length of the message in bytes
                Byte[] msgByteLength = BitConverter.GetBytes(msg.Length);
                //Sends the length of the message in bytes
                echoServiceStream.Write(msgByteLength, 0, msgByteLength.Length);
                echoServiceStream.Flush();
                //Sends the message
                echoServiceStream.Write(msgByte, 0, msgByte.Length);
                //writer.Write(msg.Length);
               // writer.WriteLine(msg);
                echoServiceStream.Flush();
             
            }
        }

        private static Byte[] encodeString(string s)
        {
            System.Text.ASCIIEncoding enconding = new System.Text.ASCIIEncoding();
            Byte[] message = enconding.GetBytes(s);
            return message;
        }

      
    }
}