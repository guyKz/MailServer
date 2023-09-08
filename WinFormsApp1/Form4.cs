using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Interfaces;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.dataGridView1.ReadOnly = true;
        }
        public void add_row(emailData a)
        {
            //var email = a.atr.
            dataGridView1.Rows.Add(a.to, a.subject, "B");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Form Data = new Form();
            Label from = new Label();
            from.Text = $"from : {e.RowIndex}";
            Label subject = new Label();
            var grid = (DataGridView)sender;
            grid.Rows.Add("Hello!", "A","B");
        }
    }
}
