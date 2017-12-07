namespace XMLBuilderWinForms
{
    partial class AddElementWindow
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
            this.newSceneButton = new System.Windows.Forms.Button();
            this.addGroupButton = new System.Windows.Forms.Button();
            this.newAssemblyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newSceneButton
            // 
            this.newSceneButton.Location = new System.Drawing.Point(86, 12);
            this.newSceneButton.Name = "newSceneButton";
            this.newSceneButton.Size = new System.Drawing.Size(135, 23);
            this.newSceneButton.TabIndex = 0;
            this.newSceneButton.Text = "Add New Scene";
            this.newSceneButton.UseVisualStyleBackColor = true;
            // 
            // addGroupButton
            // 
            this.addGroupButton.Location = new System.Drawing.Point(86, 41);
            this.addGroupButton.Name = "addGroupButton";
            this.addGroupButton.Size = new System.Drawing.Size(135, 23);
            this.addGroupButton.TabIndex = 1;
            this.addGroupButton.Text = "Add New Group";
            this.addGroupButton.UseVisualStyleBackColor = true;
            // 
            // newAssemblyButton
            // 
            this.newAssemblyButton.Location = new System.Drawing.Point(86, 70);
            this.newAssemblyButton.Name = "newAssemblyButton";
            this.newAssemblyButton.Size = new System.Drawing.Size(135, 23);
            this.newAssemblyButton.TabIndex = 2;
            this.newAssemblyButton.Text = "Add New Assembly";
            this.newAssemblyButton.UseVisualStyleBackColor = true;
            // 
            // AddElementWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 314);
            this.Controls.Add(this.newAssemblyButton);
            this.Controls.Add(this.addGroupButton);
            this.Controls.Add(this.newSceneButton);
            this.Name = "AddElementWindow";
            this.Text = "AddElementWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newSceneButton;
        private System.Windows.Forms.Button addGroupButton;
        private System.Windows.Forms.Button newAssemblyButton;
    }
}