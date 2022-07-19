using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class Cave
    {
        public Dictionary<string, Joueurs> lesJoueurs { get; set; }
        public Dictionary<string, int> montantJoueur { get; set; }
        public int nbtraps { get; set; }
        public int nbTresor { get; set; }
        public int nbTrophee { get; set; }
        //public Cave()
        //{

        //}
        public Cave(Dictionary<string, Joueurs> lesJoueurs, Dictionary<string, int> montantJoueur, int nbtraps, int nbTresor, int nbTrophee)
        {
            this.lesJoueurs = lesJoueurs;
            this.montantJoueur = montantJoueur;
            this.nbtraps = nbtraps;
            this.nbTresor = nbTresor;
            this.nbTrophee = nbTrophee; 
        }
    }
}
