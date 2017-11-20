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
            this.FlatTagBox = new System.Windows.Forms.TextBox();
            this.KVTagBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.openCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.flattaglabel = new System.Windows.Forms.Label();
            this.kvtaglabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // XMLTreeViewer
            // 
            this.XMLTreeViewer.Location = new System.Drawing.Point(12, 27);
            this.XMLTreeViewer.Name = "XMLTreeViewer";
            this.XMLTreeViewer.Size = new System.Drawing.Size(294, 547);
            this.XMLTreeViewer.TabIndex = 0;
            // 
            // FlatTagBox
            // 
            this.FlatTagBox.Location = new System.Drawing.Point(312, 395);
            this.FlatTagBox.Multiline = true;
            this.FlatTagBox.Name = "FlatTagBox";
            this.FlatTagBox.Size = new System.Drawing.Size(583, 73);
            this.FlatTagBox.TabIndex = 1;
            // 
            // KVTagBox
            // 
            this.KVTagBox.Location = new System.Drawing.Point(312, 501);
            this.KVTagBox.Multiline = true;
            this.KVTagBox.Name = "KVTagBox";
            this.KVTagBox.Size = new System.Drawing.Size(583, 73);
            this.KVTagBox.TabIndex = 2;
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
            this.newCommand.Size = new System.Drawing.Size(152, 22);
            this.newCommand.Text = "New";
            // 
            // openCommand
            // 
            this.openCommand.Name = "openCommand";
            this.openCommand.Size = new System.Drawing.Size(152, 22);
            this.openCommand.Text = "Open";
            this.openCommand.Click += new System.EventHandler(this.openCommand_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // saveCommand
            // 
            this.saveCommand.Name = "saveCommand";
            this.saveCommand.Size = new System.Drawing.Size(152, 22);
            this.saveCommand.Text = "Save";
            this.saveCommand.Click += new System.EventHandler(this.saveCommand_Click);
            // 
            // saveAsCommand
            // 
            this.saveAsCommand.Name = "saveAsCommand";
            this.saveAsCommand.Size = new System.Drawing.Size(152, 22);
            this.saveAsCommand.Text = "Save As";
            this.saveAsCommand.Click += new System.EventHandler(this.saveAsCommand_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // quitCommand
            // 
            this.quitCommand.Name = "quitCommand";
            this.quitCommand.Size = new System.Drawing.Size(152, 22);
            this.quitCommand.Text = "Quit";
            this.quitCommand.Click += new System.EventHandler(this.quitCommand_Click);
            // 
            // flattaglabel
            // 
            this.flattaglabel.AutoSize = true;
            this.flattaglabel.Location = new System.Drawing.Point(313, 376);
            this.flattaglabel.Name = "flattaglabel";
            this.flattaglabel.Size = new System.Drawing.Size(51, 13);
            this.flattaglabel.TabIndex = 4;
            this.flattaglabel.Text = "Flat Tags";
            // 
            // kvtaglabel
            // 
            this.kvtaglabel.AutoSize = true;
            this.kvtaglabel.Location = new System.Drawing.Point(312, 482);
            this.kvtaglabel.Name = "kvtaglabel";
            this.kvtaglabel.Size = new System.Drawing.Size(82, 13);
            this.kvtaglabel.TabIndex = 5;
            this.kvtaglabel.Text = "Key-Value Tags";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 586);
            this.Controls.Add(this.kvtaglabel);
            this.Controls.Add(this.flattaglabel);
            this.Controls.Add(this.KVTagBox);
            this.Controls.Add(this.FlatTagBox);
            this.Controls.Add(this.XMLTreeViewer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(923, 625);
            this.MinimumSize = new System.Drawing.Size(923, 625);
            this.Name = "MainWindow";
            this.Text = "XMLBuilder";
            this.Load += new System.EventHandler(this.XMLTreeViewer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView XMLTreeViewer;
        private System.Windows.Forms.TextBox FlatTagBox;
        private System.Windows.Forms.TextBox KVTagBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newCommand;
        private System.Windows.Forms.ToolStripMenuItem openCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveCommand;
        private System.Windows.Forms.ToolStripMenuItem saveAsCommand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem quitCommand;
        private System.Windows.Forms.Label flattaglabel;
        private System.Windows.Forms.Label kvtaglabel;
    }
}

