using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.handlers
{
    internal interface IHandleInstance
    {
        void handle(TcpClient? client, string request);
    }
}
