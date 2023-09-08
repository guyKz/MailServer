using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Interfaces;
using Client;
using System.Text.Json;

namespace WinFormsApp1
{


    public partial class Form2 : Form
    {
        private Client.Client client;
        int counter = 0;
        List<IAttribute> mail = new List<IAttribute>();
        string to, subject;
        void setTo()
        {
            to = txtTo.Text;
        }
        void setSubject()
        {
            subject = txtSubject.Text;
        }
        string getTo()
        {
            return to;
        }
        string getSubject()
        {
            return subject;
        }
        public Form2(Client.Client client)
        
        {
            InitializeComponent();
            this.client = client;
            this.Dock = DockStyle.Fill;
            contentScroll_ = new VScrollBar();
            contentPanel.Controls.Add(contentScroll_);
            
            contentPanel.AutoScroll = false;
            contentScroll_.Dock = DockStyle.Right;
            content_ = new Panel();
            content_.AutoScroll = true;
            contentPanel.Controls.Add(content_);
            content_.Dock = DockStyle.Left;
            contentScroll_.Scroll += new System.Windows.Forms.ScrollEventHandler(ScrollEvent);
            

        }
        private void ScrollEvent(object sender,ScrollEventArgs e)
        {
            content_.VerticalScroll.Value = contentScroll_.Value;
        }
        private VScrollBar contentScroll_;
        private Panel content_;
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void addtxt_Click_1(object sender, EventArgs e)
        {

                RichTextBox Text = new RichTextBox();
                content_.Controls.Add(Text);
                Text.Size = new Size(300, 60);
                Text.Dock = DockStyle.Top;

            counter++;

        }

        private void addpng_Click_1(object sender, EventArgs e)
        {

            String imageLocation = "";

                PictureBox Png = new PictureBox();
                content_.Controls.Add(Png);
                Png.Size = new Size(300, 60);
                Png.SizeMode = PictureBoxSizeMode.Zoom;
                Png.Dock = DockStyle.Top;
                Png.Tag = "PNG";

                try
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "png files(*.png)|*.png";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                    using (var fromFile = Image.FromFile(dialog.FileName))
                    {
                        Png.Image = new Bitmap(fromFile);
                    }
                        imageLocation = dialog.FileName;
                        //Png.ImageLocation = imageLocation;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    content_.Controls.Remove(Png);
                }

            counter++;
        }

        private void addgif_Click_1(object sender, EventArgs e)
        {
            String imageLocation = "";


            PictureBox gif = new PictureBox();
            content_.Controls.Add(gif);
            gif.Size = new Size(300, 60);
            gif.SizeMode = PictureBoxSizeMode.Zoom;
            gif.Dock = DockStyle.Top;
            gif.Tag = "GIF";
            
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "gif files(*.gif)|*.gif";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    gif.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contentPanel.Controls.Remove(gif);
            }
            
            counter++;
        }

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void send_Click(object sender, EventArgs e)
        {


            var email = new emailData
            {
                to = txtTo.Text,
                subject = txtSubject.Text,
                //atr = new List<string>()
            };

            List<string> email_content = new List<string>();


            List<IAttribute> mail = new List<IAttribute>();
            foreach (Control control in this.content_.Controls)
            {
                
                string type = control.GetType().ToString();
                string win_type = "System.Windows.Forms.";
                if (type ==  win_type + "RichTextBox")
                {
                    //MessageBox.Show("Hello");
                    RichTextBox txt = (RichTextBox)control;
                    email_content.Add(Encoding.ASCII.GetString(new text(txt.Text).Serialize()));
                }
                else if(type == win_type + "PictureBox")
                {
                    if (control.Tag.ToString() == "GIF")
                    {
                        if (control is PictureBox picture)
                        {
                            //MessageBox.Show("gif");
                            gif a = new gif(picture.Image);
                            email_content.Add(Encoding.ASCII.GetString(a.Serialize()));
                        }
                    }
                    if (control.Tag.ToString() == "PNG")
                    {
                        if(control is PictureBox picture)
                        {
                            //MessageBox.Show("png");
                            png a = new png(picture.Image);
                            email_content.Add(Encoding.ASCII.GetString(a.Serialize()));

                        }

                    }
                }

            }

            //string s = JsonSerializer.Serialize(email);
            if (email_content.Count == 0)
            {
                email.atr = "";
            }
            else
            {
                email.atr = JsonSerializer.Serialize(email_content);
                List<string> s = JsonSerializer.Deserialize<List<string>>(email.atr);

            }
            var res = client.Compose(email);
            if (res == null)
            {
                MessageBox.Show("An error had occured");
            }
            else
            {
                if(res.Success == true)
                {
                    MessageBox.Show(res.Message);

                }
                else
                {
                    MessageBox.Show("Unable to send email");
                }
            }
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
            
        }
    }
}
