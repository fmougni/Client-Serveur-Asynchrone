using System.Configuration;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet;
using System;
using System.Collections.Generic;
using System.IO;


public class Joueur
{
    public string address { get; set; }
    public int port { get; set; }
    public string password { get; set; }
    public string name { get; set; }

    public Joueur ()
    {
        this.address = "127.0.0.1";
        
    }

}

