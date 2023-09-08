using Interfaces;

namespace NetworkShared
{

    public class UnkownType
    {
        public string? type { get; set; }
    }

    public class LoginRequest
    {
        public string? type { get {return typeof(LoginRequest).Name; } }
        
        public DateTime? Date { get; set; }
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    public class LoginResponse
    {
        public string? type { get { return typeof(LoginResponse).Name; } }
        public DateTime? Date { get; set; }
        public bool? Success { get; set; }

        public string? Message { get; set; }
    }


    public class RegisterRequest
    {
        public string? type { get { return typeof(RegisterRequest).Name; } }

        public DateTime? Date { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }

    }


    public class RegisterResponse
    {
        public string? type { get { return typeof(RegisterResponse).Name; } }

        public DateTime? Date { get; set; }
        public bool? Success { get; set; }

        public string? Message { get; set; }
    }

    public class ComposeHeader
    {
        public string? type { get { return typeof(ComposeHeader).Name; } }

        public int size { get; set; }

    }

    public class ComposeRequest
    {
        public string? type { get { return typeof(ComposeRequest).Name; } }

        public DateTime? Date { get; set; }

        public string? from { get; set; }
        
        public string? to { get; set; }

        public string? subject { get; set; }

        public byte[]? data { get; set; }
    }

    public class ComposeResponse
    {
        public string? type { get { return typeof(ComposeResponse).Name; } }

        public DateTime? Date { get; set; }
        public bool? Success { get; set; }

        public string? Message { get; set; }
    }

    public class GetAllEmailsRequest
    {
        public string? type { get { return typeof(GetAllEmailsRequest).Name; } }
        public DateTime? Date { get; set; }

        public string? username { get; set; }
    }

    public class GetAllEmailsResponseHeader
    {
        public string? type { get { return typeof(GetAllEmailsResponse).Name; } }
        public int size { get; set; }

    }


    public class GetAllEmailsResponse
    {
        public string? type { get { return typeof(GetAllEmailsResponse).Name; } }
        public DateTime? Date { get; set; }
        public bool? Success { get; set; }

        public string? Message { get; set; }
        public List<emailData>? emails { get; set; }
    }
}