using System.Configuration;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet;
using System;
using System.Collections.Generic;
using System.IO;


public class ClientParser
{
    public string address { get; set; }
    public int port { get; set; }
    public string password { get; set; }
    public string name { get; set; }

    public static ClientParser DeserialiserClient()
    {
        // See https://aka.ms/new-console-template for more information

        var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        var path = Path.Combine("client.yaml");
        var myconfig = deserializer.Deserialize<ClientParser>(File.ReadAllText(path));
        return myconfig;
    }

}

