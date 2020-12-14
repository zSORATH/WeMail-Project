using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.IO;

public class Email
{
    public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, contentText;

    public Email() { }

    public Email(string inputEmailType, string inputSenderAddress, string inputReceiverAddress, string inputTimeStamp, string inputSubjectMatter, string inputContentText)
    {
        emailType = inputEmailType;
        senderAddress = inputSenderAddress;
        receiverAddress = inputReceiverAddress;
        timeStamp = inputTimeStamp;
        subjectMatter = inputSubjectMatter;
        contentText = inputContentText; 
    }
}

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
                string FUNC = dataReceived.Substring(0, dataReceived.IndexOf(''));
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                Email receivedEmail = new Email();
                XmlSerializer xmlSerializer = new XmlSerializer(receivedEmail.GetType());
                StringReader stringified = new StringReader(dataReceived);
                receivedEmail = (Email)xmlSerializer.Deserialize(stringified); 
                
                string Sender = receivedEmail.senderAddress;
                string timeofday = receivedEmail.timeStamp;
                string subject = receivedEmail.subjectMatter;
                string msg = receivedEmail.contentText; 

                Console.WriteLine("Received : " + dataReceived);

                //---write back the text to the client---
                // Console.WriteLine("Sending back : " + dataReceived);
                Console.WriteLine("\nSender: "+ Sender);
                Console.WriteLine("\nTimestamp: " + timeofday);
                Console.WriteLine("\nSubject line: " + subject);
                Console.WriteLine("\nMessage: " + msg);
                Console.WriteLine("\nFUNC: " + FUNC); 


                nwStream.Write(buffer, 0, bytesRead);
                client.Close();
                listener.Stop();
                Console.ReadLine();
                i++;
            }
        }
    }
}   