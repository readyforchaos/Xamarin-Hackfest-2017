namespace StorageQueueDemo
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
            this.gruppeTilkobling = new System.Windows.Forms.GroupBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.tekstQueueName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tekstConnectionString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.messagelist = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.messagetext = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gruppeTilkobling.SuspendLayout();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gruppeTilkobling
            // 
            this.gruppeTilkobling.Controls.Add(this.buttonConnect);
            this.gruppeTilkobling.Controls.Add(this.tekstQueueName);
            this.gruppeTilkobling.Controls.Add(this.label2);
            this.gruppeTilkobling.Controls.Add(this.tekstConnectionString);
            this.gruppeTilkobling.Controls.Add(this.label1);
            this.gruppeTilkobling.Location = new System.Drawing.Point(18, 18);
            this.gruppeTilkobling.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gruppeTilkobling.Name = "gruppeTilkobling";
            this.gruppeTilkobling.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gruppeTilkobling.Size = new System.Drawing.Size(922, 174);
            this.gruppeTilkobling.TabIndex = 0;
            this.gruppeTilkobling.TabStop = false;
            this.gruppeTilkobling.Text = "Connect to Storage Queue";
            // 
            // knappKobleTil
            // 
            this.buttonConnect.Location = new System.Drawing.Point(687, 122);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonConnect.Name = "knappKobleTil";
            this.buttonConnect.Size = new System.Drawing.Size(226, 35);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // tekstQueueName
            // 
            this.tekstQueueName.Location = new System.Drawing.Point(14, 125);
            this.tekstQueueName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tekstQueueName.Name = "tekstQueueName";
            this.tekstQueueName.Size = new System.Drawing.Size(662, 26);
            this.tekstQueueName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "QueueName";
            // 
            // tekstConnectionString
            // 
            this.tekstConnectionString.Location = new System.Drawing.Point(14, 49);
            this.tekstConnectionString.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tekstConnectionString.Name = "tekstConnectionString";
            this.tekstConnectionString.Size = new System.Drawing.Size(898, 26);
            this.tekstConnectionString.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ConnectionString\r\n";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.pictureBox1);
            this.groupBox.Controls.Add(this.buttonRefresh);
            this.groupBox.Controls.Add(this.buttonDelete);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.messagelist);
            this.groupBox.Controls.Add(this.buttonAdd);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.messagetext);
            this.groupBox.Enabled = false;
            this.groupBox.Location = new System.Drawing.Point(18, 202);
            this.groupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox.Size = new System.Drawing.Size(922, 297);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Manager";
            // 
            // knappLastInn
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(573, 252);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(105, 35);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // knappFjern
            // 
            this.buttonDelete.Location = new System.Drawing.Point(687, 252);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDelete.Name = "knappFjern";
            this.buttonDelete.Size = new System.Drawing.Size(226, 35);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Delete all messages";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(568, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Queue";
            // 
            // listeMeldinger
            // 
            this.messagelist.FormattingEnabled = true;
            this.messagelist.ItemHeight = 20;
            this.messagelist.Location = new System.Drawing.Point(573, 49);
            this.messagelist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messagelist.Name = "listeMeldinger";
            this.messagelist.Size = new System.Drawing.Size(338, 184);
            this.messagelist.TabIndex = 3;
            // 
            // knappLeggTil
            // 
            this.buttonAdd.Location = new System.Drawing.Point(14, 89);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAdd.Name = "knappLeggTil";
            this.buttonAdd.Size = new System.Drawing.Size(294, 35);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add message";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Message to put on queue";
            // 
            // tekstMelding
            // 
            this.messagetext.Location = new System.Drawing.Point(14, 49);
            this.messagetext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messagetext.Name = "tekstMelding";
            this.messagetext.Size = new System.Drawing.Size(292, 26);
            this.messagetext.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::StorageQueueDemo.Properties.Resources._out;
            this.pictureBox1.Location = new System.Drawing.Point(14, 132);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(294, 171);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 517);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.gruppeTilkobling);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "StorageQueue Demo";
            this.gruppeTilkobling.ResumeLayout(false);
            this.gruppeTilkobling.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gruppeTilkobling;
        private System.Windows.Forms.TextBox tekstQueueName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tekstConnectionString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messagetext;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox messagelist;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

