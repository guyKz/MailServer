using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Client;
using NetworkShared;

namespace WinFormsApp1
{
    public partial class Form6 : Form
    {
        string username;
        string password;
        private Client.Client client;

        public Form6()
        {
            client = new Client.Client("127.0.0.1", 12345);
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(308, 486);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

        }
        void setUsername()
        {
            username = txtUserName.Text;
        }
        void setPassword()
        {
            password = txtpassword.Text;
        }
        string getUsername()
        {
            return username;
        }
        string getPassword()
        {
            return password;
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var respone = client.connect(txtUserName.Text, txtpassword.Text).Success;
            if (respone == null)
            {
                MessageBox.Show("Server not found");
                Application.Exit();
            }
            if (respone == true)
            {
                new Form1(client).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("The Username or Password you entered is incorrect, pllease try again");
                txtUserName.Clear();
                txtpassword.Clear();
                txtUserName.Focus();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new Form7(client).Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
