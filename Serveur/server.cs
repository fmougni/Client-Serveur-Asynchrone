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
using Commons;

namespace csharp
{
    public class server
    {
        public string name { get; set; }
        public int port { get; set; }
        public string password { get; set; }
        public int maxconnections { get; set; }
        public int timeout { get; set; }
        public string game { get; set; }
        public static ClientParser DeserialiserClient()
        {

            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var fullpath = Path.GetFullPath("client.yaml");
            //var path = Path.Combine("client.yaml");
            var myconfig = deserializer.Deserialize<ClientParser>(File.ReadAllText(fullpath.Replace("Serveur", "Client")));
            return myconfig;
        }

        public server(string name, int port, string password, int maxconnections, int timeout, string game)
        {
            this.name = name;
            this.port = port;
            this.password = password;
            this.maxconnections = maxconnections;
            this.timeout = timeout;
            this.game = game;

        }
        public server()
        {

        }
        public static void ConnectUser(TcpClient obj)
        {
            NetworkStream stream = obj.GetStream();
            int i;
            int j = 0;
            Byte[] bytes = new Byte[256];
            String data = null;

            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {

                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);
                data = data.ToUpper();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                //stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
                j += 1;
                if (j == 2)
                {

                    byte[] msgok = Encoding.UTF8.GetBytes("\nReceived: OK");
                    stream.Write(msgok);
                    Console.WriteLine("Sent: OK");
                    byte[] salleport = Encoding.UTF8.GetBytes("\nReceived: Connexion dans une salle de jeu");
                    Console.WriteLine("Sent: Connexion dans une salle de jeu");
                    stream.Write(salleport);
                }
            }
        }
        private static void ThreadProc(object obj)
        {
            var client = (TcpClient)obj;
            NetworkStream stream2 = client.GetStream();
            ClientParser joueur = new ClientParser();
            joueur = DeserialiserClient();
            if (joueur.port == 8100)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("Bonjour " + joueur.name + " ! Bienvenue dans la salle de jeu Un \n Veuillez patientez quelque instant le jeu commen?era lorsque tout les joueurs seront connect?");
                stream2.Write(msg, 0, msg.Length);
            }
            if (joueur.port == 8200)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("Bonjour " + joueur.name + " ! Bienvenue dans la salle de jeu Deux  \n Veuillez patientez quelque instant le jeu commen?era lorsque tout les joueurs seront connect?");
                stream2.Write(msg, 0, msg.Length);
            }
            if (joueur.port == 8100)
            Console.WriteLine("Connected on Salle un!");
            if (joueur.port == 8200)
            Console.WriteLine("Connected on Salle deux!");
            

        }
        public static async Task Serverun(Serveur serverParseur)
        {
            string loading = "....";
            int k = 0;
            TcpListener salleUn = null;
            int port = serverParseur.configurations[0].port;
            int nombre_jouer = 2;
            int nb_connexions = 0;
            IPAddress localAddr2 = IPAddress.Parse("127.0.0.1");
            salleUn = new TcpListener(localAddr2, port);
            salleUn.Start();
            List<TcpClient> clients = new List<TcpClient>();
            Dictionary<string, ClientParser> InfosClients = new Dictionary<string, ClientParser>();
            List<string> ListeJoueurs = new List<string>();
            TcpClient client1 = new TcpClient();
            TcpClient client2 = new TcpClient();
            clients.Add(client1); clients.Add(client2);

            while (nombre_jouer != nb_connexions)
            {
                Console.Write("Server Un Waiting for a connection... ");
                clients[nb_connexions] = await salleUn.AcceptTcpClientAsync();
                ThreadPool.QueueUserWorkItem(ThreadProc, clients[nb_connexions]);
                ClientParser joueur = new ClientParser();

                joueur = DeserialiserClient();
                InfosClients.Add(joueur.name, joueur);
                ListeJoueurs.Add(joueur.name);
                nb_connexions += 1;
            }
            Console.Write("\nLe nombre de max a ?t? atteint pour le salle un");
            Console.Write("\nChargement du jeu");
            while (k <= 100)
            {
                Console.Write(loading);
                loading += ".";
                k += 1;
            }
            Console.Write("Server un Plein! ");
            serverParseur.games[0].StartGame(clients, ListeJoueurs, serverParseur.games[0]);
        }
        public static async Task Serverdeux(Serveur serverParseur)
        {
            string loading = "....";
            int k = 0;
            TcpListener salleUn = null;
            int port = serverParseur.configurations[1].port;
            int nombre_jouer = 3;
            int nb_connexions = 0;
            IPAddress localAddr2 = IPAddress.Parse("127.0.0.1");
            salleUn = new TcpListener(localAddr2, port);
            salleUn.Start();
            List<TcpClient> clients = new List<TcpClient>();
            Dictionary<string, ClientParser> InfosClients = new Dictionary<string, ClientParser>();
            List<string> ListeJoueurs = new List<string>();
            TcpClient client1 = new TcpClient();
            TcpClient client2 = new TcpClient();
            TcpClient client3 = new TcpClient();
            clients.Add(client1); clients.Add(client2); clients.Add(client3);

            while (nombre_jouer != nb_connexions)
            {
                Console.Write("Server deux Waiting for a connection... ");
                clients[nb_connexions] = await salleUn.AcceptTcpClientAsync();
                ThreadPool.QueueUserWorkItem(ThreadProc, clients[nb_connexions]);
                ClientParser joueur = new ClientParser();

                joueur = DeserialiserClient();
                InfosClients.Add(joueur.name, joueur);
                ListeJoueurs.Add(joueur.name);
                nb_connexions += 1;
            }
            Console.Write("\nLe nombre de max a ?t? atteint pour le salle deux");
            Console.Write("\nChargement du jeu");
            while (k <= 100)
            {
                Console.Write(loading);
                loading += ".";
                k += 1;
            }
            Console.Write("Server un Plein! ");
            serverParseur.games[0].StartGame(clients, ListeJoueurs, serverParseur.games[0]);
        }
        

        public static async Task Main()
        {   //creation des diff?rente version de protocole
            List<String> encryp = new List<string>();
            List<String> compress = new List<string>();
            protocole version1 = new protocole(1, encryp, compress);

            //Creation des salles de jeu
            server salleun = new server("serveurun", 8100, "mdpserveurun", 4, 10, "diamondSVUN");
            server salledeux = new server("serveurdeux", 8200, "mdpserveurdeux", 4, 10, "diamondSVDEUX");
            //server salletrois = new server("serveurtrois", 8300, "mdpserveurtrois", 4, 10, "diamondSVUN");

            //Creation Liste des differentes salles de jeu
            List<server> Configuration = new List<server>();
            Configuration.Add(salleun); Configuration.Add(salledeux);

            //Cr?ation des cartes pi?ges
            Traps araignees = new Traps("araignees", 3);
            Traps serpents = new Traps("serpents", 3);
            Traps laves = new Traps("laves", 3);
            Traps pierre = new Traps("pierre", 3);
            Traps bois = new Traps("bois", 3);

            //Cr?ation liste cartes pi?ges
            List<Traps> Trapslist = new List<Traps>();
            Trapslist.Add(araignees);
            Trapslist.Add(serpents);
            Trapslist.Add(laves);
            Trapslist.Add(pierre);
            Trapslist.Add(bois);
            //Creation du jeu
            Game DiamondV1 = new Game("diamondSVUN", 5, 5, 30, Trapslist);

            List<Game> GameList = new List<Game>();
            GameList.Add(DiamondV1);

            //Serealition en yml pour le ServeurParseur
            Serveur serverParser = new Serveur("One", Configuration, GameList);
            var dumpSerializer = new Serializer();
            StreamWriter Serveryaml = new StreamWriter("Server.yaml");
            dumpSerializer.Serialize(Serveryaml, serverParser);
            Serveryaml.Close();

            //Serealition en yml pour le protocoleParseur
            var dumpSerializer2 = new Serializer();
            StreamWriter Protocoleyaml = new StreamWriter("Protocole.yaml");
            dumpSerializer2.Serialize(Protocoleyaml, version1);
            Protocoleyaml.Close();
            TcpListener server = null;

            try
            {

                int port = 8001;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                Thread Serveurun = new Thread(() => Serverun(serverParser));
                Serveurun.Start();
                Thread Serveurdeux = new Thread(() => Serverdeux(serverParser));
                Serveurdeux.Start();
                
                while (true)
                {
                    Console.Write(" Waiting for a connection...\n ");
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("Connected!");
                    Thread UserThread = new Thread(() => ConnectUser(client));
                    UserThread.Start();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

    }
}