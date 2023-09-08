using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;



using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Interfaces
{
    
    public class text : IAttribute
    {
        public string type { get; set; }
        public string Text { get; set; }
        

        public text(string content)
        {
            Text = content;
            type = "Text";
        }
        
        public void Draw(Form p)
        {
            RichTextBox textBox = new RichTextBox();
            textBox.Text = Text;
            textBox.Anchor = AnchorStyles.Top;
            textBox.Dock = DockStyle.Top;
            textBox.ReadOnly = true;
            
            foreach (Control control in p.Controls)
            {
                if (control is FlowLayoutPanel)
                {
                    control.Controls.Add(textBox);
                }
            }
        }
        public byte[] Serialize()
        {
            return Encoding.ASCII.GetBytes(JsonSerializer.Serialize(this));
        }
        //s
        public string deSerialize(byte[] arr)
        {
            //var ASCII = new ASCIIEncoding();
            //arr = JsonSerializer.Deserialize
           string text_string = System.Text.Encoding.UTF8.GetString(arr);
           return text_string;
        }


    }
}

