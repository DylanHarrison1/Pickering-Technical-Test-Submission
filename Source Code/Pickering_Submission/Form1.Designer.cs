
namespace Pickering_Submission
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CardSelector = new System.Windows.Forms.ComboBox();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.IPEnter = new System.Windows.Forms.Button();
            this.ButtonHolder = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.DetailBox = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenCard = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.UpdtFrq = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CardSelector
            // 
            this.CardSelector.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CardSelector.FormattingEnabled = true;
            this.CardSelector.Location = new System.Drawing.Point(3, 16);
            this.CardSelector.Name = "CardSelector";
            this.CardSelector.Size = new System.Drawing.Size(356, 21);
            this.CardSelector.TabIndex = 0;
            this.CardSelector.SelectedIndexChanged += new System.EventHandler(this.CardSelector_SelectedIndexChanged);
            // 
            // IPBox
            // 
            this.IPBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.IPBox.Location = new System.Drawing.Point(12, 41);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(120, 20);
            this.IPBox.TabIndex = 2;
            // 
            // IPEnter
            // 
            this.IPEnter.Location = new System.Drawing.Point(147, 41);
            this.IPEnter.Name = "IPEnter";
            this.IPEnter.Size = new System.Drawing.Size(75, 20);
            this.IPEnter.TabIndex = 4;
            this.IPEnter.Text = "Enter";
            this.IPEnter.UseVisualStyleBackColor = true;
            this.IPEnter.Click += new System.EventHandler(this.IPEnter_Click);
            // 
            // ButtonHolder
            // 
            this.ButtonHolder.AutoScroll = true;
            this.ButtonHolder.Location = new System.Drawing.Point(12, 319);
            this.ButtonHolder.Name = "ButtonHolder";
            this.ButtonHolder.Size = new System.Drawing.Size(431, 128);
            this.ButtonHolder.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Please enter IP address of emulated cards:";
            // 
            // DetailBox
            // 
            this.DetailBox.AutoSize = true;
            this.DetailBox.Location = new System.Drawing.Point(3, 69);
            this.DetailBox.Name = "DetailBox";
            this.DetailBox.Size = new System.Drawing.Size(10, 13);
            this.DetailBox.TabIndex = 8;
            this.DetailBox.Text = " ";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.CardSelector);
            this.flowLayoutPanel1.Controls.Add(this.OpenCard);
            this.flowLayoutPanel1.Controls.Add(this.DetailBox);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 80);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(380, 233);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Card";
            // 
            // OpenCard
            // 
            this.OpenCard.Location = new System.Drawing.Point(3, 43);
            this.OpenCard.Name = "OpenCard";
            this.OpenCard.Size = new System.Drawing.Size(75, 23);
            this.OpenCard.TabIndex = 9;
            this.OpenCard.Text = "Open Card";
            this.OpenCard.UseVisualStyleBackColor = true;
            this.OpenCard.Click += new System.EventHandler(this.OpenCard_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Global Update Frequency (ms):";
            // 
            // UpdtFrq
            // 
            this.UpdtFrq.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UpdtFrq.Location = new System.Drawing.Point(267, 26);
            this.UpdtFrq.Name = "UpdtFrq";
            this.UpdtFrq.Size = new System.Drawing.Size(143, 20);
            this.UpdtFrq.TabIndex = 11;
            this.UpdtFrq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdtFrqEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(455, 461);
            this.Controls.Add(this.UpdtFrq);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ButtonHolder);
            this.Controls.Add(this.IPEnter);
            this.Controls.Add(this.IPBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Pickering Card Emulator";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CardSelector;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.Button IPEnter;
        private System.Windows.Forms.FlowLayoutPanel ButtonHolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DetailBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenCard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UpdtFrq;
    }
}

