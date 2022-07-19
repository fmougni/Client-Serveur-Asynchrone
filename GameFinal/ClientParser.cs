namespace GameFinal;

public class ClientParser
{
    public string address { get; set; }
    public int port { get; set; }
    public string password { get; set; }
    public string name { get; set; }
    
    public Dictionary<string, ClientParser> clients = new Dictionary<string, ClientParser>();

    public void ajoutClient(string key, ClientParser client)
    {
        clients.Add(key, client);
    }
}