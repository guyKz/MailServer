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
    internal class HandleLogin : IHandleInstance
    {
        public HandleLogin(ServerDB db)
        {
            _db = db;
        }

        public void handle(TcpClient? client, string request)
        {
            var NetStream = client?.GetStream();

            LoginRequest crends = JsonSerializer.Deserialize<LoginRequest>(request)!;

            var allowed = _db.IsUserAllowed(crends.Username, crends.Password);

            var resp = new LoginResponse
            {
                Date = DateTime.Now,
            };

            if (allowed)
            {
                resp.Success = true;
                resp.Message = "Connection granted!";
            }
            else
            {
                resp.Success = false;
                resp.Message = "Connection Not granted!";
            }


            var reps_json = JsonSerializer.Serialize(resp)!;
            var resp_msg = Encoding.ASCII.GetBytes(reps_json);
            NetStream.Write(resp_msg, 0, resp_msg.Length);

            if (!allowed)
            {
                client?.Close();
                throw new Exception("Not allowed");
            }
        }

        private ServerDB _db;
    }
}
