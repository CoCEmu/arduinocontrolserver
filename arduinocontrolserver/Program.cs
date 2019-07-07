using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace arduinocontrolserver
{
    class Program
    {
     //   const int PORT_NO = 8888;
     //   const string SERVER_IP = "192.168.1.100";

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8888);
            // we set our IP address as server's address, and we also set the port: 8888

            server.Start();  // this will start the server

            while (true)   //we wait for a connection
            {
                TcpClient client = server.AcceptTcpClient();  //if a connection exists, the server will accept it
                Console.WriteLine("Client Connected!");
                NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages

                byte[] hello = new byte[100];   //any message must be serialized (converted to byte array)
                hello = Encoding.Default.GetBytes("Server Started!");  //conversion string => byte array
          ns.Write(hello, 0, hello.Length);     //sending the message

                while (client.Connected)  //while the client is connected, we look for incoming messages
                {
                    string MSGmotorspeed = Console.ReadLine();
                    byte[] motorspeed = new byte[100];
                    motorspeed = Encoding.Default.GetBytes(MSGmotorspeed);
                    ns.Write(motorspeed, 0, motorspeed.Length);
                    Console.WriteLine("Send MSG :" + MSGmotorspeed);
                    try
                    {

                        byte[] msg = new byte[1024];     //the messages arrive as byte array
                        int recivedmsgcount = ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                        if (recivedmsgcount == 0)
                            continue;
                        Console.WriteLine("Recived MSG :" + Encoding.UTF8.GetString(msg)); //now , we write the message as string
                    }

                    catch
                    {
                        int i = 0;
                    }
                }
            }

        }
    }
}