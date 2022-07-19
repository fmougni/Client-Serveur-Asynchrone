
using csharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;

public class ServerParser
{
    public string name { get; set; }
    public List<server> configurations { get; set; }
    public List<Game> games { get; set; }

    public ServerParser(string name, List<server> configurations, List<Game> games)
    {
        this.name = name;
        this.configurations = configurations;
        this.games = games;
    }

    public ServerParser()
    {
    }
}