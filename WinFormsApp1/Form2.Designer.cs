
namespace WinFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuPanel = new System.Windows.Forms.Panel();
            this.addtxt = new System.Windows.Forms.Button();
            this.addgif = new System.Windows.Forms.Button();
            this.addpng = new System.Windows.Forms.Button();
            this.makesure = new System.Windows.Forms.RadioButton();
            this.send = new System.Windows.Forms.Button();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.menuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.Controls.Add(this.addtxt);
            this.menuPanel.Controls.Add(this.addgif);
            this.menuPanel.Controls.Add(this.addpng);
            this.menuPanel.Controls.Add(this.makesure);
            this.menuPanel.Controls.Add(this.send);
            this.menuPanel.Controls.Add(this.txtSubject);
            this.menuPanel.Controls.Add(this.label3);
            this.menuPanel.Controls.Add(this.txtTo);
            this.menuPanel.Controls.Add(this.label2);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(384, 590);
            this.menuPanel.TabIndex = 25;
            // 
            // addtxt
            // 
            this.addtxt.Location = new System.Drawing.Point(51, 232);
            this.addtxt.Name = "addtxt";
            this.addtxt.Size = new System.Drawing.Size(75, 23);
            this.addtxt.TabIndex = 32;
            this.addtxt.Text = "Add Text";
            this.addtxt.UseVisualStyleBackColor = true;
            this.addtxt.Click += new System.EventHandler(this.addtxt_Click_1);
            // 
            // addgif
            // 
            this.addgif.Location = new System.Drawing.Point(258, 232);
            this.addgif.Name = "addgif";
            this.addgif.Size = new System.Drawing.Size(75, 23);
            this.addgif.TabIndex = 31;
            this.addgif.Text = "Add GIF";
            this.addgif.UseVisualStyleBackColor = true;
            this.addgif.Click += new System.EventHandler(this.addgif_Click_1);
            // 
            // addpng
            // 
            this.addpng.Location = new System.Drawing.Point(144, 232);
            this.addpng.Name = "addpng";
            this.addpng.Size = new System.Drawing.Size(75, 23);
            this.addpng.TabIndex = 30;
            this.addpng.Text = "Add PNG";
            this.addpng.UseVisualStyleBackColor = true;
            this.addpng.Click += new System.EventHandler(this.addpng_Click_1);
            // 
            // makesure
            // 
            this.makesure.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.makesure.AutoSize = true;
            this.makesure.Location = new System.Drawing.Point(51, 359);
            this.makesure.Name = "makesure";
            this.makesure.Size = new System.Drawing.Size(190, 19);
            this.makesure.TabIndex = 29;
            this.makesure.TabStop = true;
            this.makesure.Text = "I made sure everthing is correct";
            this.makesure.UseVisualStyleBackColor = true;
            // 
            // send
            // 
            this.send.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.send.Location = new System.Drawing.Point(258, 359);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 28;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(144, 136);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(189, 23);
            this.txtSubject.TabIndex = 27;
            this.txtSubject.Text = "Enter your Subject";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(58, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 21);
            this.label3.TabIndex = 26;
            this.label3.Text = "Subject :";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(144, 96);
            this.txtTo.Name = "txtTo";
            this.txtTo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtTo.Size = new System.Drawing.Size(189, 23);
            this.txtTo.TabIndex = 25;
            this.txtTo.Text = "Enter the target\'s mail";
            this.txtTo.TextChanged += new System.EventHandler(this.txtTo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(90, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 21);
            this.label2.TabIndex = 24;
            this.label2.Text = "To : ";
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(384, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(568, 590);
            this.contentPanel.TabIndex = 26;
            this.contentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.contentPanel_Paint);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(952, 590);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.menuPanel);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Button addtxt;
        private System.Windows.Forms.Button addgif;
        private System.Windows.Forms.Button addpng;
        private System.Windows.Forms.RadioButton makesure;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel contentPanel;
    }
}