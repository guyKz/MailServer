using NetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Interfaces;

namespace Server.handlers
{
    internal class HandleCompose : IHandleInstance
    {
        public HandleCompose(ServerDB db)
        {
            _db = db;
        }


        public void handle(TcpClient? client, string request)
        {
            NetworkStream NetStream = client.GetStream();

            var header = JsonSerializer.Deserialize<ComposeHeader>(request)!;

            byte[] data = new byte[header.size];
            NetStream.Read(data,0, data.Length);

            var composeReq = JsonSerializer.Deserialize<ComposeRequest>(data);

            ComposeResponse com = new ComposeResponse
            {
                Date = DateTime.Now,
            };

            if (_db.Compose(composeReq))
            {
                com.Success = true;
                com.Message = "Sucess";
            }
            else
            {
                com.Success = false;
                com.Message = "Failed";
            }

            string msg = JsonSerializer.Serialize(com);
            byte[] msg_bytes = Encoding.ASCII.GetBytes(msg);

            NetStream.Write(msg_bytes, 0, msg_bytes.Length);
        }


        private ServerDB _db;

    }
 }
