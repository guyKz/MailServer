using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using NetworkShared;

namespace Server
{
    internal class Server
    {
        public Server(int port)
        {
            if (port <= 0 || port >= MAX_PORT_VALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid port number");
            }

            _port = port;

            var ip_endpoint = new IPEndPoint(IPAddress.Any, port);
            _tcp_listener = new(ip_endpoint);

            _db = new ServerDB();

            _tasks = new Dictionary<int, Thread>();
        }

        ~Server()
        {
            _tcp_listener.Stop();
            _db.Close();
        }
        
        public void run()
        {
            _tcp_listener.Start(10);

            try
            {
                while (true)
                {
                    //using TcpClient handler = _tcp_listener.AcceptTcpClient();

                    var handler = _tcp_listener.AcceptTcpClient();

                    if (handler == null)
                    {
                        continue;
                    }

                    var thread = new Thread(() =>
                    {
                        ServerCommonInterface.CallbackRemoveThread callback = RemoveThreadFromCollection;

                        var con = handler;
                        var client = new StreamHandler(con, _db, callback);
                        client.handle_client();

                    });

                    var thread_id = thread.ManagedThreadId;

                    _tasks.Add(thread_id, thread);
                    _tasks[thread_id].Start();
                }
            }
            catch (Exception ex)
            {
                Console.Write("Server was interupted by signal ");
                Console.WriteLine(ex.Message);
            }
        }

        private void RemoveThreadFromCollection(int id)
        {
            _tasks.Remove(id);
        }


        private const int MAX_PORT_VALUE = 65536;

        private int _port;

        private TcpListener _tcp_listener;

        private ServerDB _db;

        private Dictionary<int, Thread> _tasks;
    }
}
