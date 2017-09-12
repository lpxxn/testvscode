using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry("MTK-DEV-ELB-MT4-INTERFACE-2021849240.cn-north-1.elb.amazonaws.com.cn");
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 3390);
              
                // userinfo 
                //var message = "Wact=userinfo&uid=7000&acc=30005&ver=000001&\r\nQUIT\r\n";

                // sys
                var message = "Wact=symbol_info&uid=7000&sym=HKG港金\r\nQUIT\r\n";

                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.

                TcpClient client = new TcpClient();
                client.Connect(remoteEP);

                // Translate the passed message into ASCII and store it as a Byte array.
                //Byte[] data = System.Text.Encoding.Default.GetBytes(message);
                var d = Encoding.Default;
                Byte[] data = System.Text.Encoding.Default.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                var re2 =Encoding.Default.GetString(data, 0, bytes);
                Console.WriteLine("Received: ASCII: {0} Default: {1}\n", responseData, re2);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
