using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using NetworkShared;

namespace Server.handlers
{
    internal class HandleRegister : IHandleInstance
    {
        public HandleRegister(ServerDB db)
        {
            _db = db;
        }
        public void handle(TcpClient? client, string request)
        {
            var NetStream = client?.GetStream();

            RegisterRequest crends = JsonSerializer.Deserialize<RegisterRequest>(request)!;

            var was_created = _db.AddUser(crends.Username, crends.Password);

            var resp = new RegisterResponse
            {
                Date = DateTime.Now
            };

            if (was_created)
            {
                resp.Success = true;
                resp.Message = "Created User";
            }
            else
            {
                resp.Success = false;
                resp.Message = "Did not created User, smothing went wrong";
            }

            var reps_json = JsonSerializer.Serialize(resp);
            var resp_msg = Encoding.ASCII.GetBytes(reps_json);
            NetStream.Write(resp_msg, 0, resp_msg.Length);

            if (!was_created)
            {
                client?.Close();
                throw new Exception("Not allowed");
            }

        }

        private ServerDB _db;
    }
}
