using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetworkShared;
using Server.handlers;

namespace Server
{
    internal class StreamHandler
    {
        public StreamHandler(TcpClient? client, ServerDB db, ServerCommonInterface.CallbackRemoveThread? callback)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client TcpClient is null");
            }

            if (callback == null)
            {
                throw new ArgumentNullException("Callback must be in none null state");
            }

            _client = client;
            _stream = client.GetStream();
            _callback = callback;

            _handlers = new Dictionary<string, IHandleInstance>
            {
                { typeof(LoginRequest).Name, new HandleLogin(db) },
                { typeof(RegisterRequest).Name, new HandleRegister(db) },
                { typeof(ComposeHeader).Name, new HandleCompose(db) },
                { typeof(GetAllEmailsRequest).Name, new HandleRequestAllMails(db) }
            };
        }

        ~StreamHandler()
        {
            CloseConnection();
        }

        public void CloseConnection()
        {
            if (_callback != null)
            {
                _callback(Thread.CurrentThread.ManagedThreadId);
            }
        }

        public void handle_client()
        {
            if (_stream == null)
            {
                throw new ArgumentNullException("Invalid stream");
            }

            // handle the client while reading 1024 bytes
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    int bytes_read = _stream.Read(buffer, 0, buffer.Length);
                    string json_msg = Encoding.ASCII.GetString(buffer, 0, bytes_read);
                    var unkown_type = JsonSerializer.Deserialize<UnkownType>(json_msg);

                    var comamnd = unkown_type.type.ToString();

                    Console.WriteLine($"Message Type: {unkown_type?.type?.ToString()}");


                    if (_handlers.ContainsKey(comamnd))
                    {
                        _handlers[comamnd].handle(_client, json_msg);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Command, Disconnecting User");
                        CloseConnection();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }


        private TcpClient? _client;

        private NetworkStream? _stream;

        private ServerCommonInterface.CallbackRemoveThread? _callback;

        private Dictionary<string, IHandleInstance> _handlers;

        private ServerDB _db;
    }
}
