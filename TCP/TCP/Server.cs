using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TCP
{
    class Server
    {
        public static void Run()
        {
            int serverPort = 8080;
            using (Socket echoListener = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp))
            {
                IPEndPoint echoEndPoint = new IPEndPoint(IPAddress.Any, serverPort);
                echoListener.Bind(echoEndPoint);
                echoListener.Listen(20);

                while (true)
                {
                    Console.WriteLine("Waiting for connections...");
                    Socket incomingConnection = echoListener.Accept();

                    using (NetworkStream connStream = new NetworkStream(incomingConnection, true))
                    {
                        Console.WriteLine("Accepted connection");
                        //A buffer to hold the size of the message
                        byte[] buffer = new byte[4];
                        //Read the size of the message
                        connStream.Read(buffer, 0, 4);

                        //Convert the byte[] to an int
                        int msgLength = (buffer[0]) + (buffer[1] << 8) + (buffer[2] << 16) + (buffer[3] << 24);

                        //A buffer to hold the message
                        buffer = new byte[msgLength];
                        //Read the message
                        connStream.Read(buffer, 0, msgLength);

                        //Decode the message to a string
                        String msg = decode(buffer);

                        Console.WriteLine("[Server Recieved]: {0}", msg);
                    }
                }
            }
        }

        /// <summary>
        /// Decode a byte[] into a String
        /// </summary>
        /// <param name="arr">The byte[] to decode</param>
        /// <returns>The decoded String</returns>
        private static String decode(byte[] arr)
        {
            System.Text.ASCIIEncoding decoder = new System.Text.ASCIIEncoding();
            String message = decoder.GetString(arr);
            return message;
        }
    }
}