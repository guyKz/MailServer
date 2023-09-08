using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.Json;
using Interfaces;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        private Client.Client client;
        private List<emailData> all_emails;
        private List<List<IAttribute>> email_atr;
        public Form3(Client.Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            all_emails = client.GetAllEmails();
            email_atr = new List<List<IAttribute>>();

            foreach (var email in all_emails)
            {
                List<string> list = JsonSerializer.Deserialize<List<string>>(email.atr);
                List<IAttribute> attribute = new List<IAttribute>();
                foreach (var item in list)
                {
                    attribute.Add(Factory.CreateFromString(item));
                }
                email_atr.Add(attribute);
                dataGridView1.Rows.Add(email.to, email.subject, "Click to view more");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Form form = new Form();
            form.Size = new Size(500, 500);
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Dock = DockStyle.Fill;
            flp.FlowDirection = FlowDirection.TopDown;
            form.Controls.Add(flp);

            DataGridView gridView = (DataGridView)sender;
            
            form.TopLevel = true;
            foreach (var item in email_atr[e.RowIndex])
            {
                item.Draw(form);
            }

            Label from_lbl = new Label();
            from_lbl.Text = $"from: {all_emails[e.RowIndex].to}";
            from_lbl.Anchor = AnchorStyles.Top;
            from_lbl.Dock = DockStyle.Top;

            form.Controls.Add(from_lbl);

            Label subject_lbl = new Label();
            subject_lbl.Text = $"subject: {all_emails[e.RowIndex].subject}";
            subject_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            subject_lbl.Dock = DockStyle.Top;
            form.Controls.Add(subject_lbl);

            form.Show();
        }
    }
}
