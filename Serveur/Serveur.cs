using csharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;




public class Serveur
{
    public string name;
    public List<server> configurations { get; set; }
    public List<Game> games { get; set; }

    public Serveur(string name, List<server> configurations, List<Game> games)
    {
        this.name = name;
        this.configurations = configurations;
        this.games = games;
    }

}