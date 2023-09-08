using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        private Client.Client client;
        private Form4 menu;
        bool sidebarExpand;
        public Form1(Client.Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
           if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if(sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
           else
            {
                sidebar.Width += 10;
                if(sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void buttonCompose_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2(client));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3(client));
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            menu = new Form4();
            //menu.add_row() - for loop on each emailData
            openChildForm(menu);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new Form5());
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
