using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Interfaces
{
    public static class Factory
    {
        class unkown_class_type
        {
            public string type { get; set; }
            public string Text { get; set; }

            public int height { get; set; }
            public int width { get; set; }
            public byte[] data { get; set; }

            public string image { get; set; }

        }
        public static IAttribute CreateFromString(string s)
        {
            unkown_class_type unkown_Class_Type = JsonSerializer.Deserialize<unkown_class_type>(s);

            if (String.Equals(unkown_Class_Type.type, "Text"))
            {
                return new text(unkown_Class_Type.Text);
            }
            else if (String.Equals(unkown_Class_Type.type, "PNG"))
            {
                return new png(unkown_Class_Type.height, unkown_Class_Type.width, unkown_Class_Type.data, unkown_Class_Type.image);
            }
            else if (String.Equals(unkown_Class_Type.type, "GIF"))
            {
                return new gif(unkown_Class_Type.height, unkown_Class_Type.width, unkown_Class_Type.data, unkown_Class_Type.image);
            }
            else
            {
                throw new Exception("Invalid type");
            }
        }
    }
}
