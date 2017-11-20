namespace XMLBuilderWinForms
{
    partial class MainWindow
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
            this.XMLTreeViewer = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.openCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.addElementButton = new System.Windows.Forms.Button();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.unityRefLabel = new System.Windows.Forms.Label();
            this.unityRefTextBox = new System.Windows.Forms.TextBox();
            this.kvtaglabel = new System.Windows.Forms.Label();
            this.KVTagBox = new System.Windows.Forms.TextBox();
            this.assetLabel = new System.Windows.Forms.Label();
            this.assetTextBox = new System.Windows.Forms.TextBox();
            this.pdfLabel = new System.Windows.Forms.Label();
            this.pdfTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // XMLTreeViewer
            // 
            this.XMLTreeViewer.Location = new System.Drawing.Point(12, 27);
            this.XMLTreeViewer.Name = "XMLTreeViewer";
            this.XMLTreeViewer.Size = new System.Drawing.Size(294, 496);
            this.XMLTreeViewer.TabIndex = 0;
            this.XMLTreeViewer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.XMLTreeViewer_AfterSelect);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(907, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "topMenu";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCommand,
            this.openCommand,
            this.toolStripSeparator1,
            this.saveCommand,
            this.saveAsCommand,
            this.toolStripSeparator2,
            this.quitCommand});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "File";
            // 
            // newCommand
            // 
            this.newCommand.Name = "newCommand";
            this.newCommand.Size = new System.Drawing.Size(114, 22);
            this.newCommand.Text = "New";
            // 
            // openCommand
            // 
            this.openCommand.Name = "openCommand";
            this.openCommand.Size = new System.Drawing.Size(114, 22);
            this.openCommand.Text = "Open";
            this.openCommand.Click += new System.EventHandler(this.openCommand_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // saveCommand
            // 
            this.saveCommand.Name = "saveCommand";
            this.saveCommand.Size = new System.Drawing.Size(114, 22);
            this.saveCommand.Text = "Save";
            this.saveCommand.Click += new System.EventHandler(this.saveCommand_Click);
            // 
            // saveAsCommand
            // 
            this.saveAsCommand.Name = "saveAsCommand";
            this.saveAsCommand.Size = new System.Drawing.Size(114, 22);
            this.saveAsCommand.Text = "Save As";
            this.saveAsCommand.Click += new System.EventHandler(this.saveAsCommand_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(111, 6);
            // 
            // quitCommand
            // 
            this.quitCommand.Name = "quitCommand";
            this.quitCommand.Size = new System.Drawing.Size(114, 22);
            this.quitCommand.Text = "Quit";
            this.quitCommand.Click += new System.EventHandler(this.quitCommand_Click);
            // 
            // addElementButton
            // 
            this.addElementButton.Location = new System.Drawing.Point(13, 530);
            this.addElementButton.Name = "addElementButton";
            this.addElementButton.Size = new System.Drawing.Size(293, 44);
            this.addElementButton.TabIndex = 6;
            this.addElementButton.Text = "Add New Element";
            this.addElementButton.UseVisualStyleBackColor = true;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(312, 50);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(583, 20);
            this.nameTextBox.TabIndex = 7;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(312, 27);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(114, 20);
            this.nameLabel.TabIndex = 8;
            this.nameLabel.Text = "Element Name";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(312, 114);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(583, 20);
            this.idTextBox.TabIndex = 9;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(312, 91);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(102, 20);
            this.idLabel.TabIndex = 10;
            this.idLabel.Text = "Element ID #";
            // 
            // unityRefLabel
            // 
            this.unityRefLabel.AutoSize = true;
            this.unityRefLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unityRefLabel.Location = new System.Drawing.Point(312, 157);
            this.unityRefLabel.Name = "unityRefLabel";
            this.unityRefLabel.Size = new System.Drawing.Size(243, 20);
            this.unityRefLabel.TabIndex = 12;
            this.unityRefLabel.Text = "Unity Reference Code  (Optional)";
            // 
            // unityRefTextBox
            // 
            this.unityRefTextBox.Location = new System.Drawing.Point(312, 180);
            this.unityRefTextBox.Name = "unityRefTextBox";
            this.unityRefTextBox.Size = new System.Drawing.Size(583, 20);
            this.unityRefTextBox.TabIndex = 11;
            // 
            // kvtaglabel
            // 
            this.kvtaglabel.AutoSize = true;
            this.kvtaglabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kvtaglabel.Location = new System.Drawing.Point(312, 221);
            this.kvtaglabel.Name = "kvtaglabel";
            this.kvtaglabel.Size = new System.Drawing.Size(120, 20);
            this.kvtaglabel.TabIndex = 14;
            this.kvtaglabel.Text = "Key-Value Tags";
            // 
            // KVTagBox
            // 
            this.KVTagBox.Location = new System.Drawing.Point(312, 244);
            this.KVTagBox.Multiline = true;
            this.KVTagBox.Name = "KVTagBox";
            this.KVTagBox.Size = new System.Drawing.Size(583, 129);
            this.KVTagBox.TabIndex = 13;
            // 
            // assetLabel
            // 
            this.assetLabel.AutoSize = true;
            this.assetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assetLabel.Location = new System.Drawing.Point(312, 395);
            this.assetLabel.Name = "assetLabel";
            this.assetLabel.Size = new System.Drawing.Size(160, 20);
            this.assetLabel.TabIndex = 16;
            this.assetLabel.Text = "Asset URI  (Optional)";
            // 
            // assetTextBox
            // 
            this.assetTextBox.Location = new System.Drawing.Point(312, 418);
            this.assetTextBox.Name = "assetTextBox";
            this.assetTextBox.Size = new System.Drawing.Size(583, 20);
            this.assetTextBox.TabIndex = 15;
            // 
            // pdfLabel
            // 
            this.pdfLabel.AutoSize = true;
            this.pdfLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pdfLabel.Location = new System.Drawing.Point(312, 463);
            this.pdfLabel.Name = "pdfLabel";
            this.pdfLabel.Size = new System.Drawing.Size(219, 20);
            this.pdfLabel.TabIndex = 18;
            this.pdfLabel.Text = "PDF Page Number  (Optional)";
            // 
            // pdfTextBox
            // 
            this.pdfTextBox.Location = new System.Drawing.Point(312, 486);
            this.pdfTextBox.Name = "pdfTextBox";
            this.pdfTextBox.Size = new System.Drawing.Size(583, 20);
            this.pdfTextBox.TabIndex = 17;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 586);
            this.Controls.Add(this.pdfLabel);
            this.Controls.Add(this.pdfTextBox);
            this.Controls.Add(this.assetLabel);
            this.Controls.Add(this.assetTextBox);
            this.Controls.Add(this.kvtaglabel);
            this.Controls.Add(this.KVTagBox);
            this.Controls.Add(this.unityRefLabel);
            this.Controls.Add(this.unityRefTextBox);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.addElementButton);
            this.Controls.Add(this.XMLTreeViewer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(923, 625);
            this.MinimumSize = new System.Drawing.Size(923, 625);
            this.Name = "MainWindow";
            this.Text = "XMLBuilder Prototype - Alpha 1";
            this.Load += new System.EventHandler(this.XMLTreeViewer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView XMLTreeViewer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newCommand;
        private System.Windows.Forms.ToolStripMenuItem openCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveCommand;
        private System.Windows.Forms.ToolStripMenuItem saveAsCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem quitCommand;
        private System.Windows.Forms.Button addElementButton;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label unityRefLabel;
        private System.Windows.Forms.TextBox unityRefTextBox;
        private System.Windows.Forms.Label kvtaglabel;
        private System.Windows.Forms.TextBox KVTagBox;
        private System.Windows.Forms.Label assetLabel;
        private System.Windows.Forms.TextBox assetTextBox;
        private System.Windows.Forms.Label pdfLabel;
        private System.Windows.Forms.TextBox pdfTextBox;
    }
}

