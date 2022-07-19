// See https://aka.ms/new-console-template for more information

using System.Configuration;
using GameFinal;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet;

var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

 var myconfig = deserializer.Deserialize<ClientParser>(File.ReadAllText("C:/Users/loulou/RiderProjects/GameFinal/GameFinal/ressources/client.yaml"));
    

