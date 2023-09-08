using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;

using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;

namespace Interfaces
{
    public interface IAttribute
    {

        void Draw(Form p);
        byte[] Serialize();
        string deSerialize(byte[] arr);
        
    }
}
