using System;
using System.Threading.Tasks;

class Program
{
   static void Main(string[] args)
   {
        var server = new Server.Server(12345);
        server.run();
    }
}