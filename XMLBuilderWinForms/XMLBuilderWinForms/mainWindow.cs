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

namespace XMLBuilderWinForms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void XMLTreeViewer_Load(object sender, EventArgs e)
        {
            KVTagBox.Text = Application.StartupPath + "\\sample.xml";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openCommand_Click(object sender, EventArgs e)
        {
            try
            {
                documentHandler dh = new documentHandler("sample.xml");
                IEnumerable<IEnumerable<XElement>> groups = dh.getAllGroups("string");
                string output = "";

                foreach (IEnumerable<XElement> it in groups) {
                    MessageBox.Show("Number of Scenes");
                    foreach (XElement el in it)
                    {
                        MessageBox.Show(el.ToString());
                    }
                }

                //XMLTreeViewer.Nodes.Add(new TreeNode(dom.DocumentElement.Name));
                //TreeNode tNode = new TreeNode();
                //tNode = XMLTreeViewer.Nodes[0];

                //AddNode(dom.DocumentElement, tNode);
                //XMLTreeViewer.ExpandAll();
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
