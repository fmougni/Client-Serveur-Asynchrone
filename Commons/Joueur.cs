using Commons.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class Joueurs
    {   //represente le coffre d'un joueur
        public int nbDiamantcoffre;
        public string name { get; set; }

        public Decision cartes { get; set; }

        public Joueurs(string name, int nbDiamantcoffre)
        {
            this.name = name;
            this.nbDiamantcoffre = nbDiamantcoffre;
        }
    }
}
