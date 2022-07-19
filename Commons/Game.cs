using Commons;
using Commons.Cards;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

public class Game
{
    public string id { get; set; }
    public int maxplayers { get; set; }
    public int caves { get; set; }
    public int cardsquantity { get; set; }
    public List<Traps> trapList { get; set; }

    public Game(string id, int maxplayers, int caves, int cardsquantity, List<Traps> trapList)
    {
        this.id = id;
        this.maxplayers = maxplayers;
        this.caves = caves;
        this.cardsquantity = cardsquantity;
        this.trapList = trapList;

    }
    public Game()
    {

    }
    public string reglesduJeu()
    {
        string regles = "Explorez la grotte de Tacora d un pas prudent, sous la seule lumiere de vos torches.A chacune de vos avancees,";
        regles += "\ndecouvrez un nouveau couloir et ramassez les diamants trouves sur votre chemin.";
        regles += "\nDecidez ensuite si vous voulez rentrerau campement pour mettre ensecurite tous vos tresors dans votre coffre, ou si vous preferez continuer";
        regles += "\nvotre expedition vers les profondeurs de la grotte a vos risques et perils!";
        regles += "\nCar si vous tombez dans un piege, vous vous precipiterez vers la sortie en laissant derriere vous toutes vos trouvailles, et rentrerez au campement les mains vides…";
        regles += "\net les jambes tremblantes !";
        regles += "\nAventurez - vous assez loin dans la grotte de Tacora pour ramasser le plus de diamants possible. Soyez assez malin pour rentrer au campement avant de vous faire prendre au";
        regles += "\npiege, ou pour recuperer tous les diamants qui restent sur le chemin. Celui d’entre vous qui aura le plus de diamants dans son coffre a la fin de la partie sera vainqueur.";
        return regles;
    }
    public string elementsDuJeu()
    {
        string elementsDuJeu = "\n\n\n" + @" ELEMENTS DE JEU";
        elementsDuJeu += "\n\n" + @" CARTE EXPEDITION:";
        elementsDuJeu += "\n" + @"Les cartes Expédition vous montrent ce que vousdécouvrez en vous enfonçant dans la grotte de Tacora.Elles sont de trois types : ";
        elementsDuJeu += "\n" + @" CARTE TRESORS:";
        elementsDuJeu += "\n" + @" : indiquent lenombre de Rubis que voustrouvez à cet endroit de la grotte (1, 2, 3, 4…).";
        elementsDuJeu += "\n" + @" Cartes Danger:";
        elementsDuJeu += "\n" + @" : indiquent les pièges qui vous surprennent dans la grotte. ";
        elementsDuJeu += "\n" + @" Méfiez - vous doncdes araignées géantes, des serpents, des puits de lave,des boules de pierre et des béliers de bois. ";
        elementsDuJeu += "\n" + @" Chaque piège est présent en trois exemplaires. ";
        elementsDuJeu += "\n" + @" PIERRES PRECIEUSE ";
        elementsDuJeu += "\n" + @" Les Pierres Précieuses sont représentées par des Diamants .";
        elementsDuJeu += "\n" + @" ";
        elementsDuJeu += "\n" + @" CARTE DECISION ";
        elementsDuJeu += "\n" + @" : Les cartes Décision vous permettent d’indiquer aux autres joueurs si vous souhaitez vous avancer plus profondément dans la grotte ou rentrer au campement. ";
        elementsDuJeu += "\n" + @" Cartes Continuer : indiquent aux autres joueurs que vous décidez de poursuivre l’exploration. ";
        elementsDuJeu += "\n" + @" Cartes Sortir : indiquent aux autres joueurs que vous décidez de rentrer au campement et de déposer vos Pierres Précieuses dans votre Coffre.";
        elementsDuJeu += "\n" + @"  Coffres ";
        elementsDuJeu += "\n" + @"Les Coffres sont un endroit sûr où déposer vos Diamants lorsque vous rentrez au campement avant de vous faire piéger dans la grotte de Tacora. "; ;
        elementsDuJeu += "\n" + @"  Tous les Rubis et Diamants qui se trouvent dans votre Coffre sont en sécurité jusqu’à la fin de la partie ";
        return elementsDuJeu;
    }

    public static void afficherMerssageSalle(byte[] msg, List<TcpClient> clients)
    {
        NetworkStream stream1;
        foreach (TcpClient client in clients)
        {
            stream1 = client.GetStream();
            stream1.Write(msg, 0, msg.Length);
            
        }
        
       
    }
   

