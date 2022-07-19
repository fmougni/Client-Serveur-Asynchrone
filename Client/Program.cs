using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Threading;

using csharp;

public class client
{

    public static ServerParser DeserialiserServeur()
    {
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        var fullpath = Path.GetFullPath("Server.yaml");
        var myconfig = deserializer.Deserialize<ServerParser>(File.ReadAllText(fullpath.Replace("Client", "Serveur")));
        return myconfig;
    }
    public static protocoleParseur DeserialiserProtocole()
    {
        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        var fullpath = Path.GetFullPath("Protocole.yaml");
        var myconfig = deserializer.Deserialize<protocoleParseur>(File.ReadAllText(fullpath.Replace("Client", "Serveur")));
        return myconfig;
    }

    public static void Main()
    {

        try
        {
            TcpClient tcpclnt = new TcpClient();
            tcpclnt.Connect("127.0.0.1", 8001);
            TcpClient tcpclntjeu = new TcpClient();
            bool connectsalle = false;
            // use the ipaddress as in the server program
            NetworkStream stm = tcpclnt.GetStream();
            Console.WriteLine("Connected");
            List<String> encryp = new List<string>();
            List<String> compress = new List<string>();
            protocoleParseur version1 = DeserialiserProtocole();
            Joueur gamer = new Joueur();
            ServerParser Serveur = new ServerParser();
            Serveur = DeserialiserServeur();
            int Num_message = 0;
            while (true)
            {

                if (Num_message == 0)
                {
                    Console.WriteLine("Veuillez choisir une version de protocole");
                    Console.WriteLine("Version : " + version1.version + " Encryption []: Compression :[]");
                    Console.WriteLine("Version :2 Encryption []: Compression :[]");
                    Console.WriteLine("Version : 3 Not found");
                    Console.WriteLine("Entre le numero de version du protocole choisie, parmis la liste des versions disponibles");
                }
                if (Num_message == 1)
                {
                    Console.WriteLine("Entrez un nom ");
                }
                if (Num_message == 2)
                {
                    Console.WriteLine("Entrez le mot de passe d'une salle de jeu pour pouvoir y acceder");
                }
                if (Num_message == 3)
                {

                    Byte[] bb = new Byte[256];
                    //Int32 bytes = stm.Read(bb, 0, bb.Length);
                    String responseData = null;
                    int i;
                    while (true)
                    {
                        i = stm.Read(bb, 0, bb.Length);
                        //bytes = stm.Read(bb);
                        responseData = System.Text.Encoding.ASCII.GetString(bb, 0, i);
                        if (responseData != "")
                        {
                            Console.WriteLine(responseData);
                        }
                        if (!connectsalle)
                        {
                            tcpclnt.Close();
                            int portSalle = 0;
                            foreach (server serv in Serveur.configurations)
                            {
                                if (serv.password == gamer.password)
                                {
                                    portSalle = serv.port;
                                    gamer.port = portSalle;
                                    var dumpSerializer = new Serializer();
                                    StreamWriter Clientyaml = new StreamWriter("Client.yaml");
                                    dumpSerializer.Serialize(Clientyaml, gamer);
                                    Clientyaml.Close();
                                }
                            }
                            if (portSalle != 0)
                                tcpclntjeu.Connect("127.0.0.1", portSalle);
                            else
                                Console.WriteLine("Erreur mot de passe invalide , veuillez retentez la connection.");
                            //tcpclntjeu.Connect("127.0.0.1", 8100);
                            stm = tcpclntjeu.GetStream();
                            connectsalle = true;
                        }
                    }




                }

                String str = Console.ReadLine();
                if (Num_message == 1)
                {
                    gamer.name = str;
                }
                if (Num_message == 2)
                {
                    gamer.password = str;
                }
                Num_message += 1;
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                stm.Write(ba, 0, ba.Length);
                Console.WriteLine("Transmitting.....");
            }
        }
        //Thread ennvoyer = new Thread(()=> Run(stm));
        //ennvoyer.Start();

        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
        }
    }

}