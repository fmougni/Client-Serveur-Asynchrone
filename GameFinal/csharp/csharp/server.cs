using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    public class server
    {
        public static void Main() {
            try {
                IPAddress ipAd = IPAddress.Parse("127.0.0.1");
                // use local m/c IP address, and 
                // use the same in the client

/* Initializes the Listener */
                TcpListener myList=new TcpListener(ipAd,8001);

/* Start Listeneting at the specified port */        
                myList.Start();
        
                Console.WriteLine("The server is running at port 8001...");    
                Console.WriteLine("The local End point is  :" + 
                                  myList.LocalEndpoint );
                Console.WriteLine("Waiting for a connection.....");
        
                Socket s=myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                
                Console.WriteLine("Inscription ou Connexion (I/C)");
                
                byte[] b=new byte[100];
                int k=s.Receive(b);
                Console.WriteLine("Recieved...");
                if (Convert.ToChar(b[0]) == 'I')
                {
                    Console.WriteLine("Entrer un nom");
                    
                    Console.WriteLine("Entrer un prenom");
                    
                    Console.WriteLine("Entrer un mdp");
                    
                    Console.WriteLine("Entrer un port");
                    
                }
                else if (Convert.ToChar(b[0]) == 'C')
                {
                    
                }
                for (int i=0;i<k;i++)
                    Console.Write(Convert.ToChar(b[i]));
                ASCIIEncoding asen=new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("\nSent Acknowledgement");
/* clean up */            
                s.Close();
                myList.Stop();
            
            }
            catch (Exception e) {
                Console.WriteLine("Error..... " + e.StackTrace);
            }    
        }
       
    }
}