    public void StartGame(List<TcpClient> clients, List<string> joueurs, Game Configgames)
    {
        byte[] msg;
        
        NetworkStream stream3 = clients[0].GetStream();
        NetworkStream stream4 = clients[1].GetStream();
        
        msg = System.Text.Encoding.ASCII.GetBytes("\nPlay Diamond");
        afficherMerssageSalle(msg,clients);
        msg = System.Text.Encoding.ASCII.GetBytes("\nVoici les regles du jeu");
        afficherMerssageSalle(msg, clients);
        msg = System.Text.Encoding.UTF8.GetBytes("\n\n\n" + reglesduJeu());
        afficherMerssageSalle(msg, clients);
        msg = System.Text.Encoding.UTF8.GetBytes("\n\n\n" + elementsDuJeu());
        afficherMerssageSalle(msg, clients);
        msg = System.Text.Encoding.UTF8.GetBytes("\n\n\n Veuillez patientez pendant quelques instant pour mise en place du jeu");
        afficherMerssageSalle(msg, clients);
        MiseenPlaceDuJeu(joueurs, Configgames, msg,clients);
    }
    public void remplircaves(Cave cave,Joueurs Joueur1 , Joueurs Joueur2, Joueurs Joueur3, Joueurs Joueur4, Joueurs Joueur5)
    {
        cave.lesJoueurs.Add(Joueur1.name, Joueur1);
        cave.lesJoueurs.Add(Joueur2.name, Joueur2);
        cave.lesJoueurs.Add(Joueur3.name, Joueur3);
        cave.lesJoueurs.Add(Joueur4.name, Joueur4);
        cave.lesJoueurs.Add(Joueur5.name, Joueur5);
    }
    public int piocherUnecarte(List<int> Listecartes)
    {
        int nbdiamant = 0;
        nbdiamant = Listecartes[Listecartes.Count - 1];
        int index = Listecartes.Count - 1;
        Listecartes.RemoveAt(index);
        return nbdiamant;
    }
    public Traps piocherUneCartePiege(List<Traps> listeTraps)
    { Random hasardpiege = new Random();
        int hasard;
        hasard = hasardpiege.Next(0, 6);
        while (listeTraps[hasard].quantity == 0)
        {
            hasard = hasardpiege.Next(0, 5);
        }
        listeTraps[hasard].quantity = listeTraps[hasard].quantity - 1;
        return listeTraps[hasard] ;
    }
    public void MiseenPlaceDuJeu(List<string> joueurs, Game Configgames, byte[] msg, List<TcpClient> clients)
    {

        Decision continuer = new Decision("continuer");
        Decision sortir = new Decision("sortir");
        List<int> cartesTresors = new List<int>(); 
        Traps Dangerun = new Traps(); Dangerun = Configgames.trapList[0];
        Traps Dangerdeux = new Traps(); Dangerdeux = Configgames.trapList[1];
        Traps Dangertrois = new Traps(); Dangertrois = Configgames.trapList[2];
        Traps Dangerquatre = new Traps(); Dangerquatre = Configgames.trapList[3];
        Traps Dangercinq = new Traps(); Dangercinq = Configgames.trapList[4];
        List<Traps> listeTraps = new List<Traps>();
        listeTraps.Add(Dangerun); listeTraps.Add(Dangerdeux); listeTraps.Add(Dangertrois); listeTraps.Add(Dangerquatre);
        listeTraps.Add(Dangercinq);

        Dictionary<string,Joueurs > DictionnaireDesJoeur= new Dictionary<string, Joueurs>();
        Dictionary<string, int> MontantsJoueurs = new Dictionary<string, int>();
        List<Cave> CavesNonVisites = new List<Cave>();
        Cave caves1 = new Cave(DictionnaireDesJoeur, MontantsJoueurs,0,0,0);
        Cave caves2 = new Cave(DictionnaireDesJoeur, MontantsJoueurs, 0, 0, 0);
        Cave caves3 = new Cave(DictionnaireDesJoeur, MontantsJoueurs, 0, 0, 0);
        Cave caves4 = new Cave(DictionnaireDesJoeur, MontantsJoueurs, 0, 0, 0);
        Cave caves5 = new Cave(DictionnaireDesJoeur, MontantsJoueurs, 0, 0, 0);

        CavesNonVisites.Add(caves1); CavesNonVisites.Add(caves2); CavesNonVisites.Add(caves3); CavesNonVisites.Add(caves4); CavesNonVisites.Add(caves5);

        //il y'as des salles de jeux pouvant accepter n client (n>=2 && n<=5)  , il faaut 5 joueur pour lancer une partie
        int nombreJoueurinTheParty = 0;
        List<Joueurs> ListeJoueurParty = new List<Joueurs>();
        Joueurs Joueur1 = new Joueurs(joueurs[0], 0); ListeJoueurParty.Add(Joueur1);
        Joueurs Joueur2 = new Joueurs(joueurs[1], 0); ListeJoueurParty.Add(Joueur2);
        Joueurs Joueur3;
        Joueurs Joueur4;
        Joueurs Joueur5;
        foreach (string j in joueurs)
            nombreJoueurinTheParty += 1;
        if (Configgames.maxplayers - nombreJoueurinTheParty != 0)
        {
            if (Configgames.maxplayers - nombreJoueurinTheParty == 3)
            {
                Joueur3 = new Joueurs("Ordi_Joueur3", 0); ListeJoueurParty.Add(Joueur3);
                Joueur4 = new Joueurs("Ordi_Joueur4", 0); ListeJoueurParty.Add(Joueur4);
                Joueur5 = new Joueurs("Ordi_Joueur5", 0); ListeJoueurParty.Add(Joueur5);
            }
            else if (Configgames.maxplayers - nombreJoueurinTheParty == 2)
            {
                Joueur3 = new Joueurs(joueurs[2], 0); ListeJoueurParty.Add(Joueur3);
                Joueur4 = new Joueurs("Ordi_Joueur4", 0); ListeJoueurParty.Add(Joueur4);
                Joueur5 = new Joueurs("Ordi_Joueur5", 0); ListeJoueurParty.Add(Joueur5);

            }
            else
            {
                Joueur3 = new Joueurs(joueurs[2], 0); ListeJoueurParty.Add(Joueur3);
                Joueur4 = new Joueurs(joueurs[3], 0); ListeJoueurParty.Add(Joueur4);
                Joueur5 = new Joueurs("Ordi_Joueur5", 0); ListeJoueurParty.Add(Joueur5);

            }
            msg = System.Text.Encoding.ASCII.GetBytes("\n\nMise en place du jeu terminee :");
            msg = System.Text.Encoding.ASCII.GetBytes("\nVoici la listes des joueurs pour cette partie :");
            afficherMerssageSalle(msg, clients);
            int i = 1;
            foreach (Joueurs J in ListeJoueurParty)
            {
                msg = System.Text.Encoding.ASCII.GetBytes("\nJoueur " + i + " : ");
                afficherMerssageSalle(msg, clients);
                msg = System.Text.Encoding.ASCII.GetBytes(J.name);
                afficherMerssageSalle(msg, clients);
                i += 1;
            }
            Random nbDiamant = new Random();
            for (int y = 1; y <= Configgames.cardsquantity; y++)
            {
                cartesTresors.Add(nbDiamant.Next(5, 20));
            }
            // LES ELEMENTS DU JEU SONT PRETS LA PARTIE PEUT COMMENCER
            msg = System.Text.Encoding.ASCII.GetBytes("\n\n Debut de la partie 1 ");
            afficherMerssageSalle(msg, clients);
            msg = System.Text.Encoding.ASCII.GetBytes("\n\n Exploration de la premiere cave ");
            afficherMerssageSalle(msg, clients);
            //Première partie
            remplircaves(caves1, Joueur1, Joueur2, Joueur3, Joueur4, Joueur5);
            Random HasardcartePiochee = new Random();
            int PiegeOrTresor = 0;
            int nbdiamantfind =0;
            string lastCartePiochee="";
            while (caves1.lesJoueurs.Count > 0 && cartesTresors.Count > 0)
            {
                PiegeOrTresor = HasardcartePiochee.Next(1,3);
                if (PiegeOrTresor == 1)
                {
                    nbdiamantfind = piocherUnecarte(cartesTresors);
                    msg = System.Text.Encoding.ASCII.GetBytes("\n\n Felicitation vous avez trouver "+nbdiamantfind+" diamants !");
                    afficherMerssageSalle(msg, clients);
                }
                else
                {
                    nbdiamantfind = piocherUnecarte(cartesTresors);
                    msg = System.Text.Encoding.ASCII.GetBytes("\n\n Attention vous êtes tomber sur un piège de type " + piocherUneCartePiege(listeTraps).name + " !");
                    afficherMerssageSalle(msg, clients);
                    if (lastCartePiochee == piocherUneCartePiege(listeTraps).name)
                    {
                        caves1.lesJoueurs.Clear();
                        //Fin d'exploration de grotte

                    }
                    lastCartePiochee = piocherUneCartePiege(listeTraps).name;
                }
            }
            //Deuxieme partie
            remplircaves(caves2, Joueur1, Joueur2, Joueur3, Joueur4, Joueur5);
            while (caves2.lesJoueurs.Count < 0)
            {

            }
            //Troisieme partie
            remplircaves(caves2, Joueur1, Joueur2, Joueur3, Joueur4, Joueur5);
            while (caves3.lesJoueurs.Count < 0)
            {

            }
            //Quatrieme partie
            remplircaves(caves4, Joueur1, Joueur2, Joueur3, Joueur4, Joueur5);
            while (caves2.lesJoueurs.Count < 0)
            {

            }
            //Cinquieme partie
            remplircaves(caves5, Joueur1, Joueur2, Joueur3, Joueur4, Joueur5);
            while (caves2.lesJoueurs.Count < 0)
            {

            }


        }
    }
}