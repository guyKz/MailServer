using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Client;

namespace WinFormsApp1
{
    public partial class Form7 : Form
    {
        string user, pass;
        private Client.Client client;
        public Form7(Client.Client client)
        {
            InitializeComponent();
            this.client = client;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(308, 486);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
        }
     
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var respone = client.RegisterUser(txtReUser.Text, txtRePass.Text).Success;
            if (respone == true)
            {
                new Form6().Show();
                this.Hide();
            }
            else
                Application.Exit();
            // add to database//
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new Form6().Show();
            this.Hide();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
