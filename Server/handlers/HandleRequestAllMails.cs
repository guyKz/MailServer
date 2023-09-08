using NetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.handlers
{
    internal class HandleRequestAllMails : IHandleInstance
    {
        public HandleRequestAllMails(ServerDB db)
        {
            _db = db;
        }

        public void handle(TcpClient? client, string request)
        {
            var NetStream = client?.GetStream();

            GetAllEmailsRequest crends = JsonSerializer.Deserialize<GetAllEmailsRequest>(request)!;

            var emails = _db.GetAllMails(crends.username);

            var resp = new GetAllEmailsResponse
            {
                Date = DateTime.Now,
            };

            if (emails == null)
            {
                resp.Success = false;
                resp.Message = "Unable to get emails for given client";
                resp.emails = null;
            }
            else
            {
                resp.Success = true;
                resp.Message = "Got to get emails for given client";
                resp.emails = emails;
            }

            string msg_json = JsonSerializer.Serialize(resp);
            byte[] msg_bytes = Encoding.ASCII.GetBytes(msg_json);

            GetAllEmailsResponseHeader header = new GetAllEmailsResponseHeader
            {
                size = msg_bytes.Length
            };

            string header_json = JsonSerializer.Serialize(header);
            byte[] header_bytes = Encoding.ASCII.GetBytes(header_json);
            //Encoding.ASCII.GetBytes(header_json).CopyTo(header_bytes, 0);

            NetStream.Write(header_bytes, 0, header_bytes.Length);
            NetStream.Write(msg_bytes, 0, msg_bytes.Length);
        }

        private ServerDB _db;
    }
}
