using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.Mail;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace PInvokeTest
{

    public partial class WEMAIL : Form
    {
        public WEMAIL()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("stagger?");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sendButton_Click(object sender, EventArgs e)
        {
           
                string to = reciptxtbox.Text; //Takes user input for recipient
                string from = "TestingPurposes@wemail.com"; //Current Sender. Needs changing to be personal to each account

                MailAddress Sender = new MailAddress(from); //Its possible to do MailAdress(from,"displayname"). Could be usefull ?
                MailAddress Recipient = new MailAddress(to);
                MailMessage message = new MailMessage(Sender, Recipient);

                string subject = subjecttxtbox.Text;
                string text = @messagetxtbox.Text;
                string domain = to.Substring(to.LastIndexOf('@') + 1); //Takes everything to the right of @

                if (domain.Equals("wemail.com", StringComparison.OrdinalIgnoreCase))
                {
                    SendMail.Wemailtransfer(Sender,Recipient,subject,text);
                }
                else
                {
                    SendMail.Regular(Sender,Recipient,subject,text);
                }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
    public class SendMail
    {
        static string server = "mail.smtp2go.com"; //Current SMTP server. Coupled to IP
        public static void Regular(MailAddress Sender, MailAddress Recipient, string subject, string text)
        {
            MailMessage message = new MailMessage(Sender, Recipient);
            SmtpClient client = new SmtpClient(server);
            client.Send(message);
            MessageBox.Show("Email Sent!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Wemailtransfer(MailAddress Sender,MailAddress Recipient, string subject, string text)
        {
            char DL = '';
            try
            {
                MailMessage message = new MailMessage(Sender, Recipient);
                message.Subject = subject;
                message.Body = text;
                //MessageBox.Show("You're sending to a Wemail account", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                const int PORT_NO = 5000;
                const string LOCALHOST = "127.0.0.1";
                TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
                NetworkStream nwStream = tcpclient.GetStream();
                //sndr, timestamp, subject, txt
                var TTS = Sender.Address + DL + DateTime.Today.ToShortDateString() + DL + message.Subject + DL + message.Body + DL;//Improvement is to use stringbuilder append() as it is faster
                byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(TTS);
                Console.WriteLine("Sending : " + message.Body);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                //---read back the text---
                byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
                Console.WriteLine("Received : " + Encoding.UTF8.GetString(bytesToRead, 0, bytesRead));
                Console.ReadLine();
                tcpclient.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.SendMailException(ex);
            }
        }
        public static void Forward()
        {
            //
        }
        public static void Reply()
        {
            //
        }
    }
    public class ExceptionHandler
    {
        public static void SendMailException(Exception ex)
        {
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            MessageBox.Show(ex.ToString(), "error", buttons, MessageBoxIcon.Warning);
        }
    }
}
