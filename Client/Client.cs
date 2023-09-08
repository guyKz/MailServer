using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Encodings;
using NetworkShared;
using System.Text;
using Server;
using Interfaces;

namespace Client
{
    public class Client
    {
        public Client(string ip_address, int port)
        {
            if (port <= 0 || port >= MAX_PORT_VALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid port number");
            }

            if (ip_address == null)
            {
                throw new ArgumentNullException("Ip address can not be null");
            }

            _port = port;
            _address = ip_address;

            IPHostEntry ipHostInfo = Dns.GetHostEntry(_address);
            if (ipHostInfo == null)
            {
                throw new Exception("Unable to find server");
            }

            var ip = ipHostInfo.AddressList[1];

            _client = new TcpClient();

            _client.Connect(new IPEndPoint(ip, _port));

            _stream = _client.GetStream();
        }

        ~Client()
        {
            Close();
        }

        public void SetDataBase(ServerDB db)
        {
            _serverDB = db;
        }

        public void Close()
        {
            _client?.Close();
            _client?.Dispose();
        }
        public LoginResponse connect(string username, string password)
        {
            var login = new LoginRequest
            { 
                Date = DateTime.Now,
                Username = username,
                Password = password
            };
            
            string login_json = JsonSerializer.Serialize(login);
            var msg = Encoding.ASCII.GetBytes(login_json);
            _stream.Write(msg, 0, msg.Length);

            var login_reponse = new byte[1024];
            var read_bytes = _stream.Read(login_reponse);

            string json_msg = Encoding.ASCII.GetString(login_reponse, 0, read_bytes);

            var resp = JsonSerializer.Deserialize<LoginResponse>(json_msg);

            if (resp == null)
            {
                throw new Exception("Invalid login reponse from server");
            }

            if (resp.Date != null)
            {
                Console.WriteLine("Time: {0}", resp.Date.ToString());
            }

            if (resp.Success != null)
            {
                Console.Write("Connection was ");
                if (!resp.Success.Value)
                {
                    Console.Write("not ");
                }

                Console.WriteLine("successful");
            }

            _username = username;

            return resp;
        }


        public RegisterResponse RegisterUser(string? username, string? password)
        {
            var register = new RegisterRequest
            {
                Date = DateTime.Now,
                Username = username,
                Password = password
            };
            var register_json = JsonSerializer.Serialize(register);
            var register_bytes = Encoding.ASCII.GetBytes(register_json);

            _stream.Write(register_bytes, 0, register_bytes.Length);
            var register_reponse = new byte[1024];
            var read_bytes = _stream.Read(register_reponse);

            var con = register_reponse[0..read_bytes];
            var resp = JsonSerializer.Deserialize<RegisterResponse>(con);
            if (resp == null)
            {
                throw new Exception("Invalid register reponse from server");
            }

            return resp;
        }


        public ComposeResponse Compose(emailData email)
        {
/*            string data = "";
            if (email.atr != null)
            {
                *//*foreach (var item in email.atr)
                {
                    data += $";{item}";
                }*//*

                data = JsonSerializer.Serialize(email.atr);
            }
*/
            var compose = new ComposeRequest
            {
                Date = DateTime.Now,
                from = _username,
                to = email.to,
                subject = email.subject,
                data = Encoding.ASCII.GetBytes(email.atr)
            };

            var compose_json = JsonSerializer.Serialize(compose);
            var compose_bytes = Encoding.ASCII.GetBytes(compose_json);

            var header = new ComposeHeader
            {
                size = compose_bytes.Length
            };

            var header_json = JsonSerializer.Serialize(header);
            var header_bytes = Encoding.ASCII.GetBytes(header_json);

            _stream.Write(header_bytes, 0, header_bytes.Length);
            _stream.Write(compose_bytes, 0, compose_bytes.Length);

            var compose_reponse = new byte[1024];
            var read_bytes = _stream.Read(compose_reponse);

            var resp = JsonSerializer.Deserialize<ComposeResponse>(compose_reponse[0..read_bytes]);
            if (resp == null)
            {
                throw new Exception("Invalid register reponse from server");
            }

            return resp;
        }

        public List<emailData> GetAllEmails()
        {
            GetAllEmailsRequest req = new GetAllEmailsRequest
            {
                Date = DateTime.Now,
                username = _username
            };

            string req_json = JsonSerializer.Serialize(req);
            byte[] req_bytes = Encoding.ASCII.GetBytes(req_json);

            _stream.Write(req_bytes, 0, req_bytes.Length);

            byte[] header_bytes = new byte[1024];
            int read_bytes = _stream.Read(header_bytes, 0, header_bytes.Length);
            var arr = Encoding.ASCII.GetString(header_bytes[0..read_bytes]);
            GetAllEmailsResponseHeader header = JsonSerializer.Deserialize<GetAllEmailsResponseHeader>(arr);
            
            byte[] payload = new byte[header.size];
            read_bytes = _stream.Read(payload, 0, payload.Length);

            GetAllEmailsResponse resp = JsonSerializer.Deserialize<GetAllEmailsResponse>(payload);
            return resp.emails;
        }



        private const int MAX_PORT_VALUE = 65536;

        private string _address;
        private int _port;

        private TcpClient? _client;
        private NetworkStream? _stream;

        private ServerDB? _serverDB;

        private string? _username;
    }
}