using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
      
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1"; //Run over localhost
        
        static void Main(string[] args)
        {
            
            int i = 0;
            while (i < 10)
            {
                //---listen at the specified IP and port no.---
                IPAddress localAdd = IPAddress.Parse(SERVER_IP);
                TcpListener listener = new TcpListener(localAdd, PORT_NO);
                Console.WriteLine("Listening...");
                listener.Start();

                //---incoming client connected---
                TcpClient client = listener.AcceptTcpClient();

                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                int[] array = new int[5];
                array[0] = dataReceived.IndexOf('');
                for (int j = 1; j<4;j++)
                {
                    array[j] = dataReceived.IndexOf('', array[j-1] + 1);
                }
                
                string Sender = dataReceived.Substring(0, array[0]);
                string timeofday = dataReceived.Substring(array[0] + 1, array[1]-array[0]-1);
                string subject = dataReceived.Substring(array[1] + 1, array[2]-array[1]-1);
                string msg = dataReceived.Substring(array[2] + 1, array[3]-array[2]-1);

                Console.WriteLine("Received : " + dataReceived);

                //---write back the text to the client---
                // Console.WriteLine("Sending back : " + dataReceived);
                Console.WriteLine("\nSender: "+ Sender);
                Console.WriteLine("\nTimestamp: " + timeofday);
                Console.WriteLine("\nSubject line: " + subject);
                Console.WriteLine("\nMessage: " + msg);


                nwStream.Write(buffer, 0, bytesRead);
                client.Close();
                listener.Stop();
                Console.ReadLine();
                i++;
            }
        }
    }
}   