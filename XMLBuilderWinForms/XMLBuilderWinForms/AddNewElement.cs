using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using XMLBuilder;
using System.IO;
using System.Collections.ObjectModel;
using XMLBuilder.Models;

namespace XMLBuilderWinForms
{
    public partial class AddNewElement : Form
    {
        private ComboBox newElementCB;
        private Label NewElementLabel;
        private List<NewElemCombo> comboBoxEntries;
        private TextBox newElemName;
        private Label elemName;
        private TextBox newElemID;
        private Label ID;
        private TextBox newElemRef;
        private Label unityRef;
        private Button newElemCancel;
        private Button newElemConfirm;
        private BindingSource bindingCB;
        public XElement returnElement;

        private void InitializeComponent()
        {
            this.newElementCB = new System.Windows.Forms.ComboBox();
            this.NewElementLabel = new System.Windows.Forms.Label();
            this.newElemName = new System.Windows.Forms.TextBox();
            this.elemName = new System.Windows.Forms.Label();
            this.newElemID = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.Label();
            this.newElemRef = new System.Windows.Forms.TextBox();
            this.unityRef = new System.Windows.Forms.Label();
            this.newElemCancel = new System.Windows.Forms.Button();
            this.newElemConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newElementCB
            // 
            this.newElementCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.newElementCB.FormattingEnabled = true;
            this.newElementCB.Location = new System.Drawing.Point(38, 29);
            this.newElementCB.Name = "newElementCB";
            this.newElementCB.Size = new System.Drawing.Size(156, 26);
            this.newElementCB.TabIndex = 0;
            // 
            // NewElementLabel
            // 
            this.NewElementLabel.AutoSize = true;
            this.NewElementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.NewElementLabel.Location = new System.Drawing.Point(38, 8);
            this.NewElementLabel.Name = "NewElementLabel";
            this.NewElementLabel.Size = new System.Drawing.Size(106, 20);
            this.NewElementLabel.TabIndex = 1;
            this.NewElementLabel.Text = "Element Type";
            // 
            // newElemName
            // 
            this.newElemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.newElemName.Location = new System.Drawing.Point(38, 79);
            this.newElemName.Name = "newElemName";
            this.newElemName.Size = new System.Drawing.Size(156, 24);
            this.newElemName.TabIndex = 2;
            // 
            // elemName
            // 
            this.elemName.AutoSize = true;
            this.elemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.elemName.Location = new System.Drawing.Point(38, 58);
            this.elemName.Name = "elemName";
            this.elemName.Size = new System.Drawing.Size(51, 20);
            this.elemName.TabIndex = 3;
            this.elemName.Text = "Name";
            // 
            // newElemID
            // 
            this.newElemID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.newElemID.Location = new System.Drawing.Point(38, 128);
            this.newElemID.Name = "newElemID";
            this.newElemID.Size = new System.Drawing.Size(156, 24);
            this.newElemID.TabIndex = 4;
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ID.Location = new System.Drawing.Point(38, 107);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(26, 20);
            this.ID.TabIndex = 5;
            this.ID.Text = "ID";
            // 
            // newElemRef
            // 
            this.newElemRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.newElemRef.Location = new System.Drawing.Point(38, 179);
            this.newElemRef.Name = "newElemRef";
            this.newElemRef.Size = new System.Drawing.Size(156, 24);
            this.newElemRef.TabIndex = 6;
            // 
            // unityRef
            // 
            this.unityRef.AutoSize = true;
            this.unityRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.unityRef.Location = new System.Drawing.Point(38, 158);
            this.unityRef.Name = "unityRef";
            this.unityRef.Size = new System.Drawing.Size(75, 20);
            this.unityRef.TabIndex = 7;
            this.unityRef.Text = "Unity Ref";
            // 
            // newElemCancel
            // 
            this.newElemCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.newElemCancel.Location = new System.Drawing.Point(38, 221);
            this.newElemCancel.Name = "newElemCancel";
            this.newElemCancel.Size = new System.Drawing.Size(75, 28);
            this.newElemCancel.TabIndex = 8;
            this.newElemCancel.Text = "Cancel";
            this.newElemCancel.UseVisualStyleBackColor = true;
            this.newElemCancel.Click += new System.EventHandler(this.newElemCancel_Click);
            // 
            // newElemConfirm
            // 
            this.newElemConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.newElemConfirm.Location = new System.Drawing.Point(119, 221);
            this.newElemConfirm.Name = "newElemConfirm";
            this.newElemConfirm.Size = new System.Drawing.Size(75, 28);
            this.newElemConfirm.TabIndex = 9;
            this.newElemConfirm.Text = "Confirm";
            this.newElemConfirm.UseVisualStyleBackColor = true;
            this.newElemConfirm.Click += new System.EventHandler(this.newElemConfirm_Click);
            // 
            // AddNewElement
            // 
            this.ClientSize = new System.Drawing.Size(234, 261);
            this.Controls.Add(this.newElemConfirm);
            this.Controls.Add(this.newElemCancel);
            this.Controls.Add(this.unityRef);
            this.Controls.Add(this.newElemRef);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.newElemID);
            this.Controls.Add(this.elemName);
            this.Controls.Add(this.newElemName);
            this.Controls.Add(this.NewElementLabel);
            this.Controls.Add(this.newElementCB);
            this.Name = "AddNewElement";
            this.Load += new System.EventHandler(this.AddNewElement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public AddNewElement()
        {
            InitializeComponent();
        }

        private void InitializeComboBox()
        {
            comboBoxEntries = new List<NewElemCombo> { new NewElemCombo ("scene"),
                                                       new NewElemCombo ("group"),
                                                       new NewElemCombo ("assembly"),
                                                       new NewElemCombo ("part"),
                                                       new NewElemCombo ("kvtag"),
                                                       new NewElemCombo ("pdfcitation")};
            bindingCB = new BindingSource();
            bindingCB.DataSource = comboBoxEntries;
            newElementCB.DataSource = bindingCB.DataSource;
            newElementCB.DisplayMember = "name";
            newElementCB.ValueMember = "name";
        }

        private void AddNewElement_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
        }

        private void newElemCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void newElemConfirm_Click(object sender, EventArgs e)
        {
            if((newElemName.Text != "") && (newElemID.Text != ""))
            {
                this.returnElement = new XElement(newElementCB.SelectedValue.ToString(),
                                  new XAttribute("name", newElemName.Text),
                                  new XAttribute("id", newElemID.Text),
                                  new XAttribute("ref", newElemRef.Text));
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("New element name and id must not be empty");
            }
        }
    }

}