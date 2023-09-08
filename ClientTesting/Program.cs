using Client;
using Interfaces;

class Program
{
    static void Main(string[] args)
    {
        var client = new Client.Client("127.0.0.1", 12345);
        //client.RegisterUser("Nadav18", "N");
        client.connect("Nadav", "N");
        var e = new emailData
        {
            to = "Nadav",
            subject = "This is mail",
            atr = null
        };

        client.Compose(e);
        foreach (var i in client.GetAllEmails())
        {
            Console.WriteLine(i.subject);
        }
    }
